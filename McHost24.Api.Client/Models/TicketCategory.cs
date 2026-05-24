using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the category title.
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category icon.
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
  }
}
