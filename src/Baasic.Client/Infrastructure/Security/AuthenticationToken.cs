using Newtonsoft.Json;
using System;

namespace Baasic.Client.Infrastructure.Security
{
    /// <summary>
    /// Authentication token details.
    /// </summary>
    public class AuthenticationToken : IAuthenticationToken
    {
        #region Properties

        /// <summary>
        /// Token expiration date.
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the expires in.
        /// </summary>
        /// <value>The expires in.</value>
        [JsonProperty("expires_in")]
        public long? ExpiresIn { get; set; }

        /// <summary>
        /// Gets if token is valid.
        /// </summary>
        public bool IsValid
        {
            get
            {
                return this.ExpirationDate >= DateTime.UtcNow;
            }
        }

        /// <summary>
        /// Gets token scheme.
        /// </summary>
        [JsonProperty("token_type")]
        public string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the sliding window.
        /// </summary>
        /// <value>The sliding window.</value>
        [JsonProperty("sliding_window")]
        public long? SlidingWindow { get; set; }

        /// <summary>
        /// Gets token.
        /// </summary>
        [JsonProperty("access_token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets the URL token.
        /// </summary>
        /// <value>The URL token.</value>
        [JsonProperty("access_url_token")]
        public string UrlToken { get; set; }

        #endregion Properties
    }
}