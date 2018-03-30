using Baasic.Client.Common;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Model interface.
    /// </summary>
    public interface IModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Date created.
        /// </summary>
        System.DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the Date updated.
        /// </summary>
        System.DateTime DateUpdated { get; set; }

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        SGuid Id { get; set; }

        #endregion Properties
    }
}