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
        /// JSON media type.
        /// </summary>
        public const string JsonMediaType = "application/json";
        /// <summary>
        /// HAL+JSON media type.
        /// </summary>
        public const string HalJsonMediaType = "application/hal+json";
        /// <summary>
        /// Baasic base address.
        /// </summary>
        public const string BaasicBaseAddress = "http://api.baasic.com/v1";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; set; }

        private TimeSpan _defaultTimeout = TimeSpan.FromSeconds(10);
        /// <summary>
        /// Baasic client default timeout period.
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

        private string _baseAddress;

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

        private string _secureBaseAddress;

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

        private string _defaultMediaType;

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


        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public ClientConfiguration(string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
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
        } 
        #endregion


        
    }
}
