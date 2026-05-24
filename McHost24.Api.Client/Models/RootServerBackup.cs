using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a root server backup.
  /// </summary>
  public sealed class RootServerBackup
  {
    /// <summary>
    /// Gets or sets the backup database id.
    /// </summary>
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the backup creation timestamp.
    /// </summary>
    [JsonProperty("created_at")]
    public double? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the backup finished.
    /// </summary>
    [JsonProperty("finished")]
    public bool Finished { get; set; }
  }
}

