using System;

namespace Baasic.Client.Model.Membership
{
    /// <summary>
    /// DTO class for password recovery request.
    /// </summary>
    public class PasswordRecoveryRequestDTO : ModelBase
    {
        #region Properties

        /// <summary>
        /// Challenge identifier.
        /// </summary>
        public string ChallengeIdentifier { get; set; }

        /// <summary>
        /// Recovery request challenge.
        /// </summary>
        public string ChallengeResponse { get; set; }

        /// <summary>
        /// URL where user should go to change his password.
        /// </summary>
        public string RecoverUrl { get; set; }

        /// <summary>
        /// Username.
        /// </summary>
        public string UserName { get; set; }

        #endregion Properties
    }
}