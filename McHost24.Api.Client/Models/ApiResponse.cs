using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a non-generic API response.
  /// </summary>
  public sealed class ApiResponse : ApiResponseBase
  {
    /// <summary>
    /// Gets or sets the raw response data.
    /// </summary>
    [JsonProperty("data")]
    public JToken? Data { get; set; }
  }

  /// <summary>
  /// Represents an API response with typed data.
  /// </summary>
  /// <typeparam name="TData">The response data type.</typeparam>
  public sealed class ApiResponse<TData> : ApiResponseBase
  {
    /// <summary>
    /// Gets or sets the typed response data.
    /// </summary>
    [JsonProperty("data")]
    public TData? Data { get; set; }
  }

  /// <summary>
  /// Represents shared API response fields.
  /// </summary>
  public abstract class ApiResponseBase
  {
    /// <summary>
    /// Gets or sets the API status text.
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets translated response metadata.
    /// </summary>
    [JsonProperty("meta")]
    public ApiResponseMeta? Meta { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the request was executed correctly.
    /// </summary>
    [JsonProperty("success")]
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets all messages returned by the API.
    /// </summary>
    [JsonProperty("messages")]
    public List<string>? Messages { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether datatables should be reloaded.
    /// </summary>
    [JsonProperty("reload_datatables")]
    public bool ReloadDatatables { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the site should be reloaded.
    /// </summary>
    [JsonProperty("reload")]
    public bool Reload { get; set; }
  }

  /// <summary>
  /// Represents translated API response metadata.
  /// </summary>
  public sealed class ApiResponseMeta
  {
    /// <summary>
    /// Gets or sets warning messages.
    /// </summary>
    [JsonProperty("warnings")]
    public List<string>? Warnings { get; set; }

    /// <summary>
    /// Gets or sets error messages.
    /// </summary>
    [JsonProperty("errors")]
    public List<string>? Errors { get; set; }

    /// <summary>
    /// Gets or sets success messages.
    /// </summary>
    [JsonProperty("success")]
    public List<string>? Success { get; set; }
  }
}

