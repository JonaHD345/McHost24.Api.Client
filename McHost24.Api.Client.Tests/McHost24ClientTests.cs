using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class McHost24ClientTests
  {
    [Fact]
    public void Constructor_Default_InitializesEndpointClients()
    {
      // Arrange

      // Act
      using var client = new McHost24Client();

      // Assert
      Assert.Null(client.ApiToken);
      Assert.NotNull(client.Authentication);
      Assert.NotNull(client.MinecraftServers);
      Assert.NotNull(client.TeamSpeakServers);
      Assert.NotNull(client.Domains);
      Assert.NotNull(client.RootServers);
      Assert.NotNull(client.User);
      Assert.NotNull(client.Services);
      Assert.NotNull(client.Tickets);
    }

    [Fact]
    public void Constructor_WithApiToken_SetsApiToken()
    {
      // Arrange
      var apiToken = "constructor-token";

      // Act
      using var client = new McHost24Client(apiToken);

      // Assert
      Assert.Equal(apiToken, client.ApiToken);
    }

    [Fact]
    public void Constructor_WithOptions_UsesOptions()
    {
      // Arrange
      var options = new McHost24ClientOptions
      {
        ApiToken = "options-token"
      };

      // Act
      using var client = new McHost24Client(options);

      // Assert
      Assert.Equal(options.ApiToken, client.ApiToken);
    }

    [Fact]
    public async Task Constructor_WithHttpClient_UsesOptionsForRequests()
    {
      // Arrange
      var handler = new RecordingHttpMessageHandler();
      using var httpClient = new HttpClient(handler);
      using var client = new McHost24Client(
        httpClient,
        new McHost24ClientOptions
        {
          BaseAddress = new Uri("https://api.test"),
          ApiToken = ClientTestHost.ApiToken
        });
      handler.EnqueueJson("{}");

      // Act
      await client.User.GetProfileAsync();

      // Assert
      handler.AssertSingleRequest(HttpMethod.Get, "profile");
    }

    [Fact]
    public void Constructor_WithNullHttpClient_Throws()
    {
      // Arrange

      // Act
      var exception = Assert.Throws<ArgumentNullException>(() => new McHost24Client((HttpClient)null!));

      // Assert
      Assert.Equal("httpClient", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithNullOptions_Throws()
    {
      // Arrange

      // Act
      var exception = Assert.Throws<ArgumentNullException>(() => new McHost24Client((McHost24ClientOptions)null!));

      // Assert
      Assert.Equal("options", exception.ParamName);
    }

    [Fact]
    public void Constructor_WithRelativeBaseAddress_Throws()
    {
      // Arrange
      var options = new McHost24ClientOptions
      {
        BaseAddress = new Uri("api/v1", UriKind.Relative)
      };

      // Act
      var exception = Assert.Throws<ArgumentException>(() => new McHost24Client(options));

      // Assert
      Assert.Equal("uri", exception.ParamName);
    }

    [Fact]
    public void SetApiToken_WithToken_UpdatesApiToken()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);

      // Act
      context.Client.SetApiToken("new-token");

      // Assert
      Assert.Equal("new-token", context.Client.ApiToken);
    }

    [Fact]
    public void SetApiToken_WithWhitespace_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = Assert.Throws<ArgumentException>(() => context.Client.SetApiToken(" "));

      // Assert
      Assert.Equal("apiToken", exception.ParamName);
    }

    [Fact]
    public void ClearApiToken_WithToken_ClearsApiToken()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      context.Client.ClearApiToken();

      // Assert
      Assert.Null(context.Client.ApiToken);
    }

    [Fact]
    public async Task Dispose_PreventsFurtherRequests()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Client.Dispose();

      // Act
      var exception = await Assert.ThrowsAsync<ObjectDisposedException>(() => context.Client.User.GetProfileAsync());

      // Assert
      Assert.Equal(nameof(McHost24Client), exception.ObjectName);
      Assert.Empty(context.Handler.Requests);
    }
  }
}
