using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class ServiceClientTests
  {
    [Fact]
    public async Task GetRenewPriceAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"runtimes\":[{\"runtime\":\"30\",\"price\":1.23}]}"));

      // Act
      var response = await context.Client.Services.GetRenewPriceAsync(55);

      // Assert
      var runtime = Assert.Single(response.Data!.Runtimes!);
      Assert.Equal("30", runtime.Runtime);
      Assert.Equal(1.23m, runtime.Price);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "service/55/price");
    }

    [Fact]
    public async Task GetRenewPriceAsync_WithInvalidServiceId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.Services.GetRenewPriceAsync(0));

      // Assert
      Assert.Equal("serviceId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task RenewAsync_WithRuntime_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Services.RenewAsync(55, "30");

      // Assert
      Assert.True(response.Success);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "service/55/renew");
      Assert.Equal("application/json", request.ContentType);

      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("30", body.GetProperty("runtime").GetString());
    }

    [Fact]
    public async Task RenewAsync_WithRequest_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var renewRequest = new ServiceRenewRequest
      {
        Runtime = "90"
      };
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Services.RenewAsync(55, renewRequest);

      // Assert
      Assert.True(response.Success);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "service/55/renew");
      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("90", body.GetProperty("runtime").GetString());
    }

    [Fact]
    public async Task RenewAsync_WithNullRequest_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => context.Client.Services.RenewAsync(55, (ServiceRenewRequest)null!));

      // Assert
      Assert.Equal("request", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task RenewAsync_WithMissingRuntime_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var renewRequest = new ServiceRenewRequest();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => context.Client.Services.RenewAsync(55, renewRequest));

      // Assert
      Assert.Equal(nameof(ServiceRenewRequest.Runtime), exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }
  }
}
