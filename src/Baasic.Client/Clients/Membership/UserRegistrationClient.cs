using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model.Membership;
using Baasic.Client.Model.Security;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership module user registration client.
    /// </summary>
    public class UserRegistrationClient : ClientBase, IUserRegistrationClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRegistrationClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        /// <param name="jsonFormatter">The json formatter.</param>
        public UserRegistrationClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory,
            IJsonFormatter jsonFormatter)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
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
        /// Gets the default serializer.
        /// </summary>
        /// <value>Default serializer.</value>
        protected virtual IJsonFormatter JsonFormatter
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the module relative path.
        /// </summary>
        protected override string ModuleRelativePath
        {
            get { return "register"; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously activates the <see cref="User" /> using activation token.
        /// </summary>
        /// <param name="activationToken">The activation token.</param>
        /// <param name="tokenOptions">The token options.</param>
        /// <returns><see cref="IAuthenticationToken" />.</returns>
        public virtual async Task<IAuthenticationToken> ActivateAsync(string activationToken, TokenOptions tokenOptions = null)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                if (tokenOptions == null)
                {
                    tokenOptions = new TokenOptions();
                }

                var response = await client.PutAsync<Newtonsoft.Json.Linq.JObject>(client.GetApiUrl(String.Format("{0}/activate/{1}?{2}", ModuleRelativePath, activationToken, tokenOptions.GetOptionsCSV())), null);

                var token = this.ReadToken(response);

                var tokenHandler = this.Configuration.TokenHandler;
                if (tokenHandler != null)
                {
                    tokenHandler.Save(token);
                }

                return token;
            }
        }

        /// <summary>
        /// Asynchronously register new <see cref="User" /> using <see cref="CreateUserDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Newly registered <see cref="User" />.</returns>
        public virtual Task<User> RegisterAsync(CreateUserDTO options)
        {
            return RegisterAsync<CreateUserDTO, User>(options);
        }

        /// <summary>
        /// Asynchronously register new <see cref="User" /> using <see cref="CreateUserDTO" /> options.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>Newly registered <typeparamref name="TOut" />.</returns>
        public virtual async Task<TOut> RegisterAsync<TIn, TOut>(TIn options)
            where TIn : CreateUserDTO
            where TOut : User
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Post, client.GetApiUrl(true, this.ModuleRelativePath))
                {
                    Content = JsonFormatter.SerializeToHttpContent(options)
                };

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return JsonFormatter.Deserialize<TOut>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new InvalidOperationException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        private IAuthenticationToken ReadToken(Newtonsoft.Json.Linq.JObject rawToken)
        {
            var error = rawToken.Property("error");
            if (error != null)
            {
                throw new InvalidOperationException(rawToken.Property("error_description").ToString());
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

        #endregion Methods
    }
}