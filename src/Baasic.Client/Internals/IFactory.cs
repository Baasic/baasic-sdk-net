using Baasic.Client.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Internals
{
    /// <summary>
    /// Factory used to instantiate objects.
    /// </summary>
    public interface IFactory
    {
        /// <summary>
        /// Create client configuration.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        IClientConfiguration CreateClientConfiguration(string applicationIdentifier);

        /// <summary>
        /// Create client configuration.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        IClientConfiguration CreateClientConfiguration(string baseAddress, string applicationIdentifier);
    }
}