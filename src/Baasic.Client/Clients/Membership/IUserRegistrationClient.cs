using Baasic.Client.Infrastructure.Security;
using Baasic.Client.Model.Membership;
using Baasic.Client.Model.Security;
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
        /// <param name="tokenOptions">The token options.</param>
        /// <returns><see cref="IAuthenticationToken" />.</returns>
        Task<IAuthenticationToken> ActivateAsync(string activationToken, TokenOptions tokenOptions = null);

        /// <summary>
        /// Asynchronously register new <see cref="User" /> using <see cref="CreateUserDTO" /> options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>Newly registered <see cref="User" />.</returns>
        Task<User> RegisterAsync(CreateUserDTO options);

        /// <summary>
        /// Asynchronously register new <see cref="User" /> using <see cref="CreateUserDTO" /> options.
        /// </summary>
        /// <typeparam name="TIn">The type of the in resource.</typeparam>
        /// <typeparam name="TOut">The type of the out resource.</typeparam>
        /// <param name="options">The options.</param>
        /// <returns>Newly registered <typeparamref name="TOut" />.</returns>
        Task<TOut> RegisterAsync<TIn, TOut>(TIn options)
            where TIn : CreateUserDTO
            where TOut : User;

        #endregion Methods
    }
}