using Baasic.Client.Common;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The menu position class.
    /// </summary>
    public class MenuPosition
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public SGuid Id { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public SGuid MenuId { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public string Position { get; set; }

        #endregion Properties
    }
}