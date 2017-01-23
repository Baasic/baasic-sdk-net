using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model.Security;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Security.Token
{
    /// <summary>
    /// Token Client.
    /// </summary>
    public interface ITokenClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously creates new <see cref="IAuthenticationToken" /> for specified used.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <param name="tokenOptions">The token options.</param>
        /// <returns>New <see cref="IAuthenticationToken" /> .</returns>
        Task<IAuthenticationToken> CreateAsync(string username, string password, TokenOptions tokenOptions = null);

        /// <summary>
        /// Asynchronously destroys the <see cref="IAuthenticationToken" /> .
        /// </summary>
        /// <param name="token">Token to destroy.</param>
        /// <returns>True if <see cref="IAuthenticationToken" /> is destroyed, false otherwise.</returns>
        Task<bool> DestroyAsync();

        /// <summary>
        /// Asynchronously gets the <see cref="IAuthenticationUser" />.
        /// </summary>
        /// <param name="embed">Embed related resources.</param>
        /// <returns>New <see cref="IAuthenticationUser" /> .</returns>
        Task<IAuthenticatedUser> GetUserAsync(string embed);

        /// <summary>
        /// Asynchronously refreshes the <see cref="IAuthenticationToken" /> .
        /// </summary>
        /// <param name="token">Token to update.</param>
        /// <returns>New <see cref="IAuthenticationToken" /> .</returns>
        Task<IAuthenticationToken> RefreshAsync();

        #endregion Methods
    }
}