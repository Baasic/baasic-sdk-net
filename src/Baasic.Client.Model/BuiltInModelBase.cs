using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Built-In Model base class.
    /// </summary>
    public abstract class BuiltInModelBase : ModelBase, IBuiltInModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public new SGuid Id { get; set; }

        #endregion Properties
    }
}