using Baasic.Client.Configuration;
using Baasic.Client.Formatters;
using Baasic.Client.TokenHandler;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baasic.Client.Token
{
    /// <summary>
    /// Token Client.
    /// </summary>
    public class TokenClient : ClientBase, ITokenClient
    {
        #region Properties

        /// <summary>
        /// Gets or sets the baasic client factory.
        /// </summary>
        /// <value>The baasic client factory.</value>
        protected virtual IBaasicClientFactory BaasicClientFactory { get; set; }

        /// <summary>
        /// Gets or sets json formatter.
        /// </summary>
        /// <value>Json formatter.</value>
        protected virtual IJsonFormatter JsonFormatter { get; set; }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected override string ModuleRelativePath
        {
            get { return "Login"; }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyValueClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        /// <param name="jsonFormatter">Json formatter.</param>
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

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Asynchronously creates new <see cref="IAuthenticationToken" /> for specified used.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>New <see cref="IAuthenticationToken" />.</returns>
        public async Task<IAuthenticationToken> CreateAsync(string username, string password)
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Post, client.GetApiUrl(true, this.ModuleRelativePath))
                {
                    Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password)
                    })
                };

                var response = await client.SendAsync(request);
                var token = this.ReadToken(JsonFormatter.Deserialize<Newtonsoft.Json.Linq.JObject>(await response.Content.ReadAsStringAsync()));

                var tokenHandler = this.Configuration.TokenHandler;
                if (tokenHandler != null)
                {
                    tokenHandler.Save(token);
                }

                return token;
            }
        }

        /// <summary>
        /// Asynchronously destroys the <see cref="IAuthenticationToken" />.
        /// </summary>
        /// <param name="token">Token to destroy.</param>
        /// <returns>
        /// True if <see cref="IAuthenticationToken" /> is destoyed, false otherwise.
        /// </returns>
        public async Task<bool> DestroyAsync()
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                var token = this.Configuration.TokenHandler.Get();

                if (token != null)
                {
                    var request = new HttpRequestMessage(HttpMethod.Delete, client.GetApiUrl(true, this.ModuleRelativePath))
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

        /// <summary>
        /// Asynchronously refreshes the <see cref="IAuthenticationToken" />.
        /// </summary>
        /// <param name="token">Token to update.</param>
        /// <returns>New <see cref="IAuthenticationToken" />.</returns>
        public async Task<IAuthenticationToken> RefreshAsync()
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                var oldToken = this.Configuration.TokenHandler.Get();

                var newToken = await client.PutAsync<Newtonsoft.Json.Linq.JObject>(client.GetApiUrl(true, this.ModuleRelativePath), Newtonsoft.Json.Linq.JObject.FromObject(new
                {
                    token = oldToken.Token,
                    type = oldToken.Scheme
                }));

                var token = this.ReadToken(newToken);

                var tokenHandler = this.Configuration.TokenHandler;
                if (tokenHandler != null)
                {
                    tokenHandler.Save(token);
                }

                return token;
            }
        }

        #endregion Methods

        private IAuthenticationToken ReadToken(Newtonsoft.Json.Linq.JObject rawToken)
        {
            return new AuthenticationToken()
            {
                ExpirationDate = DateTime.UtcNow.AddSeconds(rawToken.Property("expires_in").ToObject<long>()),
                Scheme = rawToken.Property("token_type").ToObject<string>(),
                Token = rawToken.Property("access_token").ToObject<string>(),
            };
        }
    }
}