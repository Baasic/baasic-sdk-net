﻿using Newtonsoft.Json;
using System;

namespace Baasic.Client.Common
{
    /// <summary>
    /// <see cref="SGuid" /> JSON converter.
    /// </summary>
    public class NullableSGuidConverter : JsonConverter
    {
        #region Fields

        private static readonly Type sguidType = typeof(SGuid?);

        #endregion Fields

        #region Methods

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns><c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.</returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.Equals(sguidType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            if (reader.Value != null)
            {
                return new SGuid?(reader.Value.ToString());
            }
            return null;
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                var obj = value as SGuid?;
                if (obj != null && obj.HasValue)
                {
                    serializer.Serialize(writer, obj.Value.ToString());
                }
            }
        }

        #endregion Methods
    }
}