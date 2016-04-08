using Baasic.Client.Infrastructure.Security;
using System;
using System.Text;

namespace Baasic.Client.Configuration
{
    /// <summary>
    /// Client configuration.
    /// </summary>
    public abstract class ClientConfiguration : IClientConfiguration
    {
        #region Fields

        /// <summary>
        /// Baasic base address.
        /// </summary>
        public const string BaasicBaseAddress = "http://api.baasic.com/";

        /// <summary>
        /// The baasic version.
        /// </summary>
        public const string BaasicVersion = "v1";

        /// <summary>
        /// HAL+JSON media type.
        /// </summary>
        public const string HalJsonMediaType = "application/hal+json";

        /// <summary>
        /// JSON media type.
        /// </summary>
        public const string JsonMediaType = "application/json";

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; private set; }

        /// <summary>
        /// Gets or sets server base address.
        /// </summary>
        public string BaseAddress
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_baseAddress))
                    _baseAddress = BaasicBaseAddress + BaasicVersion;
                return _baseAddress;
            }
            private set
            {
                _baseAddress = value;
                _secureBaseAddress = null;
            }
        }

        /// <summary>
        /// Gets or sets default encoding.
        /// </summary>
        public Encoding DefaultEncoding
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets default media type.
        /// </summary>
        public string DefaultMediaType
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_defaultMediaType))
                    return HalJsonMediaType;
                return _defaultMediaType;
            }
            private set
            {
                _defaultMediaType = value;
            }
        }

        /// <summary>
        /// Gets or sets client default timeout period.
        /// </summary>
        public TimeSpan DefaultTimeout
        {
            get
            {
                return _defaultTimeout;
            }
            private set
            {
                _defaultTimeout = value;
            }
        }

        /// <summary>
        /// Gets or sets server secure base address.
        /// </summary>
        public string SecureBaseAddress
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_secureBaseAddress))
                    _secureBaseAddress = BaseAddress.Replace("http://", "https://");
                return _secureBaseAddress;
            }
            private set
            {
                _secureBaseAddress = value;
            }
        }

        /// <summary>
        /// Token handler.
        /// </summary>
        public ITokenHandler TokenHandler
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; private set; }

        private string _baseAddress;

        private string _defaultMediaType;

        private TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10);

        private string _secureBaseAddress;

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration" /> class.
        /// </summary>
        /// <param name="applicationIdentifier">The application identifier.</param>
        /// <param name="tokenHandler">The token handler.</param>
        public ClientConfiguration(string applicationIdentifier, ITokenHandler tokenHandler)
        {
            ApplicationIdentifier = applicationIdentifier;
            TokenHandler = tokenHandler;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="applicationIdentifier">The application identifier.</param>
        /// <param name="tokenHandler">The token handler.</param>
        public ClientConfiguration(string baseAddress, string applicationIdentifier, ITokenHandler tokenHandler)
        {
            BaseAddress = baseAddress;
            ApplicationIdentifier = applicationIdentifier;
            TokenHandler = tokenHandler;
            Initialize();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Initialize client configuration.
        /// </summary>
        protected virtual void Initialize()
        {
            this.DefaultEncoding = Encoding.UTF8;
        }

        #endregion Methods
    }
}