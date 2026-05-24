using System.Text.Json.Serialization;

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
    [JsonPropertyName("product_name")]
    public string? ProductName { get; set; }
  }
}
