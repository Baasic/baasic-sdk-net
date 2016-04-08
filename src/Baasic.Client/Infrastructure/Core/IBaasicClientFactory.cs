using Baasic.Client.Configuration;

using System;

namespace Baasic.Client.Core
{
    /// <summary>
    /// <see cref="IBaasicClient" /> factory.
    /// </summary>
    public interface IBaasicClientFactory
    {
        #region Methods

        /// <summary>
        /// Creates the specified Baasic client.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns><see cref="IBaasicClient" /> instance.</returns>
        IBaasicClient Create(IClientConfiguration configuration);

        #endregion Methods
    }
}