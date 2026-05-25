using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for TeamSpeak server endpoints.
  /// </summary>
  internal sealed class TeamSpeakServerConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TeamSpeakServerConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call TeamSpeak server endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public TeamSpeakServerConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for TeamSpeak server actions.
    }

    /// <summary>
    /// Retrieves and prints all TeamSpeak servers.
    /// </summary>
    public Task ListAsync()
    {
      // List servers first so users can find ids for status and control actions.
      return PrintAsync(Client.TeamSpeakServers.GetAllAsync());
    }

    /// <summary>
    /// Prompts for a TeamSpeak server id and prints its status.
    /// </summary>
    public Task GetStatusAsync()
    {
      // The status endpoint returns live details for one TeamSpeak server.
      int serverId = Ui.ReadPositiveInt("TeamSpeak server id");
      return PrintAsync(Client.TeamSpeakServers.GetStatusAsync(serverId));
    }

    /// <summary>
    /// Prompts for a TeamSpeak server id and starts the server.
    /// </summary>
    public Task StartAsync()
    {
      // Confirm before changing the server power state.
      int serverId = Ui.ReadPositiveInt("TeamSpeak server id");

      if (!Ui.Confirm("Send start request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed start request.
      return PrintAsync(Client.TeamSpeakServers.StartAsync(serverId));
    }

    /// <summary>
    /// Prompts for a TeamSpeak server id and stops the server.
    /// </summary>
    public Task StopAsync()
    {
      // Confirm before changing the server power state.
      int serverId = Ui.ReadPositiveInt("TeamSpeak server id");

      if (!Ui.Confirm("Send stop request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed stop request.
      return PrintAsync(Client.TeamSpeakServers.StopAsync(serverId));
    }

    /// <summary>
    /// Prompts for a TeamSpeak server id and restarts the server.
    /// </summary>
    public Task RestartAsync()
    {
      // Confirm before restarting a running service.
      int serverId = Ui.ReadPositiveInt("TeamSpeak server id");

      if (!Ui.Confirm("Send restart request?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed restart request.
      return PrintAsync(Client.TeamSpeakServers.RestartAsync(serverId));
    }
  }
}
