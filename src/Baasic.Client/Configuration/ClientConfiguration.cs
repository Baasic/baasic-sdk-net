using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Baasic.Client.Configuration
{
    /// <summary>
    /// OWIN application start.
    /// </summary>
    public class AppStart
    {
        /// <summary>
        /// Application configuration.
        /// </summary>
        /// <param name="app">Application builder instance.</param>
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver = new NinjectSignalRDependencyResolver(System.Net.Http.GlobalConfiguration.Configuration.DependencyResolver);

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }

        /// <summary>
        /// Ninject SignalR dependency resolver.
        /// </summary>
        private class NinjectSignalRDependencyResolver : DefaultDependencyResolver
        {
            private readonly System.Web.Http.Dependencies.IDependencyResolver _resolver;

            public NinjectSignalRDependencyResolver(System.Web.Http.Dependencies.IDependencyResolver resolver)
            {
                _resolver = resolver;
            }

            public override object GetService(Type serviceType)
            {
                return _resolver.GetService(serviceType) ?? base.GetService(serviceType);
            }

            public override IEnumerable<object> GetServices(Type serviceType)
            {
                return _resolver.GetServices(serviceType).Concat(base.GetServices(serviceType));
            }
        }
    }

    /// <summary>
    /// Client configuration.
    /// </summary>
    public class ClientConfiguration : IClientConfiguration
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
                    _baseAddress = BaasicBaseAddress + BaasicVersion;
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
            set
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
            set
            {
                _secureBaseAddress = value;
            }
        }

        /// <summary>
        /// Gets or sets the serializer settings.
        /// </summary>
        public JsonSerializerSettings SerializerSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration" /> class.
        /// </summary>
        /// <param name="applicationIdentifier">The application identifier.</param>
        public ClientConfiguration(string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientConfiguration" /> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="applicationIdentifier">The application identifier.</param>
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