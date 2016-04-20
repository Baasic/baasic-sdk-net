using System;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Authenticated user details.
    /// </summary>
    public class AuthenticatedUser : IAuthenticatedUser
    {
        #region Properties

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public SGuid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        public System.Collections.Generic.IDictionary<string, System.Collections.Generic.IEnumerable<string>> Permissions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        public System.Collections.Generic.IEnumerable<string> Roles
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>The username.</value>
        public string UserName
        {
            get;
            set;
        }

        #endregion Properties
    }
}