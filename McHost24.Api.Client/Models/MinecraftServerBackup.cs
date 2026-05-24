using Newtonsoft.Json;

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
    [JsonProperty("status")]
    public string? Status { get; set; }

    /// <summary>
    /// Gets or sets the backup timestamp with nanoseconds.
    /// </summary>
    [JsonProperty("time")]
    public string? Time { get; set; }

    /// <summary>
    /// Gets or sets the backup message.
    /// </summary>
    [JsonProperty("message")]
    public string? Message { get; set; }

    /// <summary>
    /// Gets or sets the backup archive file name.
    /// </summary>
    [JsonProperty("file")]
    public string? File { get; set; }

    /// <summary>
    /// Gets or sets the FTP endpoint.
    /// </summary>
    [JsonProperty("ftp")]
    public string? Ftp { get; set; }

    /// <summary>
    /// Gets or sets the backup type.
    /// </summary>
    [JsonProperty("type")]
    public string? Type { get; set; }
  }
}

