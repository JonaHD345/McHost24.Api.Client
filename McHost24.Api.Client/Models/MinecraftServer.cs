using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a Minecraft server.
  /// </summary>
  public sealed class MinecraftServer : NamedProduct
  {
    /// <summary>
    /// Gets or sets the Multicraft panel id.
    /// </summary>
    [JsonPropertyName("multicraft_id")]
    public int? MulticraftId { get; set; }

    /// <summary>
    /// Gets or sets the server address with port.
    /// </summary>
    [JsonPropertyName("address")]
    public string? Address { get; set; }

    /// <summary>
    /// Gets or sets the memory in mebibytes.
    /// </summary>
    [JsonPropertyName("memory")]
    public int? Memory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the server is online.
    /// </summary>
    [JsonPropertyName("online")]
    public bool Online { get; set; }

    /// <summary>
    /// Gets or sets the current online player count.
    /// </summary>
    [JsonPropertyName("players_online")]
    public int? PlayersOnline { get; set; }

    /// <summary>
    /// Gets or sets the maximum player count.
    /// </summary>
    [JsonPropertyName("players_max")]
    public int? PlayersMax { get; set; }

    /// <summary>
    /// Gets or sets the current CPU usage percentage.
    /// </summary>
    [JsonPropertyName("cpu_usage")]
    public int? CpuUsage { get; set; }

    /// <summary>
    /// Gets or sets the current memory usage percentage.
    /// </summary>
    [JsonPropertyName("mem_usage")]
    public int? MemoryUsage { get; set; }
  }
}
