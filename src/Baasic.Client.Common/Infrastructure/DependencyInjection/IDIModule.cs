using System;

namespace Baasic.Client.Common.Infrastructure.DependencyInjection
{
    /// <summary>
    /// A loadable unit that defines bindings for your application..
    /// </summary>
    public interface IDIModule
    {
        #region Methods

        /// <summary>
        /// Load dependency injection bindings.
        /// </summary>
        /// <param name="dependencyResolver">The dependency resolver.</param>
        void Load(IDependencyResolver dependencyResolver);

        #endregion Methods
    }
}