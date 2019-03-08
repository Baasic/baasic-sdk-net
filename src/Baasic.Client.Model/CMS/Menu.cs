using Baasic.Client.Common;
using Baasic.Client.Model.Profile;
using System.Collections.Generic;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The menu class.
    /// </summary>
    public class Menu : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets the language property of Menu.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// Gets language identifier property of Menu.
        /// </summary>
        public SGuid LanguageId { get; set; }

        /// <summary>
        /// The Name property of the Menu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the core navigations.
        /// </summary>
        /// <value>The core navigations.</value>
        public IEnumerable<Navigation> Navigations { get; set; }

        /// <summary>
        /// Gets or sets the menu positions.
        /// </summary>
        /// <value>The menu positions.</value>
        public IEnumerable<MenuPosition> Positions { get; set; }

        #endregion Properties
    }
}