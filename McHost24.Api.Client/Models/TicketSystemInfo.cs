using System.Collections.Generic;
using Newtonsoft.Json;

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
    [JsonProperty("categories")]
    public List<TicketCategory>? Categories { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether a ticket can be created.
    /// </summary>
    [JsonProperty("canCreateTicket")]
    public bool CanCreateTicket { get; set; }

    /// <summary>
    /// Gets or sets available services.
    /// </summary>
    [JsonProperty("services")]
    public List<Service>? Services { get; set; }
  }
}

