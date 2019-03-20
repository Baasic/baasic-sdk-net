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
        /// <param name="statusCode">The status code.</param>
        public BaasicClientException(int statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        public BaasicClientException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="error">The error.</param>
        public BaasicClientException(int statusCode, string message, string error) : base(message)
        {
            StatusCode = statusCode;
            Error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="error">The error:</param>
        /// <param name="errorCode">The error code.</param>
        public BaasicClientException(int statusCode, string message, string error, long errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaasicClientException" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="error">The error.</param>
        /// <param name="errorCode">The error code.</param>
        /// <param name="innerException">The inner exception.</param>
        public BaasicClientException(int statusCode, string message, string error, long errorCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
            Error = error;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>The error code.</value>
        public long ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets HTTP status code.
        /// </summary>
        public int StatusCode { get; set; }

        #endregion Properties
    }
}