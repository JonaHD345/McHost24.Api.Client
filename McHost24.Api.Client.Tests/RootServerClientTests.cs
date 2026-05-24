using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class RootServerClientTests
  {
    [Fact]
    public async Task GetAllAsync_SendsExpectedRequestAndDeserializesSingleObjectsAsLists()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"product_name\":\"Root\",\"installed\":\"1\",\"online\":0,\"addresses\":{\"ip\":\"192.0.2.10\",\"rdns\":\"host\"}}"));

      // Act
      var response = await context.Client.RootServers.GetAllAsync();

      // Assert
      var rootServer = Assert.Single(response.Data!);
      Assert.Equal("Root", rootServer.ProductName);
      Assert.True(rootServer.Installed);
      Assert.False(rootServer.Online);

      var address = Assert.Single(rootServer.Addresses!);
      Assert.Equal("192.0.2.10", address.Ip);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "vserver");
    }

    [Fact]
    public async Task GetStatusAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"product_name\":\"Root\",\"online\":true}"));

      // Act
      var response = await context.Client.RootServers.GetStatusAsync(17);

      // Assert
      Assert.Equal("Root", response.Data?.ProductName);
      Assert.True(response.Data!.Online);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "vserver/17/status");
    }

    [Fact]
    public async Task GetStatusAsync_WithInvalidServerId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.RootServers.GetStatusAsync(0));

      // Assert
      Assert.Equal("rootServerId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task StartAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.StartAsync(17);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/start");
    }

    [Fact]
    public async Task ShutdownAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.ShutdownAsync(17);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/shutdown");
    }

    [Fact]
    public async Task StopAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.StopAsync(17);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/stop");
    }

    [Fact]
    public async Task RestartAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.RestartAsync(17);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/restart");
    }

    [Fact]
    public async Task GetBackupsAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[]"));

      // Act
      var response = await context.Client.RootServers.GetBackupsAsync(17);

      // Assert
      Assert.True(response.Success);
      Assert.NotNull(response.Data);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "vserver/17/backups");
    }

    [Fact]
    public async Task CreateBackupAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.CreateBackupAsync(17);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/backups");
    }

    [Fact]
    public async Task RestoreBackupAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.RestoreBackupAsync(17, 3);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/restore/3");
    }

    [Fact]
    public async Task RestoreBackupAsync_WithInvalidBackupId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.RootServers.RestoreBackupAsync(17, 0));

      // Assert
      Assert.Equal("rootServerBackupId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task DeleteBackupAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.RootServers.DeleteBackupAsync(17, 3);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "vserver/17/backup/3/delete");
    }

    [Fact]
    public async Task GetVncAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"url\":\"https://console.test\"}"));

      // Act
      var response = await context.Client.RootServers.GetVncAsync(17);

      // Assert
      Assert.Equal("https://console.test", response.Data?.Url);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "vserver/17/vnc");
    }

    [Fact]
    public async Task GetRrdDataAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"time\":[\"10:00\"],\"cpu\":[1.5],\"maxmem\":4096}"));

      // Act
      var response = await context.Client.RootServers.GetRrdDataAsync(17, "last hour");

      // Assert
      Assert.Equal(4096d, response.Data?.MaxMemory);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "vserver/17/rrddata?tf=last%20hour");
    }

    [Fact]
    public async Task GetRrdDataAsync_WithMissingTimeframe_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => context.Client.RootServers.GetRrdDataAsync(17, " "));

      // Assert
      Assert.Equal("timeframe", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }
  }
}
