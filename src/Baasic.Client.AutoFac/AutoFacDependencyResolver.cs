using Autofac;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.DependencyInjection;
using System;

using System;

using System.Collections.Generic;

namespace Baasic.Client.AutoFac
{
    /// <summary>
    /// Baasic client dependency resolver.
    /// </summary>
    public class AutofacDependencyResolver : IDependencyResolver
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacDependencyResolver" /> class.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public AutofacDependencyResolver(AutoFacSettings settings)
        {
            Settings = settings;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        protected AutoFacSettings Settings { get; private set; }

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
            return Settings.Container.Resolve(serviceType);
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetService<T>()
        {
            return Settings.Container.Resolve<T>();
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Settings.Container.GetAll(serviceType);
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetServices<T>()
        {
            return Settings.Container.GetAll<T>();
        }

        /// <summary>
        /// Initializes the specified modules.
        /// </summary>
        /// <exception cref="System.ArgumentNullException">modules</exception>
        public void Initialize()
        {
            Settings.Container.Bind(x =>
{
    x.FromAssembliesMatching("Baasic.Client.dll", "Baasic.Client.*.dll")
     .SelectAllClasses()
     .InheritedFrom<IDIModule>()
     .BindDefaultInterface();
});

            IEnumerable<IDIModule> modules = Settings.Container.GetAll<IDIModule>();

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
            Settings.Container.Rebind(serviceType).ToMethod(ctx => activator);
        }

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        public void Register(Type serviceType, Type implementationType)
        {
            Settings.Container.Rebind(serviceType).To(implementationType);
        }

        /// <summary>
        /// Registers the specified activator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activator">The activator.</param>
        public void Register<T>(Func<T> activator) where T : class
        {
            Settings.Container.Rebind(typeof(T)).ToMethod(ctx => activator);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        public void Register<T, I>()
            where I : class, T
        {
            Settings.Container.Rebind<T>().To<I>();
        }

        #endregion Methods
    }
}