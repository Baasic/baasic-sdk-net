using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Baasic.Client.Formatters
{
    /// <summary>
    /// Default JSON formatter.
    /// </summary>
    public class JsonFormatter : IJsonFormatter
    {
        private readonly Encoding defaultEncoding = UTF8Encoding.UTF8;
        private readonly JsonSerializer serializer;

        /// <summary>
        /// Constructor
        /// </summary>
        public JsonFormatter()
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            serializerSettings.Converters.Add(new IsoDateTimeConverter());

            this.serializer = JsonSerializer.Create(serializerSettings);
        }

        /// <summary>
        /// Deserilzes object from stream.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Stream to read from.</param>
        /// <returns>Object.</returns>
        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            return Task.Run(() =>
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(streamReader))
                    {
                        return serializer.Deserialize<T>(reader);
                    }
                }
            });
        }

        /// <summary>
        /// Deserilzes object from JSON string.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="value">Serialized string.</param>
        /// <returns>Object.</returns>
        public Task<T> DeserializeAsync<T>(string value)
        {
            return Task.Run(() =>
            {
                using (var strReader = new StringReader(value))
                {
                    using (var reader = new JsonTextReader(strReader))
                    {
                        return serializer.Deserialize<T>(reader);
                    }
                }
            });
        }

        /// <summary>
        /// Serializes object to JSON string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized JSON string.</returns>
        public Task<string> SerializeAsync(object obj)
        {
            return Task.Run(() =>
            {
                var sb = new StringBuilder();
                using (var strWriter = new StringWriter(sb))
                {
                    using (var jsonWriter = new JsonTextWriter(strWriter))
                    {
                        serializer.Serialize(jsonWriter, obj);
                        jsonWriter.Flush();

                        return sb.ToString();
                    }
                }
            });
        }

        /// <summary>
        /// Serializes object to stream.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="stream">Stream to write to.</param>
        public Task SerializeAsync(object obj, Stream stream)
        {
            return Task.Run(() =>
            {
                using (var strWriter = new StreamWriter(stream))
                {
                    using (var jsonWriter = new JsonTextWriter(strWriter))
                    {
                        serializer.Serialize(jsonWriter, obj);
                    }
                }
            });
        }

        /// <summary>
        /// Creates HttpContent from object.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>HttpContent.</returns>
        public async Task<HttpContent> SerializeToHttpContentAsync(object obj)
        {
            return new StringContent(
                await this.SerializeAsync(obj),
                this.defaultEncoding,
                "application/json"
            );
        }
    }
}