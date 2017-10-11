using Autofac;
using System;

namespace Baasic.Client.AutoFac
{
    /// <summary>
    /// AutoFac settings for Baasic client dependency resolver.
    /// </summary>
    public class AutoFacSettings : IDisposable
    {
        public AutoFacSettings()
        {
            Builder = new ContainerBuilder();
        }

        public AutoFacSettings(ContainerBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Gets or sets the builder.
        /// </summary>
        /// <value>The builder.</value>
        public ContainerBuilder Builder { get; set; }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public IContainer Container { get; set; }

        /// <summary>
        /// Finishes with registration and creates the container.
        /// </summary>
        public void Build()
        {
            Container = Builder.Build();
            Builder = null;
        }

        #region Properties

        public void Dispose()
        {
            Container.Dispose();
        }

        #endregion Properties
    }
}