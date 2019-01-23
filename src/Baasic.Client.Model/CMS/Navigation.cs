using Baasic.Client.Common;
using System.Collections.Generic;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The navigation class.
    /// </summary>
    public class Navigation : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the type of the item.
        /// </summary>
        /// <value>The type of the item. [0 - container, 1 - page, 2 - link]</value>
        public int? ItemType { get; set; }

        /// <summary>
        /// The Json property of the Navigation.
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>The menu identifier.</value>
        public SGuid MenuId { get; set; }

        /// <summary>
        /// Gets or sets the core navigations.
        /// </summary>
        /// <value>The core navigations.</value>
        public IEnumerable<Navigation> Navigations { get; set; }

        /// <summary>
        /// Gets or sets the core page.
        /// </summary>
        /// <value>The core page.</value>
        public Page Page { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        /// <value>The page identifier.</value>
        public SGuid? PageId { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public SGuid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the parent navigation item.
        /// </summary>
        /// <value>The core navigation.</value>
        public Navigation ParentNavigation { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url { get; set; }

        #endregion Properties
    }
}