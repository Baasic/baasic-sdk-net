using Baasic.Client.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Client configuration.</param>
        public BaasicClient(IClientConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Create string content.
        /// </summary>
        /// <param name="data">Data.</param>
        /// <param name="mthv">Media type.</param>
        /// <returns>String content.</returns>
        public StringContent CreateStringContent(string data, string mthv)
        {
            var result = new StringContent(data, Configuration.DefaultEncoding, mthv);
            //TODO: Add authentication header
            //result.Headers
            return result;
        }

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync(string requestUri)
        {
            return DeleteAsync(requestUri, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync(string requestUri, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.DeleteAsync(requestUri, cancellationToken);
                response.EnsureSuccessStatusCode();
                return response.StatusCode.Equals(HttpStatusCode.OK);
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
        }

        public string GetApiUrl(string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(false, Configuration.ApplicationIdentifier, relativeUrl, parameters);
        }

        public string GetApiUrl(bool ssl, string relativeUrl, params object[] parameters)
        {
            return String.Format("{0}/{1}", ssl ? Configuration.SecureBaseAddress.TrimEnd('/') : Configuration.BaseAddress.TrimEnd('/'), String.Format(relativeUrl, parameters));
        }

        /// <summary>
        /// Asynchronously gets the <typeparamref name="T"/> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <returns><typeparamref name="T"/>.</returns>
        public virtual Task<T> GetAsync<T>(string requestUri)
        {
            return GetAsync<T>(requestUri, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously gets the <typeparamref name="T"/> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><typeparamref name="T"/>.</returns>
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

        public string GetSecureApiUrl(string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(true, Configuration.ApplicationIdentifier, relativeUrl, parameters);
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T"/> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T"/>.</returns>
        public virtual Task<T> PostAsync<T>(string requestUri, T content)
        {
            return PostAsync<T>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T"/> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="T"/>.</returns>
        public virtual async Task<T> PostAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.PostAsync(requestUri, CreateStringContent(await JsonConvert.SerializeObjectAsync(content), Configuration.DefaultMediaType), cancellationToken);
                response.EnsureSuccessStatusCode();
                //TODO: Add HAL Converter
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), Configuration.SerializerSettings);
            }
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="T"/> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <typeparamref name="T"/>.</returns>
        public virtual Task<T> PutAsync<T>(string requestUri, T content)
        {
            return PutAsync<T>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="T"/> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated <typeparamref name="T"/>.</returns>
        public virtual async Task<T> PutAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.PutAsync(requestUri, CreateStringContent(await JsonConvert.SerializeObjectAsync(content), Configuration.DefaultMediaType), cancellationToken);
                response.EnsureSuccessStatusCode();
                //TODO: Add HAL Converter
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync(), Configuration.SerializerSettings);
            }
        }

        /// <summary>
        /// Initialize HTTP client instance.
        /// </summary>
        /// <param name="client">HTTP client.</param>
        /// <param name="mthv">Media type header value.</param>
        protected virtual void InitializeClient(HttpClient client, string mthv)
        {
            client.Timeout = Configuration.DefaultTimeout;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            //TODO: Add authentication header
        }

        private string GetApiUrl(bool ssl, string applicationIdentifier, string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(ssl, applicationIdentifier.TrimEnd('/') + "/" + relativeUrl, parameters);
        }

        #endregion Methods
    }
}