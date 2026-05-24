using System;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a service.
  /// </summary>
  public sealed class Service
  {
    /// <summary>
    /// Gets or sets the service database id.
    /// </summary>
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the service owner database id.
    /// </summary>
    [JsonProperty("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the service order timestamp.
    /// </summary>
    [JsonProperty("ordered_at")]
    public DateTimeOffset? OrderedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned service expiration timestamp.
    /// </summary>
    [JsonProperty("expire_at")]
    public DateTimeOffset? ExpireAt { get; set; }

    /// <summary>
    /// Gets or sets the actual service expiration timestamp.
    /// </summary>
    [JsonProperty("expired_at")]
    public DateTimeOffset? ExpiredAt { get; set; }

    /// <summary>
    /// Gets or sets the associated product database id.
    /// </summary>
    [JsonProperty("service_id")]
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the service type.
    /// </summary>
    [JsonProperty("service_type")]
    public string? ServiceType { get; set; }

    /// <summary>
    /// Gets or sets the product pack id.
    /// </summary>
    [JsonProperty("product_pack")]
    public int? ProductPack { get; set; }

    /// <summary>
    /// Gets or sets the custom product price.
    /// </summary>
    [JsonProperty("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the service creation timestamp.
    /// </summary>
    [JsonProperty("created_at")]
    public string? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the service update timestamp.
    /// </summary>
    [JsonProperty("updated_at")]
    public string? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the service deletion timestamp.
    /// </summary>
    [JsonProperty("deleted_at")]
    public string? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the product database id.
    /// </summary>
    [JsonProperty("product_id")]
    public int? ProductId { get; set; }

    /// <summary>
    /// Gets or sets the user payment method id.
    /// </summary>
    [JsonProperty("user_payment_method_id")]
    public int? UserPaymentMethodId { get; set; }

    /// <summary>
    /// Gets or sets the custom product name.
    /// </summary>
    [JsonProperty("product_name")]
    public string? ProductName { get; set; }
  }
}

