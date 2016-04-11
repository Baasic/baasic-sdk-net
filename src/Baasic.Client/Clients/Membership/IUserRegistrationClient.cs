using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model.Membership;
using System;
using System.Threading.Tasks;

namespace Baasic.Client.Membership
{
    /// <summary>
    /// Membership module user registration client.
    /// </summary>
    public interface IUserRegistrationClient
    {
        #region Methods

        /// <summary>
        /// Asynchronously activates the <see cref="User" /> using activation token.
        /// </summary>
        /// <param name="activationToken">The activation token.</param>
        /// <returns><see cref="IAuthenticationToken" />.</returns>
        Task<IAuthenticationToken> ActivateAsync(string activationToken);

        /// <summary>
        /// Asynchronously register new <see cref="User" /> using <see cref="CreateUserDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Newly registered <see cref="User" />.</returns>
        Task<User> RegisterAsync(CreateUserDTO options);

        #endregion Methods
    }
}