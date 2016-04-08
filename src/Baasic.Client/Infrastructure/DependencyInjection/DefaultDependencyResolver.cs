using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Baasic.Client.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Default implementation of <see cref="IDependencyResolver" /> .
    /// </summary>
    public class DefaultDependencyResolver : IDependencyResolver
    {
        #region Fields

        private readonly Dictionary<Type, IList<Func<object>>> _resolversActivator = new Dictionary<Type, IList<Func<object>>>();
        private readonly Dictionary<Type, IList<Type>> _resolversImplementation = new Dictionary<Type, IList<Type>>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver" /> class.
        /// </summary>
        public DefaultDependencyResolver()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
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

            object result = ResolveService(serviceType);

            //Self bind-able
            if (result == null)
            {
                result = CreateService(serviceType);
            }

            if (result == null)
            {
                throw new InvalidOperationException(String.Format("Unable to resolve the service: '{0}'", serviceType.FullName));
            }

            return result;
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <returns>Service instance.</returns>
        public T GetService<T>()
        {
            var result = GetService(typeof(T));
            if (result != null)
                return (T)result;
            return default(T);
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns>List of services.</returns>
        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            List<object> result = new List<object>();

            IList<Func<object>> activators;
            if (_resolversActivator.TryGetValue(serviceType, out activators))
            {
                if (activators.Count > 0)
                {
                    result.AddRange(activators.Select(p => p.Invoke()));
                }
            }

            IList<Type> implementations;
            if (_resolversImplementation.TryGetValue(serviceType, out implementations))
            {
                if (implementations.Count > 0)
                {
                    result.AddRange(implementations.Select(p => GetService(p)));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <typeparam name="T">Type of the requested service.</typeparam>
        /// <returns>Service instances.</returns>
        public IEnumerable<T> GetServices<T>()
        {
            var result = GetServices(typeof(T));
            if (result != null)
                return (IEnumerable<T>)result;
            return default(IEnumerable<T>);
        }

        /// <summary>
        /// Registers the specified service type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="activator">The activator.</param>
        /// <exception cref="System.ArgumentNullException">activators</exception>
        public virtual void Register(Type serviceType, Func<object> activator)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (activator == null)
            {
                throw new ArgumentNullException("activators");
            }

            IList<Func<object>> list;
            if (!_resolversActivator.TryGetValue(serviceType, out list))
            {
                list = new List<Func<object>>();
                _resolversActivator.Add(serviceType, list);
            }
            else
            {
                list.Clear();
            }
            list.Add(activator);
        }

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <exception cref="System.ArgumentNullException">activators</exception>
        public virtual void Register(Type serviceType, Type implementationType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (implementationType == null)
            {
                throw new ArgumentNullException("implementationType");
            }

            IList<Type> list;
            if (!_resolversImplementation.TryGetValue(serviceType, out list))
            {
                list = new List<Type>();
                _resolversImplementation.Add(serviceType, list);
            }
            else
            {
                list.Clear();
            }
            list.Add(implementationType);
        }

        /// <summary>
        /// Registers the specified dependency resolver.
        /// </summary>
        /// <typeparam name="T">Type of the registered service.</typeparam>
        /// <param name="activator">The activator.</param>
        public void Register<T>(Func<T> activator) where T : class
        {
            Register(typeof(T), activator);
        }

        /// <summary>
        /// Registers the specified service type to implementation type.
        /// </summary>
        /// <typeparam name="T">Type of the registered service.</typeparam>
        /// <typeparam name="I">Type of the implementation</typeparam>
        public void Register<T, I>() where I : class, T
        {
            Register(typeof(T), typeof(I));
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        private object CreateService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            var typeInfo = serviceType.GetTypeInfo();
            if (typeInfo.IsClass)
            {
                //TODO: Optimize
                List<ParameterInfo> parameters = new List<ParameterInfo>();
                foreach (var ctor in typeInfo.DeclaredConstructors)
                {
                    var param = ctor.GetParameters();
                    if (parameters.Count < param.Length)
                    {
                        parameters = new List<ParameterInfo>(param);
                    }
                }
                if (parameters.Count > 0)
                {
                    List<object> dependencies = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        object dep = null;
                        var pTypeInfo = parameter.ParameterType.GetTypeInfo();
                        if (pTypeInfo.IsValueType)
                        {
                            throw new ArgumentOutOfRangeException(String.Format("Unable to resolve '{0}' for '{1}'.", parameter.ParameterType.Name, serviceType.FullName));
                        }
                        else if (pTypeInfo.IsClass)
                        {
                            dep = CreateService(parameter.ParameterType);
                        }
                        else if (pTypeInfo.IsInterface)
                        {
                            dep = ResolveService(parameter.ParameterType);
                        }
                        if (dep == null)
                        {
                            throw new ArgumentOutOfRangeException(String.Format("Unable to resolve '{0}' for '{1}'.", parameter.ParameterType.Name, serviceType.FullName));
                        }

                        dependencies.Add(dep);
                    }
                    return Activator.CreateInstance(serviceType, dependencies.ToArray());
                }
                else
                {
                    return Activator.CreateInstance(serviceType);
                }
            }
            return null;
        }

        private object ResolveService(Type serviceType)
        {
            object result = null;

            Type baseType = serviceType;

            var tInfo = serviceType.GetTypeInfo();
            if (tInfo.IsGenericType)
            {
                baseType = tInfo.GetGenericTypeDefinition();
            }

            IList<Func<object>> activators;
            if (_resolversActivator.TryGetValue(baseType, out activators))
            {
                if (activators.Count > 1)
                {
                    throw new InvalidOperationException(String.Format("Multiple activators are registered for the service: '{0}'", serviceType.FullName));
                }
                if (activators.Count == 1)
                {
                    result = activators[0].Invoke();
                }
            }

            IList<Type> implementations;
            if (_resolversImplementation.TryGetValue(baseType, out implementations))
            {
                if (implementations.Count > 1)
                {
                    throw new InvalidOperationException(String.Format("Multiple implementations are registered for the service: '{0}'", serviceType.FullName));
                }
                if (implementations.Count == 1)
                {
                    Type genericParameterType = null;
                    if (tInfo.IsGenericType)
                    {
                        genericParameterType = tInfo.GenericTypeArguments.First();
                    }
                    if (genericParameterType != null)
                    {
                        result = CreateService(implementations.First().MakeGenericType(genericParameterType));
                    }
                    else
                    {
                        result = CreateService(implementations.First());
                    }
                }
            }
            return result;
        }

        #endregion Methods
    }
}