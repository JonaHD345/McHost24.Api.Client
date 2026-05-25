using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for Minecraft server endpoints.
  /// </summary>
  internal sealed class MinecraftServerConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="MinecraftServerConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call Minecraft server endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public MinecraftServerConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for Minecraft server actions.
    }

    /// <summary>
    /// Retrieves and prints all Minecraft servers.
    /// </summary>
    public Task ListAsync()
    {
      // List servers first so users can find ids for status and control actions.
      return PrintAsync(Client.MinecraftServers.GetAllAsync());
    }

    /// <summary>
    /// Prompts for a Minecraft server id and prints its status.
    /// </summary>
    public Task GetStatusAsync()
    {
      // The status endpoint returns live server information for one server.
      int serverId = Ui.ReadPositiveInt("Minecraft server id");
      return PrintAsync(Client.MinecraftServers.GetStatusAsync(serverId));
    }

    /// <summary>
    /// Prompts for a Minecraft server id and starts the server.
    /// </summary>
    public Task StartAsync()
    {
      // Confirm before changing the server power state.
      int serverId = Ui.ReadPositiveInt("Minecraft server id");

      if (!Ui.Confirm("Send start request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed start request.
      return PrintAsync(Client.MinecraftServers.StartAsync(serverId));
    }

    /// <summary>
    /// Prompts for a Minecraft server id and stops the server.
    /// </summary>
    public Task StopAsync()
    {
      // Confirm before changing the server power state.
      int serverId = Ui.ReadPositiveInt("Minecraft server id");

      if (!Ui.Confirm("Send stop request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed stop request.
      return PrintAsync(Client.MinecraftServers.StopAsync(serverId));
    }

    /// <summary>
    /// Prompts for a Minecraft server id and restarts the server.
    /// </summary>
    public Task RestartAsync()
    {
      // Confirm before restarting a running service.
      int serverId = Ui.ReadPositiveInt("Minecraft server id");

      if (!Ui.Confirm("Send restart request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed restart request.
      return PrintAsync(Client.MinecraftServers.RestartAsync(serverId));
    }

    /// <summary>
    /// Prompts for a Minecraft server id and prints its backups.
    /// </summary>
    public Task ListBackupsAsync()
    {
      // Backup listing helps users decide whether a new backup is needed.
      int serverId = Ui.ReadPositiveInt("Minecraft server id");
      return PrintAsync(Client.MinecraftServers.GetBackupsAsync(serverId));
    }

    /// <summary>
    /// Prompts for a Minecraft server id and creates a backup.
    /// </summary>
    public Task CreateBackupAsync()
    {
      // Confirm before triggering a potentially long-running backup job.
      int serverId = Ui.ReadPositiveInt("Minecraft server id");

      if (!Ui.Confirm("Create backup now?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed backup request.
      return PrintAsync(Client.MinecraftServers.CreateBackupAsync(serverId));
    }
  }
}
