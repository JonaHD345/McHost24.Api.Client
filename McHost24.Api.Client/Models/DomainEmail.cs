using Newtonsoft.Json;

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
    [JsonProperty("username")]
    public string? Username { get; set; }

    /// <summary>
    /// Gets or sets the email password.
    /// </summary>
    [JsonProperty("password")]
    public string? Password { get; set; }
  }
}

