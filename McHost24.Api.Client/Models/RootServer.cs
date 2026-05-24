using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a root server.
  /// </summary>
  public sealed class RootServer : NamedProduct
  {
    /// <summary>
    /// Gets or sets the number of root server cores.
    /// </summary>
    [JsonPropertyName("cores")]
    public int? Cores { get; set; }

    /// <summary>
    /// Gets or sets the root server memory in mebibytes.
    /// </summary>
    [JsonPropertyName("memory")]
    public int? Memory { get; set; }

    /// <summary>
    /// Gets or sets the root server disk size in gibibytes.
    /// </summary>
    [JsonPropertyName("disk_size")]
    public int? DiskSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the root server is installed.
    /// </summary>
    [JsonPropertyName("installed")]
    public bool Installed { get; set; }

    /// <summary>
    /// Gets or sets the maximum traffic limit in gigabytes.
    /// </summary>
    [JsonPropertyName("traffic")]
    public int? Traffic { get; set; }

    /// <summary>
    /// Gets or sets the current traffic value in gigabytes.
    /// </summary>
    [JsonPropertyName("curr_traffic")]
    public double? CurrentTraffic { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the root server is online.
    /// </summary>
    [JsonPropertyName("online")]
    public bool Online { get; set; }

    /// <summary>
    /// Gets or sets the current CPU usage percentage.
    /// </summary>
    [JsonPropertyName("cpu_pc")]
    public int? CpuPercentage { get; set; }

    /// <summary>
    /// Gets or sets the current memory usage.
    /// </summary>
    [JsonPropertyName("curr_memory")]
    public int? CurrentMemory { get; set; }

    /// <summary>
    /// Gets or sets the IP addresses assigned to the server.
    /// </summary>
    [JsonPropertyName("addresses")]
    public List<Address>? Addresses { get; set; }
  }
}
