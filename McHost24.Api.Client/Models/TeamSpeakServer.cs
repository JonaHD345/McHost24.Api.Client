using Newtonsoft.Json;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents a TeamSpeak server.
  /// </summary>
  public sealed class TeamSpeakServer : NamedProduct
  {
    /// <summary>
    /// Gets or sets the TeamSpeak server name.
    /// </summary>
    [JsonProperty("servername")]
    public string? ServerName { get; set; }

    /// <summary>
    /// Gets or sets the TeamSpeak server address.
    /// </summary>
    [JsonProperty("ip_address")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the TeamSpeak server port.
    /// </summary>
    [JsonProperty("port")]
    public int? Port { get; set; }

    /// <summary>
    /// Gets or sets the maximum usable slots.
    /// </summary>
    [JsonProperty("slots")]
    public int? Slots { get; set; }

    /// <summary>
    /// Gets or sets the currently connected clients.
    /// </summary>
    [JsonProperty("current_slots")]
    public int? CurrentSlots { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the server is online.
    /// </summary>
    [JsonProperty("online")]
    public bool Online { get; set; }
  }
}

