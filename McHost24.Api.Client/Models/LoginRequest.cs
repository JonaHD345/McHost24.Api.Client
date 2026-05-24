using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents the login payload used to request an API token.
  /// </summary>
  public sealed class LoginRequest
  {
    /// <summary>
    /// Gets or sets the account username.
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the account password.
    /// </summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the optional two-factor authentication code.
    /// </summary>
    [JsonPropertyName("tfa")]
    public int? Tfa { get; set; }
  }
}
