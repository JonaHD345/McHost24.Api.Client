using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a ticket category.
  /// </summary>
  public sealed class TicketCategory
  {
    /// <summary>
    /// Gets or sets the ticket category database id.
    /// </summary>
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the category title.
    /// </summary>
    [JsonProperty("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category icon.
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; set; }
  }
}

