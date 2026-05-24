using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class MinecraftServerClientTests
  {
    [Fact]
    public async Task GetAllAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[]"));

      // Act
      var response = await context.Client.MinecraftServers.GetAllAsync();

      // Assert
      Assert.True(response.Success);
      Assert.NotNull(response.Data);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "minecraftServer");
    }

    [Fact]
    public async Task GetStatusAsync_SendsExpectedRequestAndDeserializesNumericBoolean()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"multicraft_id\":99,\"online\":1}"));

      // Act
      var response = await context.Client.MinecraftServers.GetStatusAsync(99);

      // Assert
      Assert.Equal(99, response.Data?.MulticraftId);
      Assert.True(response.Data!.Online);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "minecraftServer/99/status");
    }

    [Fact]
    public async Task GetStatusAsync_WithInvalidServerId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.MinecraftServers.GetStatusAsync(0));

      // Assert
      Assert.Equal("minecraftServerId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task StartAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.MinecraftServers.StartAsync(99);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "minecraftServer/99/start");
    }

    [Fact]
    public async Task StopAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.MinecraftServers.StopAsync(99);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "minecraftServer/99/stop");
    }

    [Fact]
    public async Task RestartAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.MinecraftServers.RestartAsync(99);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "minecraftServer/99/restart");
    }

    [Fact]
    public async Task GetBackupsAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[]"));

      // Act
      var response = await context.Client.MinecraftServers.GetBackupsAsync(99);

      // Assert
      Assert.True(response.Success);
      Assert.NotNull(response.Data);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "minecraftServer/99/backups");
    }

    [Fact]
    public async Task CreateBackupAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.MinecraftServers.CreateBackupAsync(99);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "minecraftServer/99/backups");
    }
  }
}
