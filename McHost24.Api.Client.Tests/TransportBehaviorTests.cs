using System.Net;
using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class TransportBehaviorTests
  {
    [Fact]
    public async Task AuthenticatedRequest_WithoutToken_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);

      // Act
      var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => context.Client.User.GetProfileAsync());

      // Assert
      Assert.Equal("An API token is required for this MC-HOST24 API endpoint.", exception.Message);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task AuthenticatedRequest_WithDefaultAuthorizationHeader_SendsRequest()
    {
      // Arrange
      var handler = new RecordingHttpMessageHandler();
      using var httpClient = new HttpClient(handler);
      httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "default-token");
      using var client = new McHost24Client(
        httpClient,
        new McHost24ClientOptions
        {
          BaseAddress = new Uri("https://api.test")
        });
      handler.EnqueueJson("{}");

      // Act
      var profile = await client.User.GetProfileAsync();

      // Assert
      Assert.NotNull(profile);

      var request = handler.AssertSingleRequest(HttpMethod.Get, "profile", authorization: "default-token");
      Assert.Equal("default-token", request.Authorization);
    }

    [Fact]
    public async Task SendAsync_WithUnsuccessfulStatusCode_ThrowsApiException()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson("{\"error\":\"denied\"}", HttpStatusCode.Forbidden);

      // Act
      var exception = await Assert.ThrowsAsync<McHost24ApiException>(() => context.Client.User.GetProfileAsync());

      // Assert
      Assert.Equal(HttpStatusCode.Forbidden, exception.StatusCode);
      Assert.Equal("{\"error\":\"denied\"}", exception.ResponseContent);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "profile");
    }

    [Fact]
    public async Task SendAsync_WithEmptyResponse_ThrowsApiException()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(string.Empty);

      // Act
      var exception = await Assert.ThrowsAsync<McHost24ApiException>(() => context.Client.User.GetProfileAsync());

      // Assert
      Assert.Equal("The MC-HOST24 API returned an empty response.", exception.Message);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "profile");
    }

    [Fact]
    public async Task SendAsync_WithInvalidJson_ThrowsApiException()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson("not json");

      // Act
      var exception = await Assert.ThrowsAsync<McHost24ApiException>(() => context.Client.User.GetProfileAsync());

      // Assert
      Assert.Equal("The MC-HOST24 API response could not be deserialized.", exception.Message);
      Assert.NotNull(exception.InnerException);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "profile");
    }
  }
}
