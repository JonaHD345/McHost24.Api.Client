using System.Collections.Generic;
using Newtonsoft.Json;

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
    [JsonProperty("cores")]
    public int? Cores { get; set; }

    /// <summary>
    /// Gets or sets the root server memory in mebibytes.
    /// </summary>
    [JsonProperty("memory")]
    public int? Memory { get; set; }

    /// <summary>
    /// Gets or sets the root server disk size in gibibytes.
    /// </summary>
    [JsonProperty("disk_size")]
    public int? DiskSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the root server is installed.
    /// </summary>
    [JsonProperty("installed")]
    public bool Installed { get; set; }

    /// <summary>
    /// Gets or sets the maximum traffic limit in gigabytes.
    /// </summary>
    [JsonProperty("traffic")]
    public int? Traffic { get; set; }

    /// <summary>
    /// Gets or sets the current traffic value in gigabytes.
    /// </summary>
    [JsonProperty("curr_traffic")]
    public double? CurrentTraffic { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the root server is online.
    /// </summary>
    [JsonProperty("online")]
    public bool Online { get; set; }

    /// <summary>
    /// Gets or sets the current CPU usage percentage.
    /// </summary>
    [JsonProperty("cpu_pc")]
    public int? CpuPercentage { get; set; }

    /// <summary>
    /// Gets or sets the current memory usage.
    /// </summary>
    [JsonProperty("curr_memory")]
    public int? CurrentMemory { get; set; }

    /// <summary>
    /// Gets or sets the IP addresses assigned to the server.
    /// </summary>
    [JsonProperty("addresses")]
    public List<Address>? Addresses { get; set; }
  }
}

