using System;

namespace Baasic.Client.TokenHandler
{
    /// <summary>
    /// Authentication token details.
    /// </summary>
    public interface IAuthenticationToken
    {
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
    }
}