using System;

namespace Baasic.Client.Model
{
    /// <summary>
    /// The batch result contract.
    /// </summary>
    /// <typeparam name="T">The response type.</typeparam>
    public interface IBatchResult<T>
    {
        #region Properties

        /// <summary>
        /// Gets or sets details.
        /// </summary>
        string Details { get; set; }

        /// <summary>
        /// Gets or sets exception.
        /// </summary>
        Exception Error { get; set; }

        /// <summary>
        /// Gets or sets error code.
        /// </summary>
        long ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets message.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets response.
        /// </summary>
        T Response { get; set; }

        /// <summary>
        /// Gets or sets status code.
        /// </summary>
        long StatusCode { get; set; }

        #endregion Properties
    }
}