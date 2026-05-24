using System.Text.Json.Serialization;

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
    [JsonPropertyName("servername")]
    public string? ServerName { get; set; }

    /// <summary>
    /// Gets or sets the TeamSpeak server address.
    /// </summary>
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the TeamSpeak server port.
    /// </summary>
    [JsonPropertyName("port")]
    public int? Port { get; set; }

    /// <summary>
    /// Gets or sets the maximum usable slots.
    /// </summary>
    [JsonPropertyName("slots")]
    public int? Slots { get; set; }

    /// <summary>
    /// Gets or sets the currently connected clients.
    /// </summary>
    [JsonPropertyName("current_slots")]
    public int? CurrentSlots { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the server is online.
    /// </summary>
    [JsonPropertyName("online")]
    public bool Online { get; set; }
  }
}
