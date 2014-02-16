using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baasic.Client
{
    /// <summary>
    /// Baasic.com client.
    /// </summary>
    public class BaasicClient
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
        
        public const string BaasicBaseAddress = "http://api.baasic.com/v1";
        public TimeSpan BaasicrDefaultTimeout = TimeSpan.FromSeconds(10);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; protected set; }

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
        public BaasicClient(string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="baseAddress">Baasic API address.</param>
        /// <param name="applicationIdentifier">Application identifier.</param>
        public BaasicClient(string baseAddress, string applicationIdentifier)
        {
            ApplicationIdentifier = applicationIdentifier;
            BaseAddress = baseAddress;
        } 
        #endregion

        #region Methods
        public Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return null;
        }

        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri)
        { return null; }
        
        public Task<HttpResponseMessage> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        { return null; }
        
        public Task<HttpResponseMessage> DeleteAsync(Uri requestUri, CancellationToken cancellationToken)
        { return null; }

        public Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            using (HttpClient client = new HttpClient())
            {
                return client.SendAsync(CreateRequest(requestUri, DefaultMediaType, HttpMethod.Get));
            }
        }
        
        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        { return null; }
        
        public Task<HttpResponseMessage> GetAsync(string requestUri, CancellationToken cancellationToken)
        { return null; }
        
        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption)
        { return null; }
        
        public Task<HttpResponseMessage> GetAsync(Uri requestUri, CancellationToken cancellationToken)
        { return null; }
        
        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption)
        { return null; }

        public Task<HttpResponseMessage> GetAsync(string requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        { return null; }
        
        public Task<HttpResponseMessage> GetAsync(Uri requestUri, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        { return null; }

        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content)
        { return null; }
        
        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content)
        { return null; }
        
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        { return null; }
        
        public Task<HttpResponseMessage> PostAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        { return null; }
        
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content)
        { return null; }
        
        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content)
        { return null; }
        
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return null;
        }
        
        public Task<HttpResponseMessage> PutAsync(Uri requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return null;
        }

        public HttpRequestMessage CreateRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url)
            };

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            //TODO: Add authentication header
            request.Method = method;

            return request;
        }

        public StringContent CreateStringContent(string data, string mthv)
        {
            var result = new StringContent(data, Encoding.UTF8, mthv);
            //TODO: Add authentication header
            //result.Headers
            return result;
        }

        public string GetApiUrl(string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(false, ApplicationIdentifier, relativeUrl, parameters);
        }

        public string GetSecureApiUrl(string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(true, ApplicationIdentifier, relativeUrl, parameters);
        }

        private string GetApiUrl(bool ssl, string applicationIdentifier, string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(ssl, applicationIdentifier.TrimEnd('/') + "/" + relativeUrl, parameters);
        }

        public string GetApiUrl(bool ssl, string relativeUrl, params object[] parameters)
        {
            return String.Format("{0}/{1}", ssl ? SecureBaseAddress.TrimEnd('/') : BaseAddress.TrimEnd('/'), String.Format(relativeUrl, parameters));
        }

        #endregion
    }
}
