using System;
using System.Net.Http;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides strongly typed access to the MC-HOST24 public API.
  /// </summary>
  public sealed class McHost24Client : IDisposable
  {
    private readonly McHost24ApiTransport _transport;

    /// <summary>
    /// Initializes a new instance of the <see cref="McHost24Client"/> class with a new <see cref="HttpClient"/>.
    /// </summary>
    public McHost24Client()
      : this(new HttpClient(), new McHost24ClientOptions(), true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="McHost24Client"/> class with an API token.
    /// </summary>
    /// <param name="apiToken">The API token sent through the Authorization header.</param>
    public McHost24Client(string apiToken)
      : this(new McHost24ClientOptions { ApiToken = apiToken })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="McHost24Client"/> class with custom options.
    /// </summary>
    /// <param name="options">The client options.</param>
    public McHost24Client(McHost24ClientOptions options)
      : this(new HttpClient(), options, true)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="McHost24Client"/> class with an existing <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="httpClient">The HTTP client used to send API requests.</param>
    /// <param name="options">The optional client options.</param>
    public McHost24Client(HttpClient httpClient, McHost24ClientOptions? options = null)
      : this(httpClient, options ?? new McHost24ClientOptions(), false)
    {
    }

    private McHost24Client(HttpClient httpClient, McHost24ClientOptions options, bool disposeHttpClient)
    {
      _transport = new McHost24ApiTransport(httpClient, options, disposeHttpClient);

      Authentication = new AuthenticationClient(_transport);
      MinecraftServers = new MinecraftServerClient(_transport);
      TeamSpeakServers = new TeamSpeakServerClient(_transport);
      Domains = new DomainClient(_transport);
      RootServers = new RootServerClient(_transport);
      User = new UserClient(_transport);
      Services = new ServiceClient(_transport);
      Tickets = new TicketClient(_transport);
    }

    /// <summary>
    /// Gets the API token currently used for authenticated requests.
    /// </summary>
    public string? ApiToken => _transport.ApiToken;

    /// <summary>
    /// Gets the authentication API.
    /// </summary>
    public AuthenticationClient Authentication { get; }

    /// <summary>
    /// Gets the Minecraft server API.
    /// </summary>
    public MinecraftServerClient MinecraftServers { get; }

    /// <summary>
    /// Gets the TeamSpeak server API.
    /// </summary>
    public TeamSpeakServerClient TeamSpeakServers { get; }

    /// <summary>
    /// Gets the domain API.
    /// </summary>
    public DomainClient Domains { get; }

    /// <summary>
    /// Gets the root server API.
    /// </summary>
    public RootServerClient RootServers { get; }

    /// <summary>
    /// Gets the user API.
    /// </summary>
    public UserClient User { get; }

    /// <summary>
    /// Gets the service API.
    /// </summary>
    public ServiceClient Services { get; }

    /// <summary>
    /// Gets the ticket API.
    /// </summary>
    public TicketClient Tickets { get; }

    /// <summary>
    /// Sets the API token used for authenticated requests.
    /// </summary>
    /// <param name="apiToken">The API token sent through the Authorization header.</param>
    public void SetApiToken(string apiToken)
    {
      _transport.SetApiToken(apiToken);
    }

    /// <summary>
    /// Clears the API token used for authenticated requests.
    /// </summary>
    public void ClearApiToken()
    {
      _transport.ClearApiToken();
    }

    /// <summary>
    /// Releases resources owned by the client.
    /// </summary>
    public void Dispose()
    {
      _transport.Dispose();
    }
  }
}
