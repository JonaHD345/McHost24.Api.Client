using System;
using Newtonsoft.Json;

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
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the referenced ticket database id.
    /// </summary>
    [JsonProperty("ticket_id")]
    public int? TicketId { get; set; }

    /// <summary>
    /// Gets or sets the answer message.
    /// </summary>
    [JsonProperty("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the replying user id.
    /// </summary>
    [JsonProperty("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the replying collaborator id.
    /// </summary>
    [JsonProperty("col_id")]
    public int? CollaboratorId { get; set; }

    /// <summary>
    /// Gets or sets the answer creation timestamp.
    /// </summary>
    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the answer update timestamp.
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
  }
}

