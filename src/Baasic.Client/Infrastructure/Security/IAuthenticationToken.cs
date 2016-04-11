using System;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Authentication token details.
    /// </summary>
    public interface IAuthenticationToken
    {
        #region Properties

        /// <summary>
        /// Gets if token is valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets token scheme.
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Gets token.
        /// </summary>
        string Token { get; }

        /// <summary>
        /// Gets the URL token.
        /// </summary>
        /// <value>The URL token.</value>
        string UrlToken { get; }

        #endregion Properties
    }
}