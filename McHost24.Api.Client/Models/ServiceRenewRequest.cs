using System.Text.Json.Serialization;

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
    [JsonPropertyName("runtime")]
    public string? Runtime { get; set; }
  }
}
