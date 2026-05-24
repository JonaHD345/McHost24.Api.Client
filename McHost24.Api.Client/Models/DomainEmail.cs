using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a domain email account payload.
  /// </summary>
  public sealed class DomainEmail
  {
    /// <summary>
    /// Gets or sets the email username.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the email password.
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }
  }
}
