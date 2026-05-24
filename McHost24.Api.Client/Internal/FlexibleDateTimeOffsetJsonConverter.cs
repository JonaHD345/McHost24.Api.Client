using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  internal sealed class FlexibleDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
  {
    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.String)
      {
        var value = reader.GetString();

        if (!string.IsNullOrWhiteSpace(value)
          && DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedValue))
        {
          return parsedValue;
        }
      }

      if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var unixTimestamp))
      {
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
      }

      throw new JsonException("The JSON value could not be converted to a DateTimeOffset.");
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture));
    }
  }
}
