using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents the service renew payload.
  /// </summary>
  public sealed class ServiceRenewRequest
  {
    /// <summary>
    /// Gets or sets the requested renew runtime.
    /// </summary>
    [JsonProperty("runtime")]
    public string? Runtime { get; set; }
  }
}

