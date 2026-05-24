using System.Collections.Generic;
using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents root server RRD statistics.
  /// </summary>
  public sealed class RootServerRrdData
  {
    /// <summary>
    /// Gets or sets the metric timestamps.
    /// </summary>
    [JsonProperty("time")]
    public List<string>? Time { get; set; }

    /// <summary>
    /// Gets or sets CPU metric values.
    /// </summary>
    [JsonProperty("cpu")]
    public List<double>? Cpu { get; set; }

    /// <summary>
    /// Gets or sets memory metric values.
    /// </summary>
    [JsonProperty("mem")]
    public List<double>? Memory { get; set; }

    /// <summary>
    /// Gets or sets disk read metric values.
    /// </summary>
    [JsonProperty("diskread")]
    public List<double>? DiskRead { get; set; }

    /// <summary>
    /// Gets or sets disk write metric values.
    /// </summary>
    [JsonProperty("diskwrite")]
    public List<double>? DiskWrite { get; set; }

    /// <summary>
    /// Gets or sets incoming network metric values.
    /// </summary>
    [JsonProperty("netin")]
    public List<double>? NetIn { get; set; }

    /// <summary>
    /// Gets or sets outgoing network metric values.
    /// </summary>
    [JsonProperty("netout")]
    public List<double>? NetOut { get; set; }

    /// <summary>
    /// Gets or sets the maximum memory value.
    /// </summary>
    [JsonProperty("maxmem")]
    public double? MaxMemory { get; set; }
  }
}

