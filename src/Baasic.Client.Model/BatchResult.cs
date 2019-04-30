using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// The batch result class.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    /// <seealso cref="IBatchResult{T}" />
    public class BatchResult<T> : IBatchResult<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets details.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets exception.
        /// </summary>
        public Exception Error { get; set; }

        /// <summary>
        /// Gets or sets error code.
        /// </summary>
        public long ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets response.
        /// </summary>
        public T Response { get; set; }

        /// <summary>
        /// Gets or sets status code.
        /// </summary>
        public long StatusCode { get; set; }

        #endregion Properties
    }
}