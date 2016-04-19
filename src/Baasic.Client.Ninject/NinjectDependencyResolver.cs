using Baasic.Client.Infrastructure.DependencyInjection;
using Ninject;
using Ninject.Extensions.Conventions;
using System;
using System.Collections.Generic;

namespace Baasic.Client.Ninject
{
    /// <summary>
    /// Baasic client dependency resolver.
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjectDependencyResolver" /> class.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        public NinjectDependencyResolver(IKernel kernel)
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
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return Kernel.Get<T>();
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
        /// Gets the services.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetServices<T>()
        {
            return Kernel.GetAll<T>();
        }

        /// <summary>
        /// Initializes the specified modules.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">modules</exception>
        public void Initialize()
        {
            Kernel.Bind(x =>
{
    x.FromAssembliesMatching("Baasic.Client.dll", "Baasic.Client.*.dll")
     .SelectAllClasses()
     .InheritedFrom<IDIModule>()
     .BindDefaultInterface();
});

            IEnumerable<IDIModule> modules = Kernel.GetAll<IDIModule>();

            foreach (IDIModule module in modules)
            {
                module.Load(this);
            }
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        public void Register(Type serviceType, Func<object> activator)
        {
            Kernel.Rebind(serviceType).ToMethod(ctx => activator);
        }

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        public void Register(Type serviceType, Type implementationType)
        {
            Kernel.Rebind(serviceType).To(implementationType);
        }

        /// <summary>
        /// Registers the specified activator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activator">The activator.</param>
        public void Register<T>(Func<T> activator) where T : class
        {
            Kernel.Rebind(typeof(T)).ToMethod(ctx => activator);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        public void Register<T, I>()
            where I : class, T
        {
            Kernel.Rebind<T>().To<I>();
        }

        #endregion Methods
    }
}