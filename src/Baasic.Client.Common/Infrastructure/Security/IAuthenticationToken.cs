using System;

namespace Baasic.Client.Common.Infrastructure.Security
{
    /// <summary>
    /// Authentication token details.
    /// </summary>
    public interface IAuthenticationToken
    {
        #region Properties

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        /// <value>The expiration date.</value>
        DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>The expires in.</value>
        long? ExpiresIn { get; set; }

        /// <summary>
        /// Gets if token is valid.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Gets token scheme.
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Gets or sets the expiration time.
        /// </summary>
        /// <value>The expiration time.</value>
        long? SlidingWindow { get; set; }

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