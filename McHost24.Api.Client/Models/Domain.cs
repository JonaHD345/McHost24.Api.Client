using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a domain.
  /// </summary>
  public sealed class Domain : Product
  {
    /// <summary>
    /// Gets or sets the second-level domain name.
    /// </summary>
    [JsonProperty("sld")]
    public string? Sld { get; set; }

    /// <summary>
    /// Gets or sets the top-level domain name.
    /// </summary>
    [JsonProperty("tld")]
    public string? Tld { get; set; }
  }
}

