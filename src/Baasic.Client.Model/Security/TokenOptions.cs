using System;

namespace Baasic.Client.Model.Security
{
    /// <summary>
    /// Token options.
    /// </summary>
    public class TokenOptions : BuiltInModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenOptions" /> class.
        /// </summary>
        /// <param name="slidingExpiration">if set to <c>true</c> token will use sliding expiration.</param>
        /// <param name="useSessionToken">
        /// if set to <c>true</c> token will be tide to session. This option is used for Web browsers.
        /// </param>
        public TokenOptions(bool slidingExpiration = true, bool useSessionToken = false)
        {
            SlidingExpiration = slidingExpiration;
            UseSessionToken = useSessionToken;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether token will use sliding expiration.
        /// </summary>
        /// <value><c>true</c> if token is using sliding expiration; otherwise, <c>false</c>.</value>
        public bool SlidingExpiration { get; private set; }

        /// <summary>
        /// Gets a value indicating whether token is tide to session.
        /// </summary>
        /// <value><c>true</c> if token is session based; otherwise, <c>false</c>.</value>
        public bool UseSessionToken { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the options in CSV format.
        /// </summary>
        /// <returns>Options CSV.</returns>
        public string GetOptionsCSV()
        {
            string options = "options=";
            if (SlidingExpiration)
            {
                options += "sliding,";
            }
            if (UseSessionToken)
            {
                options += "session,";
            }
            return options.Trim(',');
        }

        #endregion Methods
    }
}