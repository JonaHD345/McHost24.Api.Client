using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  internal sealed class SingleOrArrayJsonConverter<T> : JsonConverter<List<T>>
  {
    public override List<T>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      if (reader.TokenType == JsonTokenType.Null)
      {
        return null;
      }

      if (reader.TokenType != JsonTokenType.StartArray)
      {
        var singleValue = JsonSerializer.Deserialize<T>(ref reader, options);

        return singleValue == null
          ? new List<T>()
          : new List<T> { singleValue };
      }

      var values = new List<T>();

      while (reader.Read())
      {
        if (reader.TokenType == JsonTokenType.EndArray)
        {
          return values;
        }

        var value = JsonSerializer.Deserialize<T>(ref reader, options);
        values.Add(value!);
      }

      throw new JsonException("The JSON array could not be read.");
    }

    public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNullValue();
        return;
      }

      writer.WriteStartArray();

      foreach (var item in value)
      {
        JsonSerializer.Serialize(writer, item, options);
      }

      writer.WriteEndArray();
    }
  }
}
