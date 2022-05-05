using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Minimal.Blazor.Converters
{
    public class StepConverter : JsonConverter<string>
    {
        public override string Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException("Expected StartArray token");

            var set = new StringBuilder();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                //if (reader.TokenType == JsonTokenType.EndObject)

                if (reader.TokenType != JsonTokenType.PropertyName)
                    throw new JsonException("Expected PropertyName token");

                var propName = reader.GetString();
                reader.Read();

                switch (propName)
                {
                    case "steps":
                        set.AppendLine(reader.GetString());
                        break;
                }

            }

            return set.ToString();
        }

        public override void Write(
            Utf8JsonWriter writer,
            string stringValue,
            JsonSerializerOptions options) =>
                writer.WriteString("steps", stringValue);
    }
}
