using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the opener user id.
    /// </summary>
    [JsonProperty("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the collaborator id.
    /// </summary>
    [JsonProperty("col_id")]
    public int? CollaboratorId { get; set; }

    /// <summary>
    /// Gets or sets the ticket subject.
    /// </summary>
    [JsonProperty("betr")]
    public string? Betr { get; set; }

    /// <summary>
    /// Gets or sets the ticket message.
    /// </summary>
    [JsonProperty("msg")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the ticket state.
    /// </summary>
    [JsonProperty("state")]
    public string? State { get; set; }

    /// <summary>
    /// Gets or sets the server id referenced by the ticket.
    /// </summary>
    [JsonProperty("server_id")]
    public int? ServerId { get; set; }

    /// <summary>
    /// Gets or sets the service id referenced by the ticket.
    /// </summary>
    [JsonProperty("service_id")]
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the ticket category id.
    /// </summary>
    [JsonProperty("ticket_category_id")]
    public string? TicketCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the ticket answers.
    /// </summary>
    [JsonProperty("answers")]
    public List<TicketAnswer>? Answers { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ticket is pinned.
    /// </summary>
    [JsonProperty("pinned")]
    public bool Pinned { get; set; }

    /// <summary>
    /// Gets or sets the ticket creation timestamp.
    /// </summary>
    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the ticket update timestamp.
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }
  }
}

