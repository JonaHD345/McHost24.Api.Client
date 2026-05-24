using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a Minecraft server backup.
  /// </summary>
  public sealed class MinecraftServerBackup
  {
    /// <summary>
    /// Gets or sets the current backup status.
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the backup timestamp with nanoseconds.
    /// </summary>
    [JsonPropertyName("time")]
    public string? Time { get; set; }

    /// <summary>
    /// Gets or sets the backup message.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the backup archive file name.
    /// </summary>
    [JsonPropertyName("file")]
    public string? File { get; set; }

    /// <summary>
    /// Gets or sets the FTP endpoint.
    /// </summary>
    [JsonPropertyName("ftp")]
    public string? Ftp { get; set; }

    /// <summary>
    /// Gets or sets the backup type.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }
  }
}
