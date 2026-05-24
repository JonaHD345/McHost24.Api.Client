using System;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a service discount.
  /// </summary>
  public sealed class Discount
  {
    /// <summary>
    /// Gets or sets the discount database id.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage.
    /// </summary>
    [JsonPropertyName("discount_percent")]
    public int? DiscountPercent { get; set; }

    /// <summary>
    /// Gets or sets the discount scope type.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the discount start timestamp.
    /// </summary>
    [JsonPropertyName("start_at")]
    public DateTimeOffset? StartAt { get; set; }

    /// <summary>
    /// Gets or sets the discount end timestamp.
    /// </summary>
    [JsonPropertyName("end_at")]
    public DateTimeOffset? EndAt { get; set; }

    /// <summary>
    /// Gets or sets the discount calculation type.
    /// </summary>
    [JsonPropertyName("discount_type")]
    public string? DiscountType { get; set; }
  }
}
