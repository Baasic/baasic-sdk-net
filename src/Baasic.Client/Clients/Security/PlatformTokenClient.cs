﻿using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.Security;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model;
using Baasic.Client.Model.Security;
using Baasic.Client.Utility;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baasic.Client.Security.Token
{
    public class PlatformTokenClient : ClientBase, IPlatformTokenClient
    {
        #region Constructors

        public PlatformTokenClient(IPlatformClientConfiguration configuration, IBaasicPlatformClientFactory baasicPlatformClientFactory, IJsonFormatter jsonFormatter)
            : base(configuration)
        {
            BaasicPlatformClientFactory = baasicPlatformClientFactory;
            JsonFormatter = jsonFormatter;
            Configuration = configuration;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        public new IPlatformClientConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets or sets the baasic platform client factory.
        /// </summary>
        /// <value>The baasic platform client factory.</value>
        protected virtual IBaasicPlatformClientFactory BaasicPlatformClientFactory { get; set; }

        /// <summary>
        /// Gets or sets the json formatter.
        /// </summary>
        /// <value>The json formatter.</value>
        protected virtual IJsonFormatter JsonFormatter { get; set; }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected override string ModuleRelativePath => "Login";

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the asynchronous.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="tokenOptions">The token options.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<IAuthenticationToken> CreateAsync(string username, string password, TokenOptions tokenOptions = null)
        {
            using (var client = BaasicPlatformClientFactory.Create(Configuration))
            {
                if (tokenOptions == null)
                {
                    tokenOptions = new TokenOptions();
                }

                var request = new HttpRequestMessage(HttpMethod.Post, client.GetApiUrl(string.Format("{0}?{1}", this.ModuleRelativePath, tokenOptions.GetOptionsCSV())))
                {
                    Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password)
                    })
                };

                IAuthenticationToken token = null;
                IAuthenticationToken oldToken = this.Configuration.TokenHandler.Get();
                this.Configuration.TokenHandler.Clear();

                try
                {
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    BaasicErrorResponse responseObject = null;
                    try
                    {
                        responseObject = await client.ReadContentAsync<BaasicErrorResponse>(response);
                    }
                    catch { }

                    if (response.StatusCode.Equals(HttpStatusCode.OK) ||
                        response.StatusCode.Equals(HttpStatusCode.Created))
                    {
                        token = this.ReadToken(JsonFormatter.Deserialize<Newtonsoft.Json.Linq.JObject>(responseContent));
                    }
                    else
                    {
                        throw new BaasicClientException((int)response.StatusCode, "Unable to create new token.", responseObject?.Error, responseObject?.ErrorCode ?? 0, new Exception(responseContent));
                    }
                }
                catch (Exception ex)
                {
                    this.SaveToken(oldToken);
                    throw ex;
                }

                this.SaveToken(token);

                return token;
            }
        }

        /// <summary>
        /// Destroys the asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<bool> DestroyAsync()
        {
            try
            {
                using (var client = this.BaasicPlatformClientFactory.Create(this.Configuration))
                {
                    var token = this.Configuration.TokenHandler.Get();

                    if (token != null)
                    {
                        var request = new HttpRequestMessage(HttpMethod.Delete, client.GetApiUrl(this.ModuleRelativePath))
                        {
                            Content = JsonFormatter.SerializeToHttpContent(new { Type = token.Scheme, Token = token.Token })
                        };

                        var response = await client.SendAsync(request);

                        var tokenHandler = this.Configuration.TokenHandler;
                        if (tokenHandler != null)
                        {
                            tokenHandler.Clear();
                        }
                        return true;
                    }
                }
                return false;
            }
            catch (BaasicClientException ex)
            {
                if (ex.ErrorCode == (int)HttpStatusCode.NotFound)
                {
                    return false;
                }
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the user asynchronous.
        /// </summary>
        /// <param name="embed">The embed.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<IAuthenticatedUser> GetUserAsync(string embed)
        {
            using (var client = this.BaasicPlatformClientFactory.Create(this.Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(this.ModuleRelativePath));
                InitializeQueryString(uriBuilder, embed, string.Empty);
                return await client.GetAsync<AuthenticatedUser>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Refreshes the asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual async Task<IAuthenticationToken> RefreshAsync()
        {
            using (var client = this.BaasicPlatformClientFactory.Create(this.Configuration))
            {
                var oldToken = this.Configuration.TokenHandler.Get();

                var newToken = await client.PutAsync<Newtonsoft.Json.Linq.JObject>(client.GetApiUrl(this.ModuleRelativePath), Newtonsoft.Json.Linq.JObject.FromObject(new
                {
                    token = oldToken.Token,
                    type = oldToken.Scheme
                }));

                var token = this.ReadToken(newToken);

                this.SaveToken(token);

                return token;
            }
        }

        protected virtual IAuthenticationToken ReadToken(Newtonsoft.Json.Linq.JObject rawToken)
        {
            var error = rawToken.Property("error");
            if (error != null)
            {
                throw new InvalidOperationException(error.ToString());
            }
            var token = rawToken.ToObject<AuthenticationToken>();
            if (token.ExpiresIn.HasValue)
            {
                token.ExpirationDate = DateTime.UtcNow.AddSeconds(token.ExpiresIn.GetValueOrDefault());
            }
            else
            {
                token.ExpirationDate = DateTime.UtcNow.AddSeconds(token.SlidingWindow.GetValueOrDefault());
            }
            return token;
        }

        protected virtual bool SaveToken(IAuthenticationToken token)
        {
            if (token != null)
            {
                var tokenHandler = this.Configuration.TokenHandler;
                if (tokenHandler != null)
                {
                    return tokenHandler.Save(token);
                }
            }

            return false;
        }

        #endregion Methods
    }
}