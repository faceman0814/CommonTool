using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FaceMan.DynamicWebAPI.Extensions
{
    public class JsonOptionsDate : JsonConverter<DateTime>
    {
        private readonly string _format;

        public JsonOptionsDate(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), _format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
