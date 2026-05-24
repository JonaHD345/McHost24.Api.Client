using Newtonsoft.Json;

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
    [JsonProperty("id")]
    public int? Id { get; set; }

    /// <summary>
    /// Gets or sets the account name.
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the real account name.
    /// </summary>
    [JsonProperty("rname")]
    public string? RealName { get; set; }

    /// <summary>
    /// Gets or sets the account email address.
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the current account balance.
    /// </summary>
    [JsonProperty("money")]
    public decimal? Money { get; set; }

    /// <summary>
    /// Gets or sets the donation URL.
    /// </summary>
    [JsonProperty("donation_url")]
    public string? DonationUrl { get; set; }
  }
}

