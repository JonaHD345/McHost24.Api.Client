using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents the support ticket reply payload.
  /// </summary>
  public sealed class TicketReplyRequest
  {
    /// <summary>
    /// Gets or sets the reply message.
    /// </summary>
    [JsonPropertyName("reply")]
    public string? Reply { get; set; }
  }
}
