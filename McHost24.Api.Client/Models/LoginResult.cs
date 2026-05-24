using Newtonsoft.Json;

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
    [JsonProperty("api_token")]
    public string? ApiToken { get; set; }
  }
}

