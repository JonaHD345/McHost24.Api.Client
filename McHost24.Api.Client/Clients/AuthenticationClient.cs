using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to authentication endpoints.
  /// </summary>
  public sealed class AuthenticationClient
  {
    private readonly McHost24ApiTransport _transport;

    internal AuthenticationClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Authenticates with username and password and stores the returned API token when available.
    /// </summary>
    /// <param name="username">The account username.</param>
    /// <param name="password">The account password.</param>
    /// <param name="tfa">The optional two-factor authentication code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the generated token.</returns>
    public Task<ApiResponse<LoginResult>> LoginAsync(
      string username,
      string password,
      int? tfa = null,
      CancellationToken cancellationToken = default)
    {
      return LoginAsync(
        new LoginRequest
        {
          Username = username,
          Password = password,
          Tfa = tfa
        },
        cancellationToken);
    }

    /// <summary>
    /// Authenticates with the API and stores the returned API token when available.
    /// </summary>
    /// <param name="request">The login request payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the generated token.</returns>
    public async Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      McHost24ApiTransport.ValidateRequiredString(request.Username, nameof(request.Username));
      McHost24ApiTransport.ValidateRequiredString(request.Password, nameof(request.Password));

      var response = await _transport.SendAsync<ApiResponse<LoginResult>>(
        HttpMethod.Post,
        "token",
        request,
        false,
        cancellationToken).ConfigureAwait(false);

      if (!string.IsNullOrWhiteSpace(response.Data?.ApiToken))
      {
        _transport.SetApiToken(response.Data.ApiToken);
      }

      return response;
    }

    /// <summary>
    /// Logs out and invalidates the current API token.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the logout endpoint.</returns>
    public async Task<ApiResponse> LogoutAsync(CancellationToken cancellationToken = default)
    {
      var response = await _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        "logout",
        null,
        true,
        cancellationToken).ConfigureAwait(false);

      if (response.Success)
      {
        _transport.ClearApiToken();
      }

      return response;
    }
  }
}
