using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  internal sealed class SingleOrArrayJsonConverterFactory : JsonConverterFactory
  {
    public override bool CanConvert(Type typeToConvert)
    {
      return typeToConvert.IsGenericType
        && typeToConvert.GetGenericTypeDefinition() == typeof(List<>);
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
      var itemType = typeToConvert.GetGenericArguments()[0];
      var converterType = typeof(SingleOrArrayJsonConverter<>).MakeGenericType(itemType);

      return (JsonConverter)Activator.CreateInstance(converterType)!;
    }
  }
}
