using System;
using Newtonsoft.Json;

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
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the discount percentage.
    /// </summary>
    [JsonProperty("discount_percent")]
    public int? DiscountPercent { get; set; }

    /// <summary>
    /// Gets or sets the discount scope type.
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }

    /// <summary>
    /// Gets or sets the discount start timestamp.
    /// </summary>
    [JsonProperty("start_at")]
    public DateTimeOffset? StartAt { get; set; }

    /// <summary>
    /// Gets or sets the discount end timestamp.
    /// </summary>
    [JsonProperty("end_at")]
    public DateTimeOffset? EndAt { get; set; }

    /// <summary>
    /// Gets or sets the discount calculation type.
    /// </summary>
    [JsonProperty("discount_type")]
    public string? DiscountType { get; set; }
  }
}

