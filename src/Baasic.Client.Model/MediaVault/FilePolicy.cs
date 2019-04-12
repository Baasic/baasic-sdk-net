using Baasic.Client.Common;

namespace Baasic.Client.Model.MediaVault
{
    /// <summary>
    /// File policy model.
    /// </summary>
    /// <seealso cref="Baasic.Client.Model.BuiltInModelBase" />
    public class FilePolicy : BuiltInModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        public SGuid RoleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public SGuid UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        #endregion Properties
    }
}