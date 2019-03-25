using Baasic.Client.Common;
using Baasic.Client.Common.Configuration;
using Baasic.Client.Formatters;
using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Baasic.Client.Core
{
    /// <summary>
    /// Baasic client.
    /// </summary>
    public class BaasicClient : IBaasicClient
    {
        #region Fields

        private Lazy<HttpClient> _client;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClient" /> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <param name="jsonFormatter">JSON formatter.</param>
        public BaasicClient(IClientConfiguration configuration,
            IHttpClientFactory httpClientFactory,
            IJsonFormatter jsonFormatter
            )
        {
            Configuration = configuration;
            HttpClientFactory = httpClientFactory;
            JsonFormatter = jsonFormatter;
            this._client = new Lazy<HttpClient>(() =>
            {
                var client = HttpClientFactory.Create();
                InitializeClient(client, Configuration.DefaultMediaType);
                return client;
            }, true);
        }

        #endregion Constructors

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
        /// Gets or sets the client.
        /// </summary>
        /// <value>The client.</value>
        protected HttpClient Client
        {
            get
            {
                return this._client.Value;
            }
            set
            {
                this._client = new Lazy<HttpClient>(() => value);
            }
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
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(requestUri, response);
                }

                this.ProlongSlidingToken();

                return response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        public virtual Task<bool> DeleteAsync<T>(string requestUri, T content)
        {
            return DeleteAsync<T>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        public virtual async Task<bool> DeleteAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, requestUri)
                {
                    Content = JsonFormatter.SerializeToHttpContent(content)
                };
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(new { Uri = requestUri, Content = content }, response);
                }

                this.ProlongSlidingToken();

                return response.IsSuccessStatusCode;
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
            return GetApiUrl(Configuration.UseSsl, relativeUrl, parameters);
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
        /// Asynchronously gets the <typeparamref name="T" /> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <returns><typeparamref name="T" /> .</returns>
        public virtual Task<T> GetAsync<T>(string requestUri)
        {
            return GetAsync<T>(requestUri, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously gets the <typeparamref name="T" /> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><typeparamref name="T" /> .</returns>
        public virtual async Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return default(T);

                        default:
                            break;
                    }
                    await ThrowExceptionAsync(requestUri, response);
                }

                this.ProlongSlidingToken();

                return await ReadContentAsync<T>(response);
            }
        }

        /// <summary>
        /// Asynchronously gets the response as byte array from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <returns>Response as byte array.</returns>
        public virtual Task<Stream> GetAsync(string requestUri)
        {
            return GetAsync(requestUri, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously gets the response as byte array from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response as byte array.</returns>
        public virtual async Task<Stream> GetAsync(string requestUri, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return default(Stream);

                        default:
                            break;
                    }
                    await ThrowExceptionAsync(requestUri, response);
                }

                this.ProlongSlidingToken();

                return await ReadContentAsync(response);
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
            return GetApiUrl(true, relativeUrl, parameters);
        }

        /// <summary>
        /// Patches the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public Task<bool> PatchAsync<T>(string requestUri, T content)
        {
            return PatchAsync<T>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Patches the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<bool> PatchAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), requestUri)
                {
                    Content = JsonFormatter.SerializeToHttpContent(content)
                };
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                var isValid = response.StatusCode.Equals(HttpStatusCode.OK) || response.StatusCode.Equals(HttpStatusCode.NoContent);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(new { Uri = requestUri, Content = content }, response);
                }

                this.ProlongSlidingToken();

                return isValid;
            }
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> PostAsync<T>(string requestUri, T content)
        {
            return PostAsync<T>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="TIn" /> into the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="TOut" /> .</returns>
        public virtual Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn content)
        {
            return PostAsync<TIn, TOut>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual async Task<T> PostAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = JsonFormatter.SerializeToHttpContent(content)
                };
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(new { Uri = requestUri, Content = content }, response);
                }

                this.ProlongSlidingToken();

                return await ReadContentAsync<T>(response);
            }
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="TIn" /> into the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="TOut" /> .</returns>
        public virtual async Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn content, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = JsonFormatter.SerializeToHttpContent(content)
                };
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(new { Uri = requestUri, Content = content }, response);
                }

                this.ProlongSlidingToken();

                return await ReadContentAsync<TOut>(response);
            }
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="file">The file that needs to be uploaded.</param>
        /// <param name="fileName">The name of a file.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual Task<T> PostFileAsync<T>(string requestUri, byte[] file, string fileName)
        {
            return PostFileAsync<T>(requestUri, file, fileName, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="file">The file that needs to be uploaded.</param>
        /// <param name="fileName">The name of a file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        public virtual async Task<T> PostFileAsync<T>(string requestUri, byte[] file, string fileName, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                using (var multiContent = new MultipartFormDataContent())
                using (ByteArrayContent bytes = new ByteArrayContent(file))
                {
                    bytes.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = fileName };
                    multiContent.Add(bytes, "file");
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                    {
                        Content = multiContent
                    };
                    InitializeClientAuthorization(request);

                    var response = await client.SendAsync(request, cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        await ThrowExceptionAsync(new { Uri = requestUri, Content = multiContent }, response);
                    }

                    this.ProlongSlidingToken();

                    return await ReadContentAsync<T>(response);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="T" /> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        public virtual Task<T> PutAsync<T>(string requestUri, T content)
        {
            return PutAsync<T>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="T" /> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        public virtual async Task<T> PutAsync<T>(string requestUri, T content, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
                {
                    Content = content != null ? JsonFormatter.SerializeToHttpContent(content) : null
                };
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(new { Uri = requestUri, Content = content }, response);
                }
                this.ProlongSlidingToken();

                return await ReadContentAsync<T>(response);
            }
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="TIn" /> in the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns><typeparamref name="TOut" /> .</returns>
        public virtual Task<TOut> PutAsync<TIn, TOut>(string requestUri, TIn content)
        {
            return PutAsync<TIn, TOut>(requestUri, content, new CancellationToken());
        }

        /// <summary>
        /// Asynchronously update the <typeparamref name="TIn" /> in the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><typeparamref name="TOut" /> .</returns>
        public virtual async Task<TOut> PutAsync<TIn, TOut>(string requestUri, TIn content, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                var request = new HttpRequestMessage(HttpMethod.Put, requestUri)
                {
                    Content = JsonFormatter.SerializeToHttpContent(content)
                };
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    await ThrowExceptionAsync(new { Uri = requestUri, Content = content }, response);
                }
                return await ReadContentAsync<TOut>(response);
            }
        }

        /// <summary>
        /// Returns deserialized content from response.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="response">HTTP response.</param>
        /// <returns><typeparamref name="T" /> Resource.</returns>
        public virtual async Task<T> ReadContentAsync<T>(HttpResponseMessage response)
        {
            if (typeof(T) == typeof(HttpStatusCode))
            {
                return (T)(object)response.StatusCode;
            }
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
        /// Returns byte array content from response.
        /// </summary>
        /// <param name="response">HTTP response.</param>
        /// <returns>Byte array Resource.</returns>
        public virtual Task<Stream> ReadContentAsync(HttpResponseMessage response)
        {
            if (response.Content != null && response.Content.Headers.ContentLength > 0)
            {
                return response.Content.ReadAsStreamAsync();
            }
            else
            {
                return Task.FromResult(default(Stream));
            }
        }

        /// <summary>
        /// Asynchronously sends HTTP request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>HTTP response message.</returns>
        public virtual Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return this.SendAsync(request, CancellationToken.None);
        }

        /// <summary>
        /// Asynchronously sends HTTP request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>HTTP response message.</returns>
        public virtual async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpClient client = Client;
            {
                InitializeClientAuthorization(request);

                var response = await client.SendAsync(request, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    this.ProlongSlidingToken();
                }

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
        }

        /// <summary>
        /// Initialize HTTP client authorization.
        /// </summary>
        /// <param name="request">The request.</param>
        protected virtual void InitializeClientAuthorization(HttpRequestMessage request)
        {
            var tokenHandler = this.Configuration.TokenHandler;
            if (tokenHandler != null)
            {
                var token = tokenHandler.Get();
                if (token != null && token.IsValid)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue(token.Scheme, token.Token);
                }
            }
        }

        /// <summary>
        /// Prolongs the sliding token.
        /// </summary>
        protected virtual void ProlongSlidingToken()
        {
            var tokenHandler = this.Configuration.TokenHandler;
            if (tokenHandler != null)
            {
                var token = tokenHandler.Get();
                if (token != null && token.IsValid && token.SlidingWindow.HasValue)
                {
                    var newToken = new AuthenticationToken()
                    {
                        ExpirationDate = token.SlidingWindow.HasValue ? DateTime.UtcNow.AddSeconds(token.SlidingWindow.GetValueOrDefault()) : token.ExpirationDate,
                        SlidingWindow = token.SlidingWindow,
                        Scheme = token.Scheme,
                        Token = token.Token,
                        UrlToken = token.UrlToken
                    };
                    tokenHandler.Save(newToken);
                }
            }
        }

        /// <summary>
        /// Asynchronously throws the baasic client exception.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        protected async Task ThrowExceptionAsync(object content, HttpResponseMessage response)
        {
            string requestInfo = "";
            if (content != null)
            {
                if (content.GetType().GetTypeInfo().IsValueType)
                {
                    requestInfo = content.ToString();
                }
                else
                {
                    requestInfo = JsonConvert.SerializeObject(content);
                }
            }
            BaasicErrorResponse responseObject = null;
            try
            {
                responseObject = await ReadContentAsync<BaasicErrorResponse>(response);
            }
            catch { }

            throw new BaasicClientException((int)response.StatusCode, $"{response.ReasonPhrase}. Request:\"{requestInfo}\" Response:\"{await response.Content.ReadAsStringAsync()}\"", responseObject?.Error, responseObject?.ErrorCode ?? 0);
        }

        #endregion Methods
    }
}