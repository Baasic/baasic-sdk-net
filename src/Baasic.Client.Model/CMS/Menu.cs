﻿using System.Collections.Generic;

namespace Baasic.Client.Model.CMS
{
    /// <summary>
    /// The menu class.
    /// </summary>
    public class Menu : BuiltInModelBase
    {
        #region Properties

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