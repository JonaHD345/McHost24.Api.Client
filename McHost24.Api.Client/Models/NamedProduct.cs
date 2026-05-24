using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a product with a custom product name.
  /// </summary>
  public class NamedProduct : Product
  {
    /// <summary>
    /// Gets or sets the custom product name.
    /// </summary>
    [JsonProperty("product_name")]
    public string? ProductName { get; set; }
  }
}

