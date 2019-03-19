using Baasic.Client.Common.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.DependencyInjection;
using Baasic.Client.Infrastructure.Security;

using System;
using Baasic.Client.Common.Infrastructure.Security;

namespace Baasic.Client.WebHost.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Module containing Baasic Client bindings.
    /// </summary>
    public partial class DIModule : IDIModule
    {
        #region Methods

        /// <summary>
        /// Load dependency injection bindings.
        /// </summary>
        /// <param name="dependencyResolver"></param>
        public virtual void Load(IDependencyResolver dependencyResolver)
        {
            #region Security

            //dependencyResolver.Register<ITokenHandler, WebSessionTokenHandler>();
            dependencyResolver.Register<ITokenHandler, CookieTokenHandler>();
            dependencyResolver.Register<IPlatformTokenHandler, PlatformCookieTokenHandler>();

            #endregion Security
        }

        #endregion Methods
    }
}