using Baasic.Client.Infrastructure.DependencyInjection;
using System;
using System.Net.Http;

namespace Baasic.Client.Core
{
    /// <summary>
    /// <see cref="HttpClient" /> factory.
    /// </summary>
    public class HttpClientFactory : IHttpClientFactory
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientFactory" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public HttpClientFactory(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the dependency resolver.
        /// </summary>
        /// <value>The dependency resolver.</value>
        private IDependencyResolver DependencyResolver { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates <see cref="HttpClient" /> instance.
        /// </summary>
        /// <returns><see cref="HttpClient" /> instance.</returns>
        public virtual HttpClient Create()
        {
            return DependencyResolver.GetService<HttpClient>();
        }

        #endregion Methods
    }
}