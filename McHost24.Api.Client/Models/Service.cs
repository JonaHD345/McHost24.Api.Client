using System;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the service owner database id.
    /// </summary>
    [JsonPropertyName("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// Gets or sets the service order timestamp.
    /// </summary>
    [JsonPropertyName("ordered_at")]
    public DateTimeOffset? OrderedAt { get; set; }

    /// <summary>
    /// Gets or sets the planned service expiration timestamp.
    /// </summary>
    [JsonPropertyName("expire_at")]
    public DateTimeOffset? ExpireAt { get; set; }

    /// <summary>
    /// Gets or sets the actual service expiration timestamp.
    /// </summary>
    [JsonPropertyName("expired_at")]
    public DateTimeOffset? ExpiredAt { get; set; }

    /// <summary>
    /// Gets or sets the associated product database id.
    /// </summary>
    [JsonPropertyName("service_id")]
    public int? ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the service type.
    /// </summary>
    [JsonPropertyName("service_type")]
    public string? ServiceType { get; set; }

    /// <summary>
    /// Gets or sets the product pack id.
    /// </summary>
    [JsonPropertyName("product_pack")]
    public int? ProductPack { get; set; }

    /// <summary>
    /// Gets or sets the custom product price.
    /// </summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the service creation timestamp.
    /// </summary>
    [JsonPropertyName("created_at")]
    public string? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the service update timestamp.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public string? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the service deletion timestamp.
    /// </summary>
    [JsonPropertyName("deleted_at")]
    public string? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets the product database id.
    /// </summary>
    [JsonPropertyName("product_id")]
    public int? ProductId { get; set; }

    /// <summary>
    /// Gets or sets the user payment method id.
    /// </summary>
    [JsonPropertyName("user_payment_method_id")]
    public int? UserPaymentMethodId { get; set; }

    /// <summary>
    /// Gets or sets the custom product name.
    /// </summary>
    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }
  }
}
