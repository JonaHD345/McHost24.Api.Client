using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents additional domain information.
  /// </summary>
  public sealed class DomainInfo
  {
    /// <summary>
    /// Gets or sets the domain.
    /// </summary>
    [JsonPropertyName("domain")]
    public Domain? Domain { get; set; }

    /// <summary>
    /// Gets or sets the DNS records.
    /// </summary>
    [JsonPropertyName("records")]
    public List<DomainRecord>? Records { get; set; }

    /// <summary>
    /// Gets or sets the domain email entries.
    /// </summary>
    [JsonPropertyName("emails")]
    public List<DomainEmail>? Emails { get; set; }
  }
}
