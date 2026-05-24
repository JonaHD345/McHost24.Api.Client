using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents service renew pricing.
  /// </summary>
  public sealed class ServiceRenewPrice
  {
    /// <summary>
    /// Gets or sets available renew runtimes.
    /// </summary>
    [JsonPropertyName("runtimes")]
    public List<ServiceRuntimePrice>? Runtimes { get; set; }

    /// <summary>
    /// Gets or sets the active discount.
    /// </summary>
    [JsonPropertyName("discount")]
    public Discount? Discount { get; set; }
  }

  /// <summary>
  /// Represents the price for a service runtime.
  /// </summary>
  public sealed class ServiceRuntimePrice
  {
    /// <summary>
    /// Gets or sets the renew runtime.
    /// </summary>
    [JsonPropertyName("runtime")]
    public string? Runtime { get; set; }

    /// <summary>
    /// Gets or sets the renew price.
    /// </summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
  }
}
