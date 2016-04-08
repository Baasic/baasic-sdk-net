using System;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Token handler contract.
    /// </summary>
    public interface ITokenHandler
    {
        #region Methods

        /// <summary>
        /// Clear token storage.
        /// </summary>
        /// <returns>True if token has been cleared, false otherwise.</returns>
        bool Clear();

        /// <summary>
        /// Gets the token from a storage.
        /// </summary>
        /// <returns>Token.</returns>
        IAuthenticationToken Get();

        /// <summary>
        /// Saves token to storage.
        /// </summary>
        /// <param name="token">Token to save.</param>
        /// <returns>True if token has been saved, false otherwise.</returns>
        bool Save(IAuthenticationToken token);

        #endregion Methods
    }
}