using System.Text.Json.Serialization;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Represents account profile information.
  /// </summary>
  public sealed class Profile
  {
    /// <summary>
    /// Gets or sets the MC-HOST24 database id.
    /// </summary>
    [JsonPropertyName("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the account name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the real account name.
    /// </summary>
    [JsonPropertyName("rname")]
    public string? RealName { get; set; }

    /// <summary>
    /// Gets or sets the account email address.
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the current account balance.
    /// </summary>
    [JsonPropertyName("money")]
    public decimal? Money { get; set; }

    /// <summary>
    /// Gets or sets the donation URL.
    /// </summary>
    [JsonPropertyName("donation_url")]
    public string? DonationUrl { get; set; }
  }
}
