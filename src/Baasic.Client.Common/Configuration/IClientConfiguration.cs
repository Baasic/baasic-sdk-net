using Baasic.Client.Common.Infrastructure.Security;
using System;
using System.Text;

namespace Baasic.Client.Common.Configuration
{
    /// <summary>
    /// Client configuration.
    /// </summary>
    public interface IClientConfiguration
    {
        #region Properties

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        string ApplicationIdentifier
        {
            get;
        }

        /// <summary>
        /// Gets or sets server base address.
        /// </summary>
        string BaseAddress
        {
            get;
        }

        /// <summary>
        /// Gets or sets default encoding.
        /// </summary>
        Encoding DefaultEncoding
        {
            get;
        }

        /// <summary>
        /// Gets or sets default media type.
        /// </summary>
        string DefaultMediaType
        {
            get;
        }

        /// <summary>
        /// Gets or sets client default timeout period.
        /// </summary>
        TimeSpan DefaultTimeout
        {
            get;
        }

        /// <summary>
        /// Gets or sets server secure base address.
        /// </summary>
        string SecureBaseAddress
        {
            get;
        }

        /// <summary>
        /// Token handler.
        /// </summary>
        ITokenHandler TokenHandler
        {
            get;
        }

        /// <summary>
        /// Gets the use ssl.
        /// </summary>
        bool UseSsl
        {
            get;
        }

        #endregion Properties
    }
}