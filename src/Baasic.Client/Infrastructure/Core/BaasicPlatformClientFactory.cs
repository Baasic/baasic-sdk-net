using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.DependencyInjection;

namespace Baasic.Client.Core
{
    /// <summary>
    /// <see cref="IBaasicPlatformClient" /> factory.
    /// </summary>
    /// <seealso cref="Baasic.Client.Core.IBaasicPlatformClientFactory" />
    public class BaasicPlatformClientFactory : IBaasicPlatformClientFactory
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicPlatformClientFactory" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public BaasicPlatformClientFactory(IDependencyResolver dependencyResolver)
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
        /// Creates the specified Baasic platform client.
        /// </summary>
        /// <param name="clientConfiguration">The client configuration.</param>
        /// <returns><see cref="IBaasicPlatformClient" /> instance.</returns>
        public IBaasicPlatformClient Create(IPlatformClientConfiguration clientConfiguration)
        {
            IBaasicPlatformClient platformClient = DependencyResolver.GetService<IBaasicPlatformClient>();
            platformClient.Configuration = clientConfiguration;
            return platformClient;
        }

        #endregion Methods
    }
}