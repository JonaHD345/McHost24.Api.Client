using System;
using System.Globalization;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  internal sealed class FlexibleNullableIntegerJsonConverter : JsonConverter
  {
    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(int?);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
      {
        return null;
      }

      if (reader.TokenType == JsonToken.Integer)
      {
        return Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);
      }

      if (reader.TokenType == JsonToken.String)
      {
        var value = reader.Value?.ToString();

        if (string.IsNullOrWhiteSpace(value)
          || string.Equals(value, "SYSTEM", StringComparison.OrdinalIgnoreCase))
        {
          return null;
        }

        if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsedValue))
        {
          return parsedValue;
        }
      }

      throw new JsonSerializationException("The JSON value could not be converted to a nullable Int32.");
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
      if (value == null)
      {
        writer.WriteNull();
        return;
      }

      writer.WriteValue((int)value);
    }
  }
}
