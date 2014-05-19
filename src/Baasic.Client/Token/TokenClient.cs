using Baasic.Client.Configuration;
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
        public TokenClient(
            IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory
            )
            : base(configuration)
        {
            this.BaasicClientFactory = baasicClientFactory;
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
                var request = new HttpRequestMessage(HttpMethod.Post, client.GetApiUrl(this.ModuleRelativePath))
                {
                    Content = new FormUrlEncodedContent(new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("username", password)
                    })
                };

                var response = await client.SendAsync(request);

                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(await response.Content.ReadAsStringAsync());

                var token = new AuthenticationToken()
                {
                    ExpirationDate = DateTime.UtcNow.AddSeconds(obj.Property("expires_in").ToObject<long>()),
                    Scheme = obj.Property("token_type").ToObject<string>(),
                    Token = obj.Property("access_token").ToObject<string>(),
                };

                this.Configuration.TokenHandler.Save(token);

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

                await client.DeleteAsync(client.GetApiUrl(this.ModuleRelativePath));

                return true;
            }
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

                var newToken = await client.PutAsync<Newtonsoft.Json.Linq.JObject>(client.GetApiUrl(this.ModuleRelativePath), new Newtonsoft.Json.Linq.JObject(new
                {
                    Token = oldToken.Token
                }));

                var token = new AuthenticationToken()
                {
                    ExpirationDate = DateTime.UtcNow.AddSeconds(newToken.Property("expires_in").ToObject<long>()),
                    Scheme = newToken.Property("token_type").ToObject<string>(),
                    Token = newToken.Property("access_token").ToObject<string>(),
                };

                this.Configuration.TokenHandler.Save(token);

                return token;
            }
        }

        #endregion Methods
    }
}