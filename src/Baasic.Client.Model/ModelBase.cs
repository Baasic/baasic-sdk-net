using Baasic.Client.Common;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Model base class.
    /// </summary>
    public abstract class ModelBase : IModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Date created.
        /// </summary>
        public System.DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Date updated.
        /// </summary>
        public System.DateTime DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        public SGuid Id { get; set; }

        #endregion Properties
    }
}