using System;
using System.Linq;
using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for root server endpoints.
  /// </summary>
  internal sealed class RootServerConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="RootServerConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call root server endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public RootServerConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for root server actions.
    }

    /// <summary>
    /// Retrieves and prints all root servers.
    /// </summary>
    public Task ListAsync()
    {
      // List servers first so users can find ids for status and control actions.
      return PrintAsync(Client.RootServers.GetAllAsync());
    }

    /// <summary>
    /// Prompts for a root server id and prints its status.
    /// </summary>
    public Task GetStatusAsync()
    {
      // The status endpoint returns live resource and power information.
      int serverId = Ui.ReadPositiveInt("Root server id");
      return PrintAsync(Client.RootServers.GetStatusAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and starts the server.
    /// </summary>
    public Task StartAsync()
    {
      // Confirm before changing the server power state.
      int serverId = Ui.ReadPositiveInt("Root server id");

      if (!Ui.Confirm("Send start request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed start request.
      return PrintAsync(Client.RootServers.StartAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and sends a shutdown request.
    /// </summary>
    public Task ShutdownAsync()
    {
      // Confirm before asking the server to shut down.
      int serverId = Ui.ReadPositiveInt("Root server id");

      if (!Ui.Confirm("Send shutdown request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed shutdown request.
      return PrintAsync(Client.RootServers.ShutdownAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and stops the server.
    /// </summary>
    public Task StopAsync()
    {
      // Confirm before sending the hard stop request.
      int serverId = Ui.ReadPositiveInt("Root server id");

      if (!Ui.Confirm("Send hard stop request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed stop request.
      return PrintAsync(Client.RootServers.StopAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and restarts the server.
    /// </summary>
    public Task RestartAsync()
    {
      // Confirm before restarting a server.
      int serverId = Ui.ReadPositiveInt("Root server id");

      if (!Ui.Confirm("Send restart request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed restart request.
      return PrintAsync(Client.RootServers.RestartAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and prints its backups.
    /// </summary>
    public Task ListBackupsAsync()
    {
      // Backup listing helps users find ids for restore and delete actions.
      int serverId = Ui.ReadPositiveInt("Root server id");
      return PrintAsync(Client.RootServers.GetBackupsAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and creates a backup.
    /// </summary>
    public Task CreateBackupAsync()
    {
      // Confirm before triggering a backup job.
      int serverId = Ui.ReadPositiveInt("Root server id");

      if (!Ui.Confirm("Create backup now?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed backup request.
      return PrintAsync(Client.RootServers.CreateBackupAsync(serverId));
    }

    /// <summary>
    /// Prompts for server and backup ids and restores a root server backup.
    /// </summary>
    public Task RestoreBackupAsync()
    {
      // Collect both route identifiers before confirming the restore operation.
      int serverId = Ui.ReadPositiveInt("Root server id");
      int backupId = Ui.ReadPositiveInt("Root server backup id");

      if (!Ui.Confirm("Restore this backup?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed restore request.
      return PrintAsync(Client.RootServers.RestoreBackupAsync(serverId, backupId));
    }

    /// <summary>
    /// Prompts for server and backup ids and deletes a root server backup.
    /// </summary>
    public Task DeleteBackupAsync()
    {
      // Collect both route identifiers before confirming the delete operation.
      int serverId = Ui.ReadPositiveInt("Root server id");
      int backupId = Ui.ReadPositiveInt("Root server backup id");

      if (!Ui.Confirm("Delete this backup?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed backup delete request.
      return PrintAsync(Client.RootServers.DeleteBackupAsync(serverId, backupId));
    }

    /// <summary>
    /// Prompts for a root server id and prints its VNC console URL.
    /// </summary>
    public Task GetVncAsync()
    {
      // The VNC endpoint can be used to open remote console access.
      int serverId = Ui.ReadPositiveInt("Root server id");
      return PrintAsync(Client.RootServers.GetVncAsync(serverId));
    }

    /// <summary>
    /// Prompts for a root server id and timeframe, then prints RRD metric data.
    /// </summary>
    public Task GetRrdDataAsync()
    {
      // RRD data requires the server id and one supported timeframe value.
      int serverId = Ui.ReadPositiveInt("Root server id");
      string timeframe = ReadTimeframe();

      return PrintAsync(Client.RootServers.GetRrdDataAsync(serverId, timeframe));
    }

    /// <summary>
    /// Reads a supported RRD timeframe from the console.
    /// </summary>
    /// <returns>The selected timeframe value.</returns>
    private string ReadTimeframe()
    {
      // Present known constants before accepting the timeframe input.
      string[] allowedValues = new string[]
      {
        RootServerRrdTimeframes.Hour,
        RootServerRrdTimeframes.Day,
        RootServerRrdTimeframes.Week,
        RootServerRrdTimeframes.Month,
        RootServerRrdTimeframes.Year
      };

      while (true)
      {
        // Keep prompting until the value matches one of the documented timeframes.
        Console.WriteLine($"Known timeframes: {string.Join(", ", allowedValues)}");
        string value = Ui.ReadRequired("Timeframe").ToLowerInvariant();

        if (allowedValues.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
          return value;
        }

        Ui.WriteError("Unknown timeframe.");
      }
    }
  }
}
