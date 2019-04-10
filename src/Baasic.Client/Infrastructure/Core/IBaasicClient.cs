using Baasic.Client.Common.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Baasic.Client.Core
{
    /// <summary>
    /// Baasic client contract.
    /// </summary>
    public interface IBaasicClient : IDisposable
    {
        #region Properties

        /// <summary>
        /// Gets or sets client configuration.
        /// </summary>
        IClientConfiguration Configuration { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(string requestUri);

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync<T>(string requestUri, T content);

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync<T>(string requestUri, T content, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously deletes the object from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if object is deleted, false otherwise.</returns>
        Task<bool> DeleteAsync(string requestUri, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the API URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        string GetApiUrl(string relativeUrl, params object[] parameters);

        /// <summary>
        /// Gets the API URL.
        /// </summary>
        /// <param name="ssl">if set to <c>true</c> [SSL].</param>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        string GetApiUrl(bool ssl, string relativeUrl, params object[] parameters);

        /// <summary>
        /// Asynchronously gets the <typeparamref name="T" /> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <returns><typeparamref name="T" /> .</returns>
        Task<T> GetAsync<T>(string requestUri);

        /// <summary>
        /// Asynchronously gets the <typeparamref name="T" /> from the system.
        /// </summary>
        /// <typeparam name="T">Object type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><typeparamref name="T" /> .</returns>
        Task<T> GetAsync<T>(string requestUri, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously gets the response as byte array from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response as byte array.</returns>
        Task<Stream> GetAsync(string requestUri, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously gets the response as byte array from the system.
        /// </summary>
        /// <param name="requestUri">Request URI.</param>
        /// <returns>Response as byte array.</returns>
        Task<Stream> GetAsync(string requestUri);

        /// <summary>
        /// Gets the secure API URL.
        /// </summary>
        /// <param name="relativeUrl">The relative URL.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        string GetSecureApiUrl(string relativeUrl, params object[] parameters);

        /// <summary>
        /// Patches the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        Task<bool> PatchAsync<T>(string requestUri, T content);

        /// <summary>
        /// Patches the asynchronous.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">The request URI.</param>
        /// <param name="content">The content.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> PatchAsync<T>(string requestUri, T content, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> PostAsync<T>(string requestUri, T content);

        /// <summary>
        /// Asynchronously insert the <typeparamref name="TIn" /> into the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Newly created <typeparamref name="TOut" /> .</returns>
        Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn content);

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> PostAsync<T>(string requestUri, T content, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously insert the <typeparamref name="TIn" /> into the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="TOut" /> .</returns>
        Task<TOut> PostAsync<TIn, TOut>(string requestUri, TIn content, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="file">The file that needs to be uploaded.</param>
        /// <param name="fileName">The name of a file.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> PostFileAsync<T>(string requestUri, Stream file, string fileName);

        /// <summary>
        /// Asynchronously insert the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="file">The file that needs to be uploaded.</param>
        /// <param name="fileName">The name of a file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> PostFileAsync<T>(string requestUri, Stream file, string fileName, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously update the <typeparamref name="TIn" /> in the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns><typeparamref name="TOut" /> .</returns>
        Task<TOut> PutAsync<TIn, TOut>(string requestUri, TIn content);

        /// <summary>
        /// Asynchronously update the <typeparamref name="TIn" /> in the system.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns><typeparamref name="TOut" /> .</returns>
        Task<TOut> PutAsync<TIn, TOut>(string requestUri, TIn content, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously update the <typeparamref name="T" /> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        Task<T> PutAsync<T>(string requestUri, T content);

        /// <summary>
        /// Asynchronously update the <typeparamref name="T" /> in the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="content">Resource instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Updated <typeparamref name="T" /> .</returns>
        Task<T> PutAsync<T>(string requestUri, T content, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously updates the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="file">The file that needs to be uploaded.</param>
        /// <param name="fileName">The name of a file.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> PutFileAsync<T>(string requestUri, Stream file, string fileName);

        /// <summary>
        /// Asynchronously updates the <typeparamref name="T" /> into the system.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="requestUri">Request URI.</param>
        /// <param name="file">The file that needs to be uploaded.</param>
        /// <param name="fileName">The name of a file.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Newly created <typeparamref name="T" /> .</returns>
        Task<T> PutFileAsync<T>(string requestUri, Stream file, string fileName, CancellationToken cancellationToken);

        /// <summary>
        /// Returns byte array content from response.
        /// </summary>
        /// <param name="response">HTTP response.</param>
        /// <returns>Byte array Resource.</returns>
        Task<Stream> ReadContentAsync(HttpResponseMessage response);

        /// <summary>
        /// Returns deserialized content from response.
        /// </summary>
        /// <typeparam name="T">Resource type.</typeparam>
        /// <param name="response">HTTP response.</param>
        /// <returns><typeparamref name="T" /> Resource.</returns>
        Task<T> ReadContentAsync<T>(HttpResponseMessage response);

        /// <summary>
        /// Asynchronously sends HTTP request.
        /// </summary>
        /// <typeparam name="request">HTTP request.</typeparam>
        /// <returns>HTTP response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);

        /// <summary>
        /// Asynchronously sends HTTP request.
        /// </summary>
        /// <typeparam name="request">HTTP request.</typeparam>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>HTTP response message.</returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);

        #endregion Methods
    }
}