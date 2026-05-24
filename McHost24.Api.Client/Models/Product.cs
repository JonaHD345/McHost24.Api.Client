using Newtonsoft.Json;

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
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the MC-HOST24 service id.
    /// </summary>
    [JsonProperty("service_id")]
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the order timestamp.
    /// </summary>
    [JsonProperty("service_ordered_at")]
    public long? ServiceOrderedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned expiration timestamp.
    /// </summary>
    [JsonProperty("expire_at")]
    public long? ExpireAt { get; set; }

    /// <summary>
    /// Gets or sets the actual expiration timestamp.
    /// </summary>
    [JsonProperty("expired_at")]
    public long? ExpiredAt { get; set; }
  }
}

