using Newtonsoft.Json;

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
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the subdomain.
    /// </summary>
    [JsonProperty("sld")]
    public string? Sld { get; set; }

    /// <summary>
    /// Gets or sets the DNS record type.
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the DNS record target.
    /// </summary>
    [JsonProperty("target")]
    public string? Target { get; set; }
  }
}

