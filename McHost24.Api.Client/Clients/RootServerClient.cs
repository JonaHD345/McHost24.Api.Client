using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to root server endpoints.
  /// </summary>
  public sealed class RootServerClient
  {
    private readonly McHost24ApiTransport _transport;

    internal RootServerClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets all root servers owned by the authenticated account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing root servers.</returns>
    public Task<ApiResponse<List<RootServer>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<ApiResponse<List<RootServer>>>(
        HttpMethod.Get,
        "vserver",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets the current status of a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the root server status.</returns>
    public Task<ApiResponse<RootServer>> GetStatusAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse<RootServer>>(
        HttpMethod.Get,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/status",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Starts a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the start endpoint.</returns>
    public Task<ApiResponse> StartAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/start",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Shuts down a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the shutdown endpoint.</returns>
    public Task<ApiResponse> ShutdownAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/shutdown",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Stops a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the stop endpoint.</returns>
    public Task<ApiResponse> StopAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/stop",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Restarts a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the restart endpoint.</returns>
    public Task<ApiResponse> RestartAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/restart",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets backups for a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing root server backups.</returns>
    public Task<ApiResponse<List<RootServerBackup>>> GetBackupsAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse<List<RootServerBackup>>>(
        HttpMethod.Get,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/backups",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Creates a new backup for a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the backup endpoint.</returns>
    public Task<ApiResponse> CreateBackupAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/backups",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Restores a root server backup.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="rootServerBackupId">The root server backup database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the restore endpoint.</returns>
    public Task<ApiResponse> RestoreBackupAsync(int rootServerId, int rootServerBackupId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));
      McHost24ApiTransport.ValidatePositiveId(rootServerBackupId, nameof(rootServerBackupId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/restore/{McHost24ApiTransport.FormatId(rootServerBackupId)}",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Deletes a root server backup.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="rootServerBackupId">The root server backup database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the delete endpoint.</returns>
    public Task<ApiResponse> DeleteBackupAsync(int rootServerId, int rootServerBackupId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));
      McHost24ApiTransport.ValidatePositiveId(rootServerBackupId, nameof(rootServerBackupId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/backup/{McHost24ApiTransport.FormatId(rootServerBackupId)}/delete",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets the VNC console URL for a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the VNC console URL.</returns>
    public Task<ApiResponse<RootServerVncConsole>> GetVncAsync(int rootServerId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));

      return _transport.SendAsync<ApiResponse<RootServerVncConsole>>(
        HttpMethod.Get,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/vnc",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets RRD statistics for a root server.
    /// </summary>
    /// <param name="rootServerId">The root server database id.</param>
    /// <param name="timeframe">The statistics timeframe. Use values from <see cref="RootServerRrdTimeframes"/>.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing root server RRD statistics.</returns>
    public Task<ApiResponse<RootServerRrdData>> GetRrdDataAsync(
      int rootServerId,
      string timeframe,
      CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(rootServerId, nameof(rootServerId));
      McHost24ApiTransport.ValidateRequiredString(timeframe, nameof(timeframe));

      return _transport.SendAsync<ApiResponse<RootServerRrdData>>(
        HttpMethod.Get,
        $"vserver/{McHost24ApiTransport.FormatId(rootServerId)}/rrddata?tf={Uri.EscapeDataString(timeframe)}",
        null,
        true,
        cancellationToken);
    }
  }
}
