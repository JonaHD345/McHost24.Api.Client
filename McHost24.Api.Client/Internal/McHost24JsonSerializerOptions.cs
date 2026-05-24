using System.Text.Json;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  internal static class McHost24JsonSerializerOptions
  {
    public static JsonSerializerOptions Create(JsonSerializerOptions? source)
    {
      var options = source == null
        ? new JsonSerializerOptions()
        : new JsonSerializerOptions(source);

      options.PropertyNameCaseInsensitive = true;
      options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
      options.NumberHandling = JsonNumberHandling.AllowReadingFromString;

      AddConverterIfMissing(options, new FlexibleBooleanJsonConverter());
      AddConverterIfMissing(options, new FlexibleDateTimeOffsetJsonConverter());
      AddConverterIfMissing(options, new SingleOrArrayJsonConverterFactory());

      return options;
    }

    private static void AddConverterIfMissing(JsonSerializerOptions options, JsonConverter converter)
    {
      foreach (var existingConverter in options.Converters)
      {
        if (existingConverter.GetType() == converter.GetType())
        {
          return;
        }
      }

      options.Converters.Add(converter);
    }
  }
}
