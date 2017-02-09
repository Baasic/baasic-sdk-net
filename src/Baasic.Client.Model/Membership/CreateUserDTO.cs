using System;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// DTO class for user credentials.
    /// </summary>
    public class CreateUserDTO : ModelBase
    {
        #region Properties

        /// <summary>
        /// Activation URL to put in activation notification.
        /// </summary>
        public string ActivationUrl { get; set; }

        /// <summary>
        /// Recovery request challenge.
        /// </summary>
        public string ChallengeResponse { get; set; }

        /// <summary>
        /// URL where user should go to change his password.
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>
        /// URL where user should go to change his password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public string UserName { get; set; }

        #endregion Properties
    }
}