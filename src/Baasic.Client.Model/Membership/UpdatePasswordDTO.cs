using System;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// DTO class for password update.
    /// </summary>
    public class UpdatePasswordDTO : ModelBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        /// <value>The new password.</value>
        public string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to send mail notification.
        /// </summary>
        public bool SendMailNotification { get; set; }

        /// <summary>
        /// Gets or sets the Web Site URL.
        /// </summary>
        /// <value>The Web Site URL.</value>
        public string SiteUrl { get; set; }

        #endregion Properties
    }
}