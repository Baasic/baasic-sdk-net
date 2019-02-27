using Baasic.Client.Common.Configuration;

namespace Baasic.Client.Core
{
    /// <summary>
    /// <see cref="IBaasicPlatformClient" /> factory.
    /// </summary>
    public interface IBaasicPlatformClientFactory
    {
        #region Methods

        /// <summary>
        /// Creates the specified Baasic platform client.
        /// </summary>
        /// <param name="clientConfiguration">The client configuration.</param>
        /// <returns><see cref="IBaasicPlatformClient" /> instance.</returns>
        IBaasicPlatformClient Create(IPlatformClientConfiguration clientConfiguration);

        #endregion Methods
    }
}