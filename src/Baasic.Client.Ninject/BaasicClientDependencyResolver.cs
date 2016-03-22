using Ninject;
using System;
using System.Collections.Generic;

namespace Baasic.Client.Ninject
{
    public class BaasicClientDependencyResolver : IDependencyResolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientDependencyResolver" /> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public BaasicClientDependencyResolver(IKernel kernel)
        {
            Kernel = kernel;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        /// <value>The kernel.</value>
        private IKernel Kernel { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            return Kernel.GetService(serviceType);
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activators">The activators.</param>
        public void Register(Type serviceType, IEnumerable<Func<object>> activators)
        {
            Kernel.Bind(serviceType).ToMethod(ctx => activators);
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        public void Register(Type serviceType, Func<object> activator)
        {
            Kernel.Bind(serviceType).ToMethod(ctx => activator);
        }

        #endregion Methods
    }
}