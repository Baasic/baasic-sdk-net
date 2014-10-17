using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Built-In Model interface.
    /// </summary>
    public interface IBuiltInModel : IModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        new Guid Id { get; set; }

        #endregion Properties
    }
}