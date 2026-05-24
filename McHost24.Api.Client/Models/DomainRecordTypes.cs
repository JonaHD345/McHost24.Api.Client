namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides constants for supported domain record types.
  /// </summary>
  public static class DomainRecordTypes
  {
    /// <summary>
    /// Represents an A record.
    /// </summary>
    public const string A = "A";

    /// <summary>
    /// Represents an AAAA record.
    /// </summary>
    public const string Aaaa = "AAAA";

    /// <summary>
    /// Represents a CNAME record.
    /// </summary>
    public const string Cname = "CNAME";

    /// <summary>
    /// Represents an MX record.
    /// </summary>
    public const string Mx = "MX";

    /// <summary>
    /// Represents an NS record.
    /// </summary>
    public const string Ns = "NS";

    /// <summary>
    /// Represents an SRV record.
    /// </summary>
    public const string Srv = "SRV";

    /// <summary>
    /// Represents a TXT record.
    /// </summary>
    public const string Txt = "TXT";

    /// <summary>
    /// Represents a CAA record.
    /// </summary>
    public const string Caa = "CAA";

    /// <summary>
    /// Represents an HTTP frame redirect record.
    /// </summary>
    public const string HttpFrame = "HTTP_F";

    /// <summary>
    /// Represents an HTTPS frame redirect record.
    /// </summary>
    public const string HttpsFrame = "HTTPS_F";

    /// <summary>
    /// Represents an HTTP header redirect record.
    /// </summary>
    public const string HttpHeader = "HTTP_H";

    /// <summary>
    /// Represents an HTTPS header redirect record.
    /// </summary>
    public const string HttpsHeader = "HTTPS_H";
  }
}
