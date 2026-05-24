using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents an IP address assigned to a server.
  /// </summary>
  public sealed class Address
  {
    /// <summary>
    /// Gets or sets the IPv4 address.
    /// </summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; }

    /// <summary>
    /// Gets or sets the reverse DNS value.
    /// </summary>
    [JsonPropertyName("rdns")]
    public string? Rdns { get; set; }
  }
}
