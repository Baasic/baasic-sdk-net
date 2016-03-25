using System;
using System.Collections.Generic;

namespace Baasic.Client.Model.Permissions
{
    /// <summary>
    /// Access policy.
    /// </summary>
    public class AccessPolicy : BuiltInModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessPolicy" /> class.
        /// </summary>
        public AccessPolicy()
        {
            Actions = new List<AccessAction>();
            Section = String.Empty;
            Role = String.Empty;
            UserName = String.Empty;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="AccessAction" /> .
        /// </summary>
        /// <value>The <see cref="AccessAction" /> .</value>
        public List<AccessAction> Actions { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the section.
        /// </summary>
        /// <value>The section.</value>
        public string Section { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string UserName { get; set; }

        #endregion Properties
    }
}