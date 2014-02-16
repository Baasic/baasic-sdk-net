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
        public const string BaasicSSLBaseAddress = "https://api.baasic.com/v1";
        public TimeSpan BaasicrDefaultTimeout = TimeSpan.FromSeconds(10);
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the application identifier.
        /// </summary>
        public string ApplicationIdentifier { get; protected set; }
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
        { return null; }
        
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
        #endregion
    }
}
