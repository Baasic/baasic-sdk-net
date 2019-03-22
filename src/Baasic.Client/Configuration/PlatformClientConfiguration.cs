using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.Security;

namespace Baasic.Client.Configuration
{
    /// <summary>
    /// Platform client configuration
    /// </summary>
    /// <seealso cref="Baasic.Client.Configuration.ClientConfiguration" />
    /// <seealso cref="Baasic.Client.Common.Configuration.IPlatformClientConfiguration" />
    public class PlatformClientConfiguration : ClientConfiguration, IPlatformClientConfiguration
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClientConfiguration" /> class.
        /// </summary>
        /// <param name="tokenHandler">The token handler.</param>
        public PlatformClientConfiguration(IPlatformTokenHandler tokenHandler)
            : base("platform", tokenHandler)
        {
            TokenHandler = tokenHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClientConfiguration" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="applicationIdentifier">The application identifier.</param>
        /// <param name="tokenHandler">The token handler.</param>
        public PlatformClientConfiguration(string baseAddress, IPlatformTokenHandler tokenHandler)
            : base(baseAddress, "platform", tokenHandler)
        {
            TokenHandler = tokenHandler;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Platform token handler.
        /// </summary>
        public new IPlatformTokenHandler TokenHandler { get; private set; }

        #endregion Properties
    }
}