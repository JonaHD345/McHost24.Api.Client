using System;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a support ticket answer.
  /// </summary>
  public sealed class TicketAnswer
  {
    /// <summary>
    /// Gets or sets the answer database id.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the referenced ticket database id.
    /// </summary>
    [JsonPropertyName("ticket_id")]
    public int? TicketId { get; set; }

    /// <summary>
    /// Gets or sets the answer message.
    /// </summary>
    [JsonPropertyName("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the replying user id.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the replying collaborator id.
    /// </summary>
    [JsonPropertyName("col_id")]
    public int? CollaboratorId { get; set; }

    /// <summary>
    /// Gets or sets the answer creation timestamp.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the answer update timestamp.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
  }
}
