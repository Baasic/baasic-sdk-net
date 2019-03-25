using Baasic.Client.Common;
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
    /// <summary>
    /// Token Client.
    /// </summary>
    public class TokenClient : ClientBase, ITokenClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        /// <param name="jsonFormatter">JSON formatter.</param>
        public TokenClient(
            IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory,
            IJsonFormatter jsonFormatter
            )
            : base(configuration)
        {
            this.BaasicClientFactory = baasicClientFactory;
            this.JsonFormatter = jsonFormatter;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the baasic client factory.
        /// </summary>
        /// <value>The baasic client factory.</value>
        protected virtual IBaasicClientFactory BaasicClientFactory { get; set; }

        /// <summary>
        /// Gets or sets JSON formatter.
        /// </summary>
        /// <value>JSON formatter.</value>
        protected virtual IJsonFormatter JsonFormatter { get; set; }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected override string ModuleRelativePath
        {
            get { return "Login"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously creates new <see cref="IAuthenticationToken" /> for specified used.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="tokenOptions">The token options.</param>
        /// <returns>New <see cref="IAuthenticationToken" /> .</returns>
        public virtual async Task<IAuthenticationToken> CreateAsync(string username, string password, TokenOptions tokenOptions = null)
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
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
                    var responseObject = await client.ReadContentAsync<BaasicErrorResponse>(response);

                    if (response.StatusCode.Equals(HttpStatusCode.OK) ||
                        response.StatusCode.Equals(HttpStatusCode.Created))
                    {
                        token = this.ReadToken(JsonFormatter.Deserialize<Newtonsoft.Json.Linq.JObject>(responseContent));
                    }
                    else
                    {
                        throw new BaasicClientException((int)response.StatusCode, "Unable to create new token.", responseObject.Error, responseObject.ErrorCode, new Exception(responseContent));
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
        /// Asynchronously destroys the <see cref="IAuthenticationToken" /> .
        /// </summary>
        /// <returns>True if <see cref="IAuthenticationToken" /> is destroyed, false otherwise.</returns>
        public virtual async Task<bool> DestroyAsync()
        {
            try
            {
                using (var client = this.BaasicClientFactory.Create(this.Configuration))
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
        /// Asynchronously gets the <see cref="IAuthenticationUser" />.
        /// </summary>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>New <see cref="IAuthenticationUser" /> .</returns>
        public virtual async Task<IAuthenticatedUser> GetUserAsync(string embed)
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                UrlBuilder uriBuilder = new UrlBuilder(client.GetApiUrl(this.ModuleRelativePath));
                InitializeQueryString(uriBuilder, embed, string.Empty);
                return await client.GetAsync<AuthenticatedUser>(uriBuilder.ToString());
            }
        }

        /// <summary>
        /// Asynchronously refreshes the <see cref="IAuthenticationToken" /> .
        /// </summary>
        /// <param name="token">Token to update.</param>
        /// <returns>New <see cref="IAuthenticationToken" /> .</returns>
        public virtual async Task<IAuthenticationToken> RefreshAsync()
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
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