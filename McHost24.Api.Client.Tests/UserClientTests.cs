using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class UserClientTests
  {
    [Fact]
    public async Task GetProfileAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson("{\"id\":11,\"name\":\"user\",\"rname\":\"Real User\",\"email\":\"user@example.test\"}");

      // Act
      var profile = await context.Client.User.GetProfileAsync();

      // Assert
      Assert.Equal(11, profile.Id);
      Assert.Equal("user", profile.Name);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "profile");
    }
  }
}
