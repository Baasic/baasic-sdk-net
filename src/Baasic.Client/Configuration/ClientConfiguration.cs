using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baasic.Client.Configuration
{
    public class ClientConfiguration : IClientConfiguration
    {
        #region Fields

        /// <summary>
        /// Baasic base address.
        /// </summary>
        public const string BaasicBaseAddress = "http://api.baasic.com/v1";

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

        private string _baseAddress;

        private string _defaultMediaType;

        private TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10);

        private string _secureBaseAddress;

        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; set; }

        /// <summary>
        /// Gets or sets server base address.
        /// </summary>
        public string BaseAddress
        {
            get
            {
                if (String.IsNullOrWhiteSpace(_baseAddress))
                    _baseAddress = BaasicBaseAddress;
                return _baseAddress;
            }
            set
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
            set;
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
            set { _defaultMediaType = value; }
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
            set
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
            set { _secureBaseAddress = value; }
        }

        /// <summary>
        /// Gets or sets the serializer settings.
        /// </summary>
        public JsonSerializerSettings SerializerSettings
        {
            get;
            set;
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public ClientConfiguration(string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
            Initialize();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseAddress">Baasic API address.</param>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public ClientConfiguration(string baseAddress, string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
            BaseAddress = baseAddress;
            Initialize();
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Initialize client configuration.
        /// </summary>
        protected virtual void Initialize()
        {
            SerializerSettings = new JsonSerializerSettings();
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            SerializerSettings.Converters.Add(new IsoDateTimeConverter());
            SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            SerializerSettings.NullValueHandling = NullValueHandling.Include;
            SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            DefaultEncoding = Encoding.UTF8;
        }

        #endregion Methods
    }
}