using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents the support ticket creation payload.
  /// </summary>
  public sealed class CreateTicketRequest
  {
    /// <summary>
    /// Gets or sets the ticket subject.
    /// </summary>
    [JsonPropertyName("betr")]
    public string? Betr { get; set; }

    /// <summary>
    /// Gets or sets the ticket message.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the related service id.
    /// </summary>
    [JsonPropertyName("service")]
    public int Service { get; set; }

    /// <summary>
    /// Gets or sets the ticket category id.
    /// </summary>
    [JsonPropertyName("ticket_category_id")]
    public int TicketCategoryId { get; set; }
  }
}
