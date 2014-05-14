using System;
using System.Collections.Generic;

namespace Baasic.Client
{
    /// <summary>
    /// Dependency resolver extensions.
    /// </summary>
    public static class IDependencyResolverExtensions
    {
        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <returns>Service instance.</returns>
        public static T GetService<T>(this IDependencyResolver dependencyResolver)
        {
            var result = dependencyResolver.GetService(typeof(T));
            if (result != null)
                return (T)result;
            return default(T);
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <returns>Service instances.</returns>
        public static IEnumerable<T> GetServices<T>(this IDependencyResolver dependencyResolver)
        {
            var result = dependencyResolver.GetServices(typeof(T));
            if (result != null)
                return (IEnumerable<T>)result;
            return default(IEnumerable<T>);
        }

        /// <summary>
        /// Registers the specified dependency resolver.
        /// </summary>
        /// <typeparam name="T">Type of the registered service.</typeparam>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <param name="activator">The activator.</param>
        public static void Register<T>(this IDependencyResolver dependencyResolver, Func<T> activator) where T : class
        {
            dependencyResolver.Register(typeof(T), activator);
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <typeparam name="T">Type of the registered service.</typeparam>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        /// <param name="activators">The activators.</param>
        public static void Register<T>(this IDependencyResolver dependencyResolver, IEnumerable<Func<T>> activators) where T : class
        {
            dependencyResolver.Register(typeof(T), activators);
        }
    }
}