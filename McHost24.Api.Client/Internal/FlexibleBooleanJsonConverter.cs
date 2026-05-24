using System;
using System.Globalization;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  internal sealed class FlexibleBooleanJsonConverter : JsonConverter<bool>
  {
    public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Boolean)
      {
        return (bool)reader.Value!;
      }

      if (reader.TokenType == JsonToken.Integer)
      {
        var numericValue = Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture);
        return numericValue != 0;
      }

      if (reader.TokenType == JsonToken.String)
      {
        var value = reader.Value?.ToString();

        if (bool.TryParse(value, out var boolValue))
        {
          return boolValue;
        }

        if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericValue))
        {
          return numericValue != 0;
        }
      }

      throw new JsonSerializationException("The JSON value could not be converted to a Boolean.");
    }

    public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
    {
      writer.WriteValue(value);
    }
  }
}
