using System;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Configures the MC-HOST24 API client.
  /// </summary>
  public sealed class McHost24ClientOptions
  {
    /// <summary>
    /// Gets the default production API base address.
    /// </summary>
    public static Uri DefaultBaseAddress { get; } = new Uri("https://mc-host24.de/api/v1/");

    /// <summary>
    /// Gets or sets the API base address used for requests.
    /// </summary>
    public Uri BaseAddress { get; set; } = DefaultBaseAddress;

    /// <summary>
    /// Gets or sets the API token sent through the Authorization header.
    /// </summary>
    public string? ApiToken { get; set; }

    /// <summary>
    /// Gets or sets custom JSON serializer settings.
    /// </summary>
    public JsonSerializerSettings? JsonSerializerSettings { get; set; }
  }
}
