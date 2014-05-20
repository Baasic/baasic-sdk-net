using Baasic.Client.Configuration;
using Baasic.Client.Formatters;
using Baasic.Client.KeyValueModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Baasic.Client
{
    /// <summary>
    /// Default implementation of <see cref="IDependencyResolver" />.
    /// </summary>
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, IList<Func<object>>> _resolvers = new Dictionary<Type, IList<Func<object>>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver" /> class.
        /// </summary>
        public DefaultDependencyResolver()
        {
            RegisterDefaultServices();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">serviceType</exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public virtual object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            IList<Func<object>> activators;
            if (_resolvers.TryGetValue(serviceType, out activators))
            {
                if (activators.Count == 0)
                {
                    return null;
                }
                if (activators.Count > 1)
                {
                    throw new InvalidOperationException(String.Format("Multiple activators are registered for this call to get service for service type: '{0}'", serviceType.FullName));
                }
                return activators[0].Invoke();
            }
            return null;
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>List of services.</returns>
        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            IList<Func<object>> activators;
            if (_resolvers.TryGetValue(serviceType, out activators))
            {
                if (activators.Count == 0)
                {
                    return null;
                }
                return activators.Select(p => p.Invoke()).ToList();
            }
            return null;
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        public virtual void Register(Type serviceType, Func<object> activator)
        {
            IList<Func<object>> activators;
            if (!_resolvers.TryGetValue(serviceType, out activators))
            {
                activators = new List<Func<object>>();
                _resolvers.Add(serviceType, activators);
            }
            else
            {
                activators.Clear();
            }
            activators.Add(activator);
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activators">The activators.</param>
        /// <exception cref="System.ArgumentNullException">activators</exception>
        public virtual void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            if (activators == null)
            {
                throw new ArgumentNullException("activators");
            }

            IList<Func<object>> list;
            if (!_resolvers.TryGetValue(serviceType, out list))
            {
                list = new List<Func<object>>();
                _resolvers.Add(serviceType, list);
            }
            else
            {
                list.Clear();
            }
            foreach (var a in activators)
            {
                list.Add(a);
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release
        /// only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        /// <summary>
        /// Registers the default services.
        /// </summary>
        private void RegisterDefaultServices()
        {
            this.Register<HttpClient>(() => new HttpClient());
            this.Register<IHttpClientFactory>(() => new HttpClientFactory(this));
            this.Register<IBaasicClientFactory>(() => new BaasicClientFactory(this));
            this.Register<IJsonFormatter>(() => new JsonFormatter());
            this.Register<IKeyValueClient>(() => new KeyValueClient(this.GetService<IClientConfiguration>(), this.GetService<IBaasicClientFactory>()));
        }
    }
}