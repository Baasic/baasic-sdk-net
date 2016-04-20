using System;
using System.Collections.Generic;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Authenticated user details.
    /// </summary>
    public interface IAuthenticatedUser
    {
        #region Properties

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        string DisplayName { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        string Email { get; }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        SGuid Id { get; }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        /// <value>The permissions.</value>
        IDictionary<string, IEnumerable<string>> Permissions { get; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        /// <value>The roles.</value>
        IEnumerable<string> Roles { get; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>The username.</value>
        string UserName { get; }

        #endregion Properties
    }
}