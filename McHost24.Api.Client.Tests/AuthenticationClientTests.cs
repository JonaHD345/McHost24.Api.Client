using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class AuthenticationClientTests
  {
    [Fact]
    public async Task LoginAsync_WithCredentials_SendsTokenRequestAndStoresReturnedToken()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"api_token\":\"returned-token\"}"));

      // Act
      var response = await context.Client.Authentication.LoginAsync("user", "secret", 123456);

      // Assert
      Assert.Equal("returned-token", response.Data?.ApiToken);
      Assert.Equal("returned-token", context.Client.ApiToken);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "token", authenticated: false);
      Assert.Equal("application/json", request.ContentType);

      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("user", body.GetProperty("username").GetString());
      Assert.Equal("secret", body.GetProperty("password").GetString());
      Assert.Equal(123456, body.GetProperty("tfa").GetInt32());
    }

    [Fact]
    public async Task LoginAsync_WithRequest_SendsTokenRequestAndStoresReturnedToken()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);
      var loginRequest = new LoginRequest
      {
        Username = "user",
        Password = "secret"
      };
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"api_token\":\"returned-token\"}"));

      // Act
      var response = await context.Client.Authentication.LoginAsync(loginRequest);

      // Assert
      Assert.Equal("returned-token", response.Data?.ApiToken);
      Assert.Equal("returned-token", context.Client.ApiToken);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "token", authenticated: false);
      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("user", body.GetProperty("username").GetString());
      Assert.Equal("secret", body.GetProperty("password").GetString());
      Assert.False(body.TryGetProperty("tfa", out _));
    }

    [Fact]
    public async Task LoginAsync_WithNullRequest_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => context.Client.Authentication.LoginAsync(null!));

      // Assert
      Assert.Equal("request", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task LoginAsync_WithMissingUsername_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);
      var loginRequest = new LoginRequest
      {
        Password = "secret"
      };

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => context.Client.Authentication.LoginAsync(loginRequest));

      // Assert
      Assert.Equal(nameof(LoginRequest.Username), exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task LoginAsync_WithMissingPassword_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient(null);
      var loginRequest = new LoginRequest
      {
        Username = "user"
      };

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => context.Client.Authentication.LoginAsync(loginRequest));

      // Assert
      Assert.Equal(nameof(LoginRequest.Password), exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task LogoutAsync_WithSuccessfulResponse_SendsLogoutRequestAndClearsToken()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Authentication.LogoutAsync();

      // Assert
      Assert.True(response.Success);
      Assert.Null(context.Client.ApiToken);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "logout");
    }

    [Fact]
    public async Task LogoutAsync_WithUnsuccessfulApiResponse_DoesNotClearToken()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson(false));

      // Act
      var response = await context.Client.Authentication.LogoutAsync();

      // Assert
      Assert.False(response.Success);
      Assert.Equal(ClientTestHost.ApiToken, context.Client.ApiToken);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "logout");
    }
  }
}
