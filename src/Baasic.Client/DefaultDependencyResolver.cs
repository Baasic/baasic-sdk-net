using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Baasic.Client
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        private readonly Dictionary<Type, IList<Func<object>>> _resolvers = new Dictionary<Type, IList<Func<object>>>();
        private readonly HashSet<IDisposable> _trackedDisposables = new HashSet<IDisposable>();
        private int _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDependencyResolver" /> class.
        /// </summary>
        public DefaultDependencyResolver()
        {
            RegisterDefaultServices();
        }

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
                    throw new InvalidOperationException(String.Format("Multiple Activators Area Registered Call Get Services - {0}", serviceType.FullName));
                }
                return Track(activators[0]);
            }
            return null;
        }

        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            IList<Func<object>> activators;
            if (_resolvers.TryGetValue(serviceType, out activators))
            {
                if (activators.Count == 0)
                {
                    return null;
                }
                return activators.Select(Track).ToList();
            }
            return null;
        }

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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Interlocked.Exchange(ref _disposed, 1) == 0)
                {
                    lock (_trackedDisposables)
                    {
                        foreach (var d in _trackedDisposables)
                        {
                            d.Dispose();
                        }

                        _trackedDisposables.Clear();
                    }
                }
            }
        }

        /// <summary>
        /// Registers the default services.
        /// </summary>
        private void RegisterDefaultServices()
        {
            //var traceManager = new Lazy<TraceManager>(() => new TraceManager());
            //Register(typeof(ITraceManager), () => traceManager.Value);

            //var serverIdManager = new ServerIdManager();
            //Register(typeof(IServerIdManager), () => serverIdManager);

            //var serverMessageHandler = new Lazy<IServerCommandHandler>(() => new ServerCommandHandler(this));
            //Register(typeof(IServerCommandHandler), () => serverMessageHandler.Value);

            //var newMessageBus = new Lazy<IMessageBus>(() => new MessageBus(this));
            //Register(typeof(IMessageBus), () => newMessageBus.Value);

            //var stringMinifier = new Lazy<IStringMinifier>(() => new StringMinifier());
            //Register(typeof(IStringMinifier), () => stringMinifier.Value);

            //var jsonSerializer = new Lazy<JsonSerializer>();
            //Register(typeof(JsonSerializer), () => jsonSerializer.Value);

            //var transportManager = new Lazy<TransportManager>(() => new TransportManager(this));
            //Register(typeof(ITransportManager), () => transportManager.Value);

            //var configurationManager = new DefaultConfigurationManager();
            //Register(typeof(IConfigurationManager), () => configurationManager);

            //var transportHeartbeat = new Lazy<TransportHeartbeat>(() => new TransportHeartbeat(this));
            //Register(typeof(ITransportHeartbeat), () => transportHeartbeat.Value);

            //var connectionManager = new Lazy<ConnectionManager>(() => new ConnectionManager(this));
            //Register(typeof(IConnectionManager), () => connectionManager.Value);

            //var ackHandler = new Lazy<AckHandler>();
            //Register(typeof(IAckHandler), () => ackHandler.Value);

            //var perfCounterWriter = new Lazy<PerformanceCounterManager>(() => new PerformanceCounterManager(this));
            //Register(typeof(IPerformanceCounterManager), () => perfCounterWriter.Value);

            //var userIdProvider = new PrincipalUserIdProvider();
            //Register(typeof(IUserIdProvider), () => userIdProvider);
        }

        private object Track(Func<object> creator)
        {
            object obj = creator();

            if (_disposed == 0)
            {
                var disposable = obj as IDisposable;
                if (disposable != null)
                {
                    lock (_trackedDisposables)
                    {
                        if (_disposed == 0)
                        {
                            _trackedDisposables.Add(disposable);
                        }
                    }
                }
            }

            return obj;
        }
    }
}