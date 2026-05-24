using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a support ticket.
  /// </summary>
  public sealed class Ticket
  {
    /// <summary>
    /// Gets or sets the ticket database id.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the opener user id.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the collaborator id.
    /// </summary>
    [JsonPropertyName("col_id")]
    public int? CollaboratorId { get; set; }

    /// <summary>
    /// Gets or sets the ticket subject.
    /// </summary>
    [JsonPropertyName("betr")]
    public string? Betr { get; set; }

    /// <summary>
    /// Gets or sets the ticket message.
    /// </summary>
    [JsonPropertyName("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the ticket state.
    /// </summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the server id referenced by the ticket.
    /// </summary>
    [JsonPropertyName("server_id")]
    public int? ServerId { get; set; }

    /// <summary>
    /// Gets or sets the service id referenced by the ticket.
    /// </summary>
    [JsonPropertyName("service_id")]
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the ticket category id.
    /// </summary>
    [JsonPropertyName("ticket_category_id")]
    public string? TicketCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the ticket answers.
    /// </summary>
    [JsonPropertyName("answers")]
    public List<TicketAnswer>? Answers { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ticket is pinned.
    /// </summary>
    [JsonPropertyName("pinned")]
    public bool Pinned { get; set; }

    /// <summary>
    /// Gets or sets the ticket creation timestamp.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the ticket update timestamp.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
  }
}
