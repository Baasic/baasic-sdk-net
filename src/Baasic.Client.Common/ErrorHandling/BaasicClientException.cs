using System;

namespace Baasic.Client.Common
{
    /// <summary>
    /// Baasic client exception.
    /// </summary>
    public class BaasicClientException : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public BaasicClientException(long errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        public BaasicClientException(long errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public BaasicClientException(long errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public long ErrorCode { get; set; }

        #endregion Properties
    }
}