using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class TeamSpeakServerClientTests
  {
    [Fact]
    public async Task GetAllAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[]"));

      // Act
      var response = await context.Client.TeamSpeakServers.GetAllAsync();

      // Assert
      Assert.True(response.Success);
      Assert.NotNull(response.Data);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "teamspeak");
    }

    [Fact]
    public async Task GetStatusAsync_SendsExpectedRequestAndDeserializesStringBoolean()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"servername\":\"Voice\",\"online\":\"true\"}"));

      // Act
      var response = await context.Client.TeamSpeakServers.GetStatusAsync(25);

      // Assert
      Assert.Equal("Voice", response.Data?.ServerName);
      Assert.True(response.Data!.Online);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "teamspeak/25/status");
    }

    [Fact]
    public async Task GetStatusAsync_WithInvalidServerId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.TeamSpeakServers.GetStatusAsync(0));

      // Assert
      Assert.Equal("teamSpeakServerId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task StartAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.TeamSpeakServers.StartAsync(25);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "teamspeak/25/start");
    }

    [Fact]
    public async Task StopAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.TeamSpeakServers.StopAsync(25);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "teamspeak/25/stop");
    }

    [Fact]
    public async Task RestartAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.TeamSpeakServers.RestartAsync(25);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "teamspeak/25/restart");
    }
  }
}
