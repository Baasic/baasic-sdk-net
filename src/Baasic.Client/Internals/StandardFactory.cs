using Baasic.Client.Configuration;
using System;

namespace Baasic.Client.Internals
{
    /// <summary>
    /// Factory used to instantiate objects.
    /// </summary>
    public class StandardFactory : IFactory
    {
        /// <summary>
        /// Creates the baasic client.
        /// </summary>
        /// <returns>Baasic client.</returns>
        public virtual IBaasicClient CreateBaasicClient()
        {
            return new BaasicClient(this);
        }

        /// <summary>
        /// Creates the client configuration.
        /// </summary>
        /// <returns>Client configuration.</returns>
        public virtual IClientConfiguration CreateClientConfiguration()
        {
            return new ClientConfiguration("");
        }

        /// <summary>
        /// Create client configuration.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        /// <returns>Client configuration.</returns>
        public virtual IClientConfiguration CreateClientConfiguration(string applicationIdentifier)
        {
            return new ClientConfiguration(applicationIdentifier);
        }

        /// <summary>
        /// Create client configuration.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        /// <returns>Client configuration.</returns>
        public virtual IClientConfiguration CreateClientConfiguration(string baseAddress, string applicationIdentifier)
        {
            return new ClientConfiguration(baseAddress, applicationIdentifier);
        }
    }
}