using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents ticket system metadata.
  /// </summary>
  public sealed class TicketSystemInfo
  {
    /// <summary>
    /// Gets or sets available ticket categories.
    /// </summary>
    [JsonPropertyName("categories")]
    public List<TicketCategory>? Categories { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a ticket can be created.
    /// </summary>
    [JsonPropertyName("canCreateTicket")]
    public bool CanCreateTicket { get; set; }

    /// <summary>
    /// Gets or sets available services.
    /// </summary>
    [JsonPropertyName("services")]
    public List<Service>? Services { get; set; }
  }
}
