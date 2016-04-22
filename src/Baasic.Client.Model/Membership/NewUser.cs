using System;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// User used to transfer data from and to Baasic.
    /// </summary>
    public class NewUser : User
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [automatic create password].
        /// </summary>
        /// <value><c>true</c> if [automatic create password]; otherwise, <c>false</c>.</value>
        public bool AutoCreatePassword { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>The confirm password.</value>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the mail URL.
        /// </summary>
        /// <value>The mail URL.</value>
        public string MailUrl { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the system should send an email notification.
        /// </summary>
        public bool SendEmailNotification { get; set; }

        #endregion Properties
    }
}