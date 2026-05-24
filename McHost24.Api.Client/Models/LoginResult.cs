using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents the login result returned by the API.
  /// </summary>
  public sealed class LoginResult
  {
    /// <summary>
    /// Gets or sets the generated API token.
    /// </summary>
    [JsonPropertyName("api_token")]
    public string? ApiToken { get; set; }
  }
}
