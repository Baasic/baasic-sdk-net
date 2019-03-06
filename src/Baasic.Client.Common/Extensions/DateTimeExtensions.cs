using System;

namespace Baasic.Client.Common
{
    /// <summary>
    /// The date time extensions class.
    /// </summary>
    public static class DateTimeExtensions
    {
        #region Methods

        /// <summary>
        /// Converts the instance of <see cref="DateTime" /> to ISO 8601 standard.
        /// </summary>
        /// <param name="date">The instance of <see cref="DateTime" /></param>
        /// <returns>The ISO 8601 string.</returns>
        public static string ToISOString(this DateTime date)
        {
            return date.ToUniversalTime().ToString("o");
        }

        #endregion Methods
    }
}