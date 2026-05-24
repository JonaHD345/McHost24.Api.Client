using Newtonsoft.Json;

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
    [JsonProperty("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the account password.
    /// </summary>
    [JsonProperty("password")]
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets the optional two-factor authentication code.
    /// </summary>
    [JsonProperty("tfa")]
    public int? Tfa { get; set; }
  }
}

