using KPZ_lab5.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KPZ_lab5.Converters
{
    public class BookStatusConverter : JsonConverter<BookStatus>
    {
        public override BookStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string? value = reader.GetString();
                if (Enum.TryParse(value, ignoreCase: true, out BookStatus result))
                {
                    return result;
                }
            }

            throw new JsonException($"Unable to convert {reader.GetString()} to {nameof(BookStatus)}.");
        }

        public override void Write(Utf8JsonWriter writer, BookStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
