using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a domain DNS record.
  /// </summary>
  public sealed class DomainRecord
  {
    /// <summary>
    /// Gets or sets the DNS record database id.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the subdomain.
    /// </summary>
    [JsonPropertyName("sld")]
    public string? Sld { get; set; }

    /// <summary>
    /// Gets or sets the DNS record type.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the DNS record target.
    /// </summary>
    [JsonPropertyName("target")]
    public string? Target { get; set; }
  }
}
