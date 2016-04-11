using System;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// DTO class for password recovery.
    /// </summary>
    public class PasswordRecoveryDTO : ModelBase
    {
        #region Properties

        /// <summary>
        /// New user password.
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// Password recovery token.
        /// </summary>
        public string PasswordRecoveryToken { get; set; }

        #endregion Properties
    }
}