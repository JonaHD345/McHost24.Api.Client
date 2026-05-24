using Newtonsoft.Json;

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
    [JsonProperty("betr")]
    public string? Betr { get; set; }

    /// <summary>
    /// Gets or sets the ticket message.
    /// </summary>
    [JsonProperty("text")]
    public string? Text { get; set; }

    /// <summary>
    /// Gets or sets the related service id.
    /// </summary>
    [JsonProperty("service")]
    public int Service { get; set; }

    /// <summary>
    /// Gets or sets the ticket category id.
    /// </summary>
    [JsonProperty("ticket_category_id")]
    public int TicketCategoryId { get; set; }
  }
}

