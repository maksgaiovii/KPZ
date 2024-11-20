using KPZ_lab5.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KPZ_lab5.Converters
{
    public class ContributorStatusConverter : JsonConverter<ContributorStatus>
    {
        public override ContributorStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string? value = reader.GetString();
                if (Enum.TryParse(value, ignoreCase: true, out ContributorStatus result))
                {
                    return result;
                }
            }

            throw new JsonException($"Unable to convert {reader.GetString()} to {nameof(ContributorStatus)}.");
        }

        public override void Write(Utf8JsonWriter writer, ContributorStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
