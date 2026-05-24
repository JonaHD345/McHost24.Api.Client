using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a root server VNC console response.
  /// </summary>
  public sealed class RootServerVncConsole
  {
    /// <summary>
    /// Gets or sets the VNC console URL.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }
  }
}
