using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model.Membership;
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
        /// <returns><see cref="IAuthenticationToken" />.</returns>
        public virtual async Task<IAuthenticationToken> ActivateAsync(string activationToken)
        {
            using (IBaasicClient client = BaasicClientFactory.Create(Configuration))
            {
                var token = await client.PutAsync<AuthenticationToken>(client.GetApiUrl(String.Format("{0}/activate/{1}", ModuleRelativePath, activationToken)), null);
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
        public virtual async Task<User> RegisterAsync(CreateUserDTO options)
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
                    return JsonFormatter.Deserialize<User>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new InvalidOperationException(await response.Content.ReadAsStringAsync());
                }
            }
        }

        #endregion Methods
    }
}