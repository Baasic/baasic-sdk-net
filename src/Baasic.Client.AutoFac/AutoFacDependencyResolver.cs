using Autofac;
using Baasic.Client.Common.Infrastructure.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
        /// <param name="settings">The settings for the resolver.</param>
        public AutofacDependencyResolver(AutoFacSettings settings)
        {
            Settings = settings;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        private AutoFacSettings Settings { get; set; }

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
            var genericEnumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
            return Settings.Container.Resolve(genericEnumerable) as IEnumerable<object>;
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetServices<T>()
        {
            return Settings.Container.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Initializes the specified modules.
        /// </summary>
        public void Initialize()
        {
            LoadModules();
        }

        /// <summary>
        /// Not possible with Autofac, use Register<T>(Func<T>).
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        public void Register(Type serviceType, Func<object> activator)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        public void Register(Type serviceType, Type implementationType)
        {
            Settings.Builder.RegisterGeneric(implementationType).AsImplementedInterfaces();
        }

        /// <summary>
        /// Registers the specified activator.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activator">The activator.</param>
        public void Register<T>(Func<T> activator) where T : class
        {
            Settings.Builder.Register<T>(ctx => activator());
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        public void Register<T, I>() where I : class, T
        {
            Settings.Builder.RegisterType<I>().As<T>();
        }

        #endregion Methods

        /// <summary>
        /// Loads and registers modules from assembly.
        /// </summary>
        private void LoadModules()
        {
            //Get all the assemblies matching the given pattern
            var executingAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            var appFolder = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(executingAssembly.CodeBase).Path));
            var assemblyPatterns = new[] { "Baasic.Client.dll", "Baasic.Client.*.dll" };

            var assembliesToScan = new List<Assembly>();
            foreach (var pattern in assemblyPatterns)
            {
                foreach (var file in Directory.EnumerateFiles(appFolder, pattern, SearchOption.AllDirectories))
                {
                    assembliesToScan.Add(Assembly.LoadFrom(file));
                }
            }

            var assemblies = assembliesToScan.Distinct().ToArray();

            //Get all IDIModule instances from the assemblies
            var DIModules = from t in assemblies.SelectMany(x => x.GetTypes())
                            where t.GetInterfaces().Contains(typeof(IDIModule)) && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as IDIModule;

            //Register the resolver instance
            Settings.Builder.RegisterInstance<IDependencyResolver>(this);

            //Load the DIModules
            foreach (var instance in DIModules)
            {
                instance.Load(this);
            }          
        }
    }
}