using System;

namespace Baasic.Client.Model.Permissions
{
    /// <summary>
    /// Access action.
    /// </summary>
    public class AccessAction : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the action abbreviation.
        /// </summary>
        /// <value>The action abbreviation.</value>
        public string Abrv { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        #endregion Properties
    }
}