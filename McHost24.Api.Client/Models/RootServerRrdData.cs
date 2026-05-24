using System.Collections.Generic;
using System.Text.Json.Serialization;

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
    [JsonPropertyName("time")]
    public List<string>? Time { get; set; }

    /// <summary>
    /// Gets or sets CPU metric values.
    /// </summary>
    [JsonPropertyName("cpu")]
    public List<double>? Cpu { get; set; }

    /// <summary>
    /// Gets or sets memory metric values.
    /// </summary>
    [JsonPropertyName("mem")]
    public List<double>? Memory { get; set; }

    /// <summary>
    /// Gets or sets disk read metric values.
    /// </summary>
    [JsonPropertyName("diskread")]
    public List<double>? DiskRead { get; set; }

    /// <summary>
    /// Gets or sets disk write metric values.
    /// </summary>
    [JsonPropertyName("diskwrite")]
    public List<double>? DiskWrite { get; set; }

    /// <summary>
    /// Gets or sets incoming network metric values.
    /// </summary>
    [JsonPropertyName("netin")]
    public List<double>? NetIn { get; set; }

    /// <summary>
    /// Gets or sets outgoing network metric values.
    /// </summary>
    [JsonPropertyName("netout")]
    public List<double>? NetOut { get; set; }

    /// <summary>
    /// Gets or sets the maximum memory value.
    /// </summary>
    [JsonPropertyName("maxmem")]
    public double? MaxMemory { get; set; }
  }
}
