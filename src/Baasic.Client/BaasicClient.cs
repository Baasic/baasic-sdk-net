using Baasic.Client.Configuration;
using Baasic.Client.Formatters;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Baasic.Client
{
    /// <summary>
    /// Baasic client.
    /// </summary>
    public class BaasicClient : IBaasicClient
    {
        #region Properties

        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        public IClientConfiguration Configuration
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the HTTP client factory.
        /// </summary>
        /// <value>The HTTP client factory.</value>
        protected IHttpClientFactory HttpClientFactory
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the default serializer.
        /// </summary>
        /// <value>Default serializer.</value>
        protected virtual IJsonFormatter JsonFormatter
        {
            get;
            private set;
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClient"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="jsonFormatter">JSON formatter.</param>
        public BaasicClient(IClientConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IJsonFormatter jsonFormatter
            )
        {
            this.Configuration = configuration;
            this.HttpClientFactory = httpClientFactory;
            this.JsonFormatter = jsonFormatter;
        }

        #endregion Constructor

        #region Methods

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
            using (HttpClient client = HttpClientFactory.Create())
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

        /// <summary>
        /// Gets the API URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public string GetApiUrl(string relativeUrl, params object[] parameters)
        {
            return GetApiUrl(false, relativeUrl, parameters);
        }

        /// <summary>
        /// Gets the API URL.
        /// </summary>
        /// <param name="ssl">if set to <c>true</c> [SSL].</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public string GetApiUrl(bool ssl, string relativeUrl, params object[] parameters)
        {
            return String.Format("{0}/{1}", ssl ? Configuration.SecureBaseAddress.TrimEnd('/') : Configuration.BaseAddress.TrimEnd('/'), String.Format(this.Configuration.ApplicationIdentifier.TrimEnd('/') + "/" + relativeUrl, parameters));
        }

        /// <summary>
        /// Asynchronously gets the <typeparamref name="T"/> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <returns><typeparamref name="T"/> .</returns>
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
        /// <returns><typeparamref name="T"/> .</returns>
        public virtual async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            using (HttpClient client = HttpClientFactory.Create())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.GetAsync(requestUri, cancellationToken);
                response.EnsureSuccessStatusCode();
                //TODO: Add HAL Converter

                return await ReadContentAsync<T>(response);
            }
        }

        /// <summary>
        /// Gets the secure API URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
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
        /// <returns>Newly created <typeparamref name="T"/> .</returns>
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
        /// <returns>Newly created <typeparamref name="T"/> .</returns>
        public virtual async Task<T> PostAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            using (HttpClient client = HttpClientFactory.Create())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.PostAsync(requestUri, JsonFormatter.SerializeToHttpContent(content), cancellationToken);
                response.EnsureSuccessStatusCode();

                //TODO: Add HAL Converter
                return await ReadContentAsync<T>(response);
            }
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="T"/> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <typeparamref name="T"/> .</returns>
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
        /// <returns>Updated <typeparamref name="T"/> .</returns>
        public virtual async Task<T> PutAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            using (HttpClient client = HttpClientFactory.Create())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.PutAsync(requestUri, JsonFormatter.SerializeToHttpContent(content), cancellationToken);
                response.EnsureSuccessStatusCode();

                return await ReadContentAsync<T>(response);
            }
        }

        /// <summary>
        /// Returns deserialized content from response.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="response">HTTP response.</param>
        /// <returns><typeparamref name="T"/> Resource.</returns>
        public virtual async Task<T> ReadContentAsync<T>(HttpResponseMessage response)
        {
            if (response.Content != null && response.Content.Headers.ContentLength > 0)
            {
                return JsonFormatter.Deserialize<T>(await response.Content.ReadAsStreamAsync());
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Asynchronously sends HTTP request.
        /// </summary>
        /// <typeparam name="request">HTTP request.</typeparam>
        /// <returns>HTTP response message.</returns>
        public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return this.SendAsync(request, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously sends HTTP request.
        /// </summary>
        /// <typeparam name="request">HTTP request.</typeparam>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>HTTP response message.</returns>
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (HttpClient client = HttpClientFactory.Create())
            {
                InitializeClient(client, Configuration.DefaultMediaType);

                var response = await client.SendAsync(request, cancellationToken);
                response.EnsureSuccessStatusCode();
                //TODO: Add HAL Converter
                return response;
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
            var tokenHandler = this.Configuration.TokenHandler;
            if (tokenHandler != null)
            {
                var token = tokenHandler.Get();
                if (token != null && token.IsValid)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.Scheme, token.Token);
                }
            }
        }

        #endregion Methods
    }
}