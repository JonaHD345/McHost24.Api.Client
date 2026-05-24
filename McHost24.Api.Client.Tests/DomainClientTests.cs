using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class DomainClientTests
  {
    [Fact]
    public async Task GetAllAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[]"));

      // Act
      var response = await context.Client.Domains.GetAllAsync();

      // Assert
      Assert.True(response.Success);
      Assert.NotNull(response.Data);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "domain");
    }

    [Fact]
    public async Task GetAvailableRecordTypesAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"A\":\"IPv4\"}"));

      // Act
      var response = await context.Client.Domains.GetAvailableRecordTypesAsync();

      // Assert
      Assert.Equal("IPv4", response.Data?["A"]);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "domain/availableRecords");
    }

    [Fact]
    public async Task GetInfoAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"domain\":{\"sld\":\"example\",\"tld\":\"de\"},\"records\":[],\"emails\":[]}"));

      // Act
      var response = await context.Client.Domains.GetInfoAsync(42);

      // Assert
      Assert.Equal("example", response.Data?.Domain?.Sld);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "domain/42/info");
    }

    [Fact]
    public async Task GetInfoAsync_WithInvalidDomainId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.Domains.GetInfoAsync(0));

      // Assert
      Assert.Equal("domainId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task CreateDnsRecordAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var record = new DomainRecord
      {
        Sld = "www",
        Type = DomainRecordTypes.A,
        Target = "192.0.2.1"
      };
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"id\":5,\"sld\":\"www\",\"type\":\"A\",\"target\":\"192.0.2.1\"}"));

      // Act
      var response = await context.Client.Domains.CreateDnsRecordAsync(42, record);

      // Assert
      Assert.Equal(5, response.Data?.Id);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "domain/42/dns");
      Assert.Equal("application/json", request.ContentType);

      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("www", body.GetProperty("sld").GetString());
      Assert.Equal(DomainRecordTypes.A, body.GetProperty("type").GetString());
      Assert.Equal("192.0.2.1", body.GetProperty("target").GetString());
    }

    [Fact]
    public async Task CreateDnsRecordAsync_WithNullRecord_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => context.Client.Domains.CreateDnsRecordAsync(42, null!));

      // Assert
      Assert.Equal("record", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task DeleteDnsRecordAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Domains.DeleteDnsRecordAsync(42, 7);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Delete, "domain/42/dns/7");
    }

    [Fact]
    public async Task CreateEmailAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var email = new DomainEmail
      {
        Username = "mail",
        Password = "secret"
      };
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"username\":\"mail\"}"));

      // Act
      var response = await context.Client.Domains.CreateEmailAsync(42, email);

      // Assert
      Assert.Equal("mail", response.Data?.Username);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "domain/42/email");
      Assert.Equal("application/json", request.ContentType);

      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("mail", body.GetProperty("username").GetString());
      Assert.Equal("secret", body.GetProperty("password").GetString());
    }

    [Fact]
    public async Task CreateEmailAsync_WithNullEmail_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => context.Client.Domains.CreateEmailAsync(42, null!));

      // Assert
      Assert.Equal("email", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task DeleteEmailAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Domains.DeleteEmailAsync(42, 8);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Delete, "domain/42/email/8");
    }
  }
}
