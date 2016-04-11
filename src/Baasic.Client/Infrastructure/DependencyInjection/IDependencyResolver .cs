using System;
using System.Collections.Generic;

namespace Baasic.Client.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency resolver contract.
    /// </summary>
    public interface IDependencyResolver : IDisposable
    {
        #region Methods

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>Service instance.</returns>
        object GetService(Type serviceType);

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <returns>Service instance.</returns>
        T GetService<T>();

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        IEnumerable<object> GetServices(Type serviceType);

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <returns>Service instances.</returns>
        IEnumerable<T> GetServices<T>();

        /// <summary>
        /// Initializes the specified modules.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Registers the specified service type to activator.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        void Register(Type serviceType, Func<object> activator);

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        void Register(Type serviceType, Type implementationType);

        /// <summary>
        /// Registers the specified service type to activator.
        /// </summary>
        /// <typeparam name="T">Type of the registered service.</typeparam>
        /// <param name="activator">The activator.</param>
        void Register<T>(Func<T> activator) where T : class;

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <typeparam name="T">Type of the registered service.</typeparam>
        /// <typeparam name="I">Type of the implementation</typeparam>
        void Register<T, I>()
            where I : class, T;

        #endregion Methods
    }
}