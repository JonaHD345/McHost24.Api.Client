using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents fields shared by MC-HOST24 products.
  /// </summary>
  public class Product
  {
    /// <summary>
    /// Gets or sets the MC-HOST24 database id.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the MC-HOST24 service id.
    /// </summary>
    [JsonPropertyName("service_id")]
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the order timestamp.
    /// </summary>
    [JsonPropertyName("service_ordered_at")]
    public long? ServiceOrderedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned expiration timestamp.
    /// </summary>
    [JsonPropertyName("expire_at")]
    public long? ExpireAt { get; set; }

    /// <summary>
    /// Gets or sets the actual expiration timestamp.
    /// </summary>
    [JsonPropertyName("expired_at")]
    public long? ExpiredAt { get; set; }
  }
}
