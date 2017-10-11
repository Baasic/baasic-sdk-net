using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Configuration;
using Baasic.Client.Infrastructure.DependencyInjection;
using System;
using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Core
{
    /// <summary>
    /// <see cref="IBaasicClient" /> factory.
    /// </summary>
    public class BaasicClientFactory : IBaasicClientFactory
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientFactory" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public BaasicClientFactory(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the dependency resolver.
        /// </summary>
        /// <value>The dependency resolver.</value>
        private IDependencyResolver DependencyResolver { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates the specified Baasic client.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns><see cref="IBaasicClient" /> instance.</returns>
        public virtual IBaasicClient Create(IClientConfiguration configuration)
        {
            IBaasicClient client = DependencyResolver.GetService<IBaasicClient>();
            client.Configuration = configuration;
            return client;
        }

        #endregion Methods
    }
}