using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// Model interface.
    /// </summary>
    public interface IModel
    {
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
        object Id { get; set; }
    }
}