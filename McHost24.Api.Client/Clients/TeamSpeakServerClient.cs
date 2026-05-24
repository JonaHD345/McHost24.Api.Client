using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to TeamSpeak server endpoints.
  /// </summary>
  public sealed class TeamSpeakServerClient
  {
    private readonly McHost24ApiTransport _transport;

    internal TeamSpeakServerClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets all TeamSpeak servers owned by the authenticated account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing TeamSpeak servers.</returns>
    public Task<ApiResponse<List<TeamSpeakServer>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<ApiResponse<List<TeamSpeakServer>>>(
        HttpMethod.Get,
        "teamspeak",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets the current status of a TeamSpeak server.
    /// </summary>
    /// <param name="teamSpeakServerId">The TeamSpeak server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the TeamSpeak server status.</returns>
    public Task<ApiResponse<TeamSpeakServer>> GetStatusAsync(int teamSpeakServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(teamSpeakServerId, nameof(teamSpeakServerId));

      return _transport.SendAsync<ApiResponse<TeamSpeakServer>>(
        HttpMethod.Get,
        $"teamspeak/{McHost24ApiTransport.FormatId(teamSpeakServerId)}/status",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Starts a TeamSpeak server.
    /// </summary>
    /// <param name="teamSpeakServerId">The TeamSpeak server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the start endpoint.</returns>
    public Task<ApiResponse> StartAsync(int teamSpeakServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(teamSpeakServerId, nameof(teamSpeakServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"teamspeak/{McHost24ApiTransport.FormatId(teamSpeakServerId)}/start",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Stops a TeamSpeak server.
    /// </summary>
    /// <param name="teamSpeakServerId">The TeamSpeak server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the stop endpoint.</returns>
    public Task<ApiResponse> StopAsync(int teamSpeakServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(teamSpeakServerId, nameof(teamSpeakServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"teamspeak/{McHost24ApiTransport.FormatId(teamSpeakServerId)}/stop",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Restarts a TeamSpeak server.
    /// </summary>
    /// <param name="teamSpeakServerId">The TeamSpeak server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the restart endpoint.</returns>
    public Task<ApiResponse> RestartAsync(int teamSpeakServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(teamSpeakServerId, nameof(teamSpeakServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"teamspeak/{McHost24ApiTransport.FormatId(teamSpeakServerId)}/restart",
        null,
        true,
        cancellationToken);
    }
  }
}
