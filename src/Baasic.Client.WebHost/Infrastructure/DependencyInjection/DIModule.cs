using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.Security;
using System;

namespace Baasic.Client.WebHost.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Module containing Baasic Client bindings.
    /// </summary>
    public partial class DIModule
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DIModule" /> class.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        public DIModule(IDependencyResolver dependencyResolver)
        {
            DependencyResolver = dependencyResolver;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the dependency resolver.
        /// </summary>
        /// <value>The dependency resolver.</value>
        protected IDependencyResolver DependencyResolver { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Load dependency injection bindings.
        /// </summary>
        public virtual void Load()
        {
            #region Security

            DependencyResolver.Register<ITokenHandler, WebSessionTokenHandler>();

            #endregion Security
        }

        #endregion Methods
    }
}