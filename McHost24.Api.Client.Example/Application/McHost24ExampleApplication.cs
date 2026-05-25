using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Coordinates login, menu selection, and action execution for the example app.
  /// </summary>
  internal sealed class McHost24ExampleApplication
  {
    private readonly AuthenticationConsoleActions _authenticationActions;
    private readonly ConsoleMenu _menu;
    private readonly ConsoleUi _ui;

    /// <summary>
    /// Initializes a new instance of the <see cref="McHost24ExampleApplication"/> class.
    /// </summary>
    /// <param name="client">The API client shared by all actions.</param>
    /// <param name="ui">The console UI helper.</param>
    /// <param name="authenticationActions">The authentication actions used before menu access.</param>
    public McHost24ExampleApplication(
      McHost24Client client,
      ConsoleUi ui,
      AuthenticationConsoleActions authenticationActions)
    {
      // Build the menu once because the set of available API actions is static.
      _ui = ui ?? throw new ArgumentNullException(nameof(ui));
      _authenticationActions = authenticationActions ?? throw new ArgumentNullException(nameof(authenticationActions));
      _menu = new ConsoleMenu(_ui, CreateActions(client, _ui, _authenticationActions));
    }

    /// <summary>
    /// Runs the login flow and keeps reading menu actions until the user exits.
    /// </summary>
    public async Task RunAsync()
    {
      // Require a successful login before any authenticated endpoint can be selected.
      _ui.WriteBanner();
      await _authenticationActions.LoginAsync().ConfigureAwait(false);

      while (true)
      {
        // Every completed action returns here so another number can be entered.
        MenuAction? action = _menu.ReadAction();

        if (action == null)
        {
          continue;
        }

        if (action.IsExit)
        {
          _ui.WriteSuccess("Bye.");
          return;
        }

        // Render each action in a clean section and keep failures inside the menu loop.
        _ui.ClearScreen();
        _ui.WriteHeader(action.Title);

        try
        {
          await action.ExecuteAsync().ConfigureAwait(false);
        }
        catch (McHost24ApiException ex)
        {
          _ui.WriteApiException(ex);
        }
        catch (Exception ex)
        {
          _ui.WriteError(ex.Message);
        }

        _ui.WaitForMenu();
      }
    }

    /// <summary>
    /// Creates all menu actions exposed by the example application.
    /// </summary>
    /// <param name="client">The API client shared by all actions.</param>
    /// <param name="ui">The console UI helper.</param>
    /// <param name="authenticationActions">The authentication actions reused by the account menu.</param>
    /// <returns>The complete list of selectable menu actions.</returns>
    private static IReadOnlyList<MenuAction> CreateActions(
      McHost24Client client,
      ConsoleUi ui,
      AuthenticationConsoleActions authenticationActions)
    {
      // Instantiate one small action group per API area to keep menu wiring readable.
      int nextNumber = 1;
      List<MenuAction> actions = new List<MenuAction>();
      AccountConsoleActions accountActions = new AccountConsoleActions(client, ui);
      MinecraftServerConsoleActions minecraftActions = new MinecraftServerConsoleActions(client, ui);
      TeamSpeakServerConsoleActions teamSpeakActions = new TeamSpeakServerConsoleActions(client, ui);
      RootServerConsoleActions rootServerActions = new RootServerConsoleActions(client, ui);
      DomainConsoleActions domainActions = new DomainConsoleActions(client, ui);
      ServiceConsoleActions serviceActions = new ServiceConsoleActions(client, ui);
      TicketConsoleActions ticketActions = new TicketConsoleActions(client, ui);

      void Add(string group, string title, Func<Task> executeAsync)
      {
        // Assign numbers in registration order so the displayed menu stays predictable.
        actions.Add(new MenuAction(nextNumber++, group, title, executeAsync));
      }

      // Register every public client capability exposed by this package.
      Add("Account", "Show profile", accountActions.ShowProfileAsync);
      Add("Account", "Logout and login again", authenticationActions.LogoutAndLoginAgainAsync);

      Add("Minecraft servers", "List Minecraft servers", minecraftActions.ListAsync);
      Add("Minecraft servers", "Get Minecraft server status", minecraftActions.GetStatusAsync);
      Add("Minecraft servers", "Start Minecraft server", minecraftActions.StartAsync);
      Add("Minecraft servers", "Stop Minecraft server", minecraftActions.StopAsync);
      Add("Minecraft servers", "Restart Minecraft server", minecraftActions.RestartAsync);
      Add("Minecraft servers", "List Minecraft server backups", minecraftActions.ListBackupsAsync);
      Add("Minecraft servers", "Create Minecraft server backup", minecraftActions.CreateBackupAsync);

      Add("TeamSpeak servers", "List TeamSpeak servers", teamSpeakActions.ListAsync);
      Add("TeamSpeak servers", "Get TeamSpeak server status", teamSpeakActions.GetStatusAsync);
      Add("TeamSpeak servers", "Start TeamSpeak server", teamSpeakActions.StartAsync);
      Add("TeamSpeak servers", "Stop TeamSpeak server", teamSpeakActions.StopAsync);
      Add("TeamSpeak servers", "Restart TeamSpeak server", teamSpeakActions.RestartAsync);

      Add("Root servers", "List root servers", rootServerActions.ListAsync);
      Add("Root servers", "Get root server status", rootServerActions.GetStatusAsync);
      Add("Root servers", "Start root server", rootServerActions.StartAsync);
      Add("Root servers", "Shutdown root server", rootServerActions.ShutdownAsync);
      Add("Root servers", "Stop root server", rootServerActions.StopAsync);
      Add("Root servers", "Restart root server", rootServerActions.RestartAsync);
      Add("Root servers", "List root server backups", rootServerActions.ListBackupsAsync);
      Add("Root servers", "Create root server backup", rootServerActions.CreateBackupAsync);
      Add("Root servers", "Restore root server backup", rootServerActions.RestoreBackupAsync);
      Add("Root servers", "Delete root server backup", rootServerActions.DeleteBackupAsync);
      Add("Root servers", "Get root server VNC URL", rootServerActions.GetVncAsync);
      Add("Root servers", "Get root server RRD data", rootServerActions.GetRrdDataAsync);

      Add("Domains", "List domains", domainActions.ListAsync);
      Add("Domains", "List available DNS record types", domainActions.ListRecordTypesAsync);
      Add("Domains", "Get domain info", domainActions.GetInfoAsync);
      Add("Domains", "Create DNS record", domainActions.CreateDnsRecordAsync);
      Add("Domains", "Delete DNS record", domainActions.DeleteDnsRecordAsync);
      Add("Domains", "Create domain email account", domainActions.CreateEmailAsync);
      Add("Domains", "Delete domain email account", domainActions.DeleteEmailAsync);

      Add("Services", "Get service renew price", serviceActions.GetRenewPriceAsync);
      Add("Services", "Renew service", serviceActions.RenewAsync);

      Add("Support tickets", "Get ticket system info", ticketActions.GetInfoAsync);
      Add("Support tickets", "List tickets", ticketActions.ListAsync);
      Add("Support tickets", "Get ticket", ticketActions.GetAsync);
      Add("Support tickets", "Create ticket", ticketActions.CreateAsync);
      Add("Support tickets", "Reply to ticket", ticketActions.ReplyAsync);
      Add("Support tickets", "Reopen ticket", ticketActions.ReopenAsync);
      Add("Support tickets", "Close ticket", ticketActions.CloseAsync);

      actions.Add(MenuAction.Exit);

      // Return an immutable view so the menu can render without mutating registrations.
      return actions;
    }
  }
}
