using System;
using System.Collections.Generic;

namespace Baasic.Client
{
    /// <summary>
    /// Dependency resolver contract.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        object GetService(Type serviceType);

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        IEnumerable<object> GetServices(Type serviceType);

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        void Register(Type serviceType, Func<object> activator);

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activators">The activators.</param>
        void Register(Type serviceType, IEnumerable<Func<object>> activators);
    }
}