using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  internal sealed class FlexibleBooleanJsonConverter : JsonConverter<bool>
  {
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.True)
      {
        return true;
      }

      if (reader.TokenType == JsonTokenType.False)
      {
        return false;
      }

      if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt64(out var number))
      {
        return number != 0;
      }

      if (reader.TokenType == JsonTokenType.String)
      {
        var value = reader.GetString();

        if (bool.TryParse(value, out var boolValue))
        {
          return boolValue;
        }

        if (long.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var numericValue))
        {
          return numericValue != 0;
        }
      }

      throw new JsonException("The JSON value could not be converted to a Boolean.");
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
      writer.WriteBooleanValue(value);
    }
  }
}
