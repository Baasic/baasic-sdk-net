using Baasic.Client.Common.Configuration;
using Baasic.Client.Common.Infrastructure.Security;

namespace Baasic.Client.Configuration
{
    public class PlatformClientConfiguration : ClientConfiguration, IPlatformClientConfiguration
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClientConfiguration" /> class.
        /// </summary>
        /// <param name="tokenHandler">The token handler.</param>
        public PlatformClientConfiguration(ITokenHandler tokenHandler)
            : base("platform", tokenHandler)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlatformClientConfiguration" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="tokenHandler">The token handler.</param>
        public PlatformClientConfiguration(string baseAddress, ITokenHandler tokenHandler)
            : base(baseAddress, "platform", tokenHandler)
        {
        }

        #endregion Constructors
    }
}