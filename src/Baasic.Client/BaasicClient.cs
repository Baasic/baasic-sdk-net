using Baasic.Client.Configuration;
using Newtonsoft.Json;
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
    public class BaasicClient : IDisposable
    {
        #region Properties
        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        public IClientConfiguration Configuration { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Client configuration.</param>
        public BaasicClient(IClientConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        #region Methods
        public virtual Task<bool> DeleteAsync(string requestUri)
        {
            return null;
        }

        public virtual Task<bool> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        { return null; }

        public virtual async Task<T> GetAsync<T>(string requestUri)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();
                //TODO: Add HAL Converter
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }
        
        public virtual async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.GetAsync(requestUri, cancellationToken);
                response.EnsureSuccessStatusCode();
                //TODO: Add HAL Converter
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        public Task<T> PostAsync<T>(string requestUri, HttpContent content)
        { return null; }


        public Task<T> PostAsync<T>(string requestUri, HttpContent content, CancellationToken cancellationToken)
        { return null; }

        public Task<T> PutAsync<T>(string requestUri, HttpContent content)
        { return null; }


        public Task<T> PutAsync<T>(string requestUri, HttpContent content, CancellationToken cancellationToken)
        {
            return null;
        }

        protected virtual void InitializeClient(HttpClient client, string mthv)
        {
            client.Timeout = Configuration.DefaultTimeout;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            //TODO: Add authentication header
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
            return GetApiUrl(false, Configuration.ApplicationIdentifier, relativeUrl, parameters);
        }

        public string GetSecureApiUrl(string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(true, Configuration.ApplicationIdentifier, relativeUrl, parameters);
        }

        private string GetApiUrl(bool ssl, string applicationIdentifier, string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(ssl, applicationIdentifier.TrimEnd('/') + "/" + relativeUrl, parameters);
        }

        public string GetApiUrl(bool ssl, string relativeUrl, params object[] parameters)
        {
            return String.Format("{0}/{1}", ssl ? Configuration.SecureBaseAddress.TrimEnd('/') : Configuration.BaseAddress.TrimEnd('/'), String.Format(relativeUrl, parameters));
        }

        public void Dispose()
        {

        }
        #endregion

    }
}
