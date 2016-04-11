using Baasic.Client.Model.Membership;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership module user password recovery client.
    /// </summary>
    public interface IUserPasswordRecoveryClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously recovery password using <see cref="PasswordRecoveryDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>True if password is recovered, false otherwise.</returns>
        Task<bool> PasswordRecoveryAsync(PasswordRecoveryDTO options);

        /// <summary>
        /// Asynchronously request a new password recovery using <see cref="PasswordRecoveryRequestDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>True if password recovery request is valid, false otherwise.</returns>
        Task<bool> RequestPasswordRecoveryAsync(PasswordRecoveryRequestDTO options);

        #endregion Methods
    }
}