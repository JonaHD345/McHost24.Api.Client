using System.Text.Json.Serialization;

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
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the backup creation timestamp.
    /// </summary>
    [JsonPropertyName("created_at")]
    public double? CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the backup finished.
    /// </summary>
    [JsonPropertyName("finished")]
    public bool Finished { get; set; }
  }
}
