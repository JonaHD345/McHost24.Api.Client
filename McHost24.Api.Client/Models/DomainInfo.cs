using System.Collections.Generic;
using Newtonsoft.Json;

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
    [JsonProperty("domain")]
    public Domain? Domain { get; set; }

    /// <summary>
    /// Gets or sets the DNS records.
    /// </summary>
    [JsonProperty("records")]
    public List<DomainRecord>? Records { get; set; }

    /// <summary>
    /// Gets or sets the domain email entries.
    /// </summary>
    [JsonProperty("emails")]
    public List<DomainEmail>? Emails { get; set; }
  }
}

