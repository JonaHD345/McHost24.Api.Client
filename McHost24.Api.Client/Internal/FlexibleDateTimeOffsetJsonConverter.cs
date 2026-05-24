using System;
using System.Globalization;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  internal sealed class FlexibleDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
  {
    public override DateTimeOffset ReadJson(JsonReader reader, Type objectType, DateTimeOffset existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.String)
      {
        var value = reader.Value?.ToString();

        if (!string.IsNullOrWhiteSpace(value)
          && DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var parsedValue))
        {
          return parsedValue;
        }
      }

      if (reader.TokenType == JsonToken.Integer)
      {
        var unixTimestamp = Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture);
        return DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
      }

      throw new JsonSerializationException("The JSON value could not be converted to a DateTimeOffset.");
    }

    public override void WriteJson(JsonWriter writer, DateTimeOffset value, JsonSerializer serializer)
    {
      writer.WriteValue(value.ToUniversalTime().ToString("O", CultureInfo.InvariantCulture));
    }
  }
}
