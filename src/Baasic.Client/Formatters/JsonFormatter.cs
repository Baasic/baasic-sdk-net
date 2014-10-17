using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Baasic.Client.Formatters
{
    /// <summary>
    /// Default JSON formatter.
    /// </summary>
    public class JsonFormatter : IJsonFormatter
    {
        #region Fields

        private readonly Encoding defaultEncoding = UTF8Encoding.UTF8;
        private readonly JsonSerializer serializer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonFormatter"/> class.
        /// </summary>
        public JsonFormatter()
        {
            var serializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };

            serializerSettings.Converters.Add(new IsoDateTimeConverter());

            this.serializer = JsonSerializer.Create(serializerSettings);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Deserialize object from JSON string.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="value">Serialized string.</param>
        /// <returns>Object.</returns>
        public T Deserialize<T>(string value)
        {
            using (var strReader = new StringReader(value))
            {
                using (var reader = new JsonTextReader(strReader))
                {
                    return serializer.Deserialize<T>(reader);
                }
            }
        }

        /// <summary>
        /// Deserilzes object from stream.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="stream">Stream to read from.</param>
        /// <returns>Object.</returns>
        public T Deserialize<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                using (var reader = new JsonTextReader(streamReader))
                {
                    return serializer.Deserialize<T>(reader);
                }
            }
        }

        /// <summary>
        /// Serializes object to JSON string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized JSON string.</returns>
        public string Serialize(object obj)
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
        }

        /// <summary>
        /// Serializes object to stream.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <param name="stream">Stream to write to.</param>
        public void Serialize(object obj, Stream stream)
        {
            using (var strWriter = new StreamWriter(stream))
            {
                using (var jsonWriter = new JsonTextWriter(strWriter))
                {
                    serializer.Serialize(jsonWriter, obj);
                }
            }
        }

        /// <summary>
        /// Creates HttpContent from object.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>HttpContent.</returns>
        public HttpContent SerializeToHttpContent(object obj)
        {
            var content = new PushStreamContent((stream, httpContent, transportContext) =>
                {
                    using (var writer = new StreamWriter(stream, defaultEncoding))
                    {
                        this.serializer.Serialize(writer, obj);
                    }
                }
            );

            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json")
            {
                CharSet = defaultEncoding.WebName
            };

            return content;
        }

        #endregion Methods
    }
}