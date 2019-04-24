namespace Baasic.Client.Model
{
    /// <summary>
    /// The baasic error response class.
    /// </summary>
    public class BaasicErrorResponse
    {
        #region Properties

        /// <summary>
        /// Gets or sets details.
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Gets or sets error.
        /// </summary>
        public object Error { get; set; }

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
        public object Response { get; set; }

        /// <summary>
        /// Gets or sets status code.
        /// </summary>
        public int StatusCode { get; set; }

        #endregion Properties
    }
}