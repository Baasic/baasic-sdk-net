using System;
using System.Net.Http;

namespace Baasic.Client
{
    /// <summary>
    /// <see cref="HttpClient"/> factory.
    /// </summary>
    public interface IHttpClientFactory
    {
        #region Methods

        /// <summary>
        /// Creates <see cref="HttpClient"/> instance.
        /// </summary>
        /// <returns><see cref="HttpClient"/> instance.</returns>
        HttpClient Create();

        #endregion Methods
    }
}