using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to Minecraft server endpoints.
  /// </summary>
  public sealed class MinecraftServerClient
  {
    private readonly McHost24ApiTransport _transport;

    internal MinecraftServerClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets all Minecraft servers owned by the authenticated account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing Minecraft servers.</returns>
    public Task<ApiResponse<List<MinecraftServer>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<ApiResponse<List<MinecraftServer>>>(
        HttpMethod.Get,
        "minecraftServer",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets the current status of a Minecraft server.
    /// </summary>
    /// <param name="minecraftServerId">The Minecraft server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the Minecraft server status.</returns>
    public Task<ApiResponse<MinecraftServer>> GetStatusAsync(int minecraftServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(minecraftServerId, nameof(minecraftServerId));

      return _transport.SendAsync<ApiResponse<MinecraftServer>>(
        HttpMethod.Get,
        $"minecraftServer/{McHost24ApiTransport.FormatId(minecraftServerId)}/status",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Starts a Minecraft server.
    /// </summary>
    /// <param name="minecraftServerId">The Minecraft server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the start endpoint.</returns>
    public Task<ApiResponse> StartAsync(int minecraftServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(minecraftServerId, nameof(minecraftServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"minecraftServer/{McHost24ApiTransport.FormatId(minecraftServerId)}/start",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Stops a Minecraft server.
    /// </summary>
    /// <param name="minecraftServerId">The Minecraft server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the stop endpoint.</returns>
    public Task<ApiResponse> StopAsync(int minecraftServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(minecraftServerId, nameof(minecraftServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"minecraftServer/{McHost24ApiTransport.FormatId(minecraftServerId)}/stop",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Restarts a Minecraft server.
    /// </summary>
    /// <param name="minecraftServerId">The Minecraft server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the restart endpoint.</returns>
    public Task<ApiResponse> RestartAsync(int minecraftServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(minecraftServerId, nameof(minecraftServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"minecraftServer/{McHost24ApiTransport.FormatId(minecraftServerId)}/restart",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets backups for a Minecraft server.
    /// </summary>
    /// <param name="minecraftServerId">The Minecraft server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing Minecraft server backups.</returns>
    public Task<ApiResponse<List<MinecraftServerBackup>>> GetBackupsAsync(int minecraftServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(minecraftServerId, nameof(minecraftServerId));

      return _transport.SendAsync<ApiResponse<List<MinecraftServerBackup>>>(
        HttpMethod.Get,
        $"minecraftServer/{McHost24ApiTransport.FormatId(minecraftServerId)}/backups",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Creates a new backup for a Minecraft server.
    /// </summary>
    /// <param name="minecraftServerId">The Minecraft server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the backup endpoint.</returns>
    public Task<ApiResponse> CreateBackupAsync(int minecraftServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(minecraftServerId, nameof(minecraftServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"minecraftServer/{McHost24ApiTransport.FormatId(minecraftServerId)}/backups",
        null,
        true,
        cancellationToken);
    }
  }
}
