using Baasic.Client.Configuration;
using Baasic.Client.Core;
using Baasic.Client.Formatters;
using Baasic.Client.Model.Membership;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership module user password recovery client.
    /// </summary>
    public class UserPasswordRecoveryClient : ClientBase, IUserPasswordRecoveryClient
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPasswordRecoveryClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="baasicClientFactory">The baasic client factory.</param>
        /// <param name="jsonFormatter">The json formatter.</param>
        public UserPasswordRecoveryClient(IClientConfiguration configuration,
            IBaasicClientFactory baasicClientFactory,
            IJsonFormatter jsonFormatter)
            : base(configuration)
        {
            BaasicClientFactory = baasicClientFactory;
            this.JsonFormatter = jsonFormatter;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Asynchronously recovery password using <see cref="PasswordRecoveryDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>True if password is recovered, false otherwise.</returns>
        public virtual async Task<bool> PasswordRecoveryAsync(PasswordRecoveryDTO options)
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Put, client.GetApiUrl(true, this.ModuleRelativePath))
                {
                    Content = JsonFormatter.SerializeToHttpContent(options)
                };

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously request a new password recovery using <see cref="PasswordRecoveryRequestDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>True if password recovery request is valid, false otherwise.</returns>
        public virtual async Task<bool> RequestPasswordRecoveryAsync(PasswordRecoveryRequestDTO options)
        {
            using (var client = this.BaasicClientFactory.Create(this.Configuration))
            {
                var request = new HttpRequestMessage(HttpMethod.Post, client.GetApiUrl(true, this.ModuleRelativePath))
                {
                    Content = JsonFormatter.SerializeToHttpContent(options)
                };

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

        #endregion Methods

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
            get { return "recover-password"; }
        }

        #endregion Properties
    }
}