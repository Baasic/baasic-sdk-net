using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Baasic.Client.Formatters
{
    /// <summary>
    /// Contract for JSON formatter.
    /// </summary>
    public interface IJsonFormatter
    {
        /// <summary>
        /// Deserilzes object from stream.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Stream to read from.</param>
        /// <returns>Object.</returns>
        Task<T> DeserializeAsync<T>(Stream stream);

        /// <summary>
        /// Deserilzes object from JSON string.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="value">Serialized string.</param>
        /// <returns>Object.</returns>
        Task<T> DeserializeAsync<T>(string value);

        /// <summary>
        /// Serializes object to JSON string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized JSON string.</returns>
        Task<string> SerializeAsync(object obj);

        /// <summary>
        /// Serializes object to stream.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="stream">Stream to write to.</param>
        Task SerializeAsync(object obj, Stream stream);

        /// <summary>
        /// Creates HttpContent from object.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>HttpContent.</returns>
        Task<HttpContent> SerializeToHttpContentAsync(object obj);
    }
}