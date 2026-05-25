using System;
using System.Text;
using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains the entry point for the interactive example application.
  /// </summary>
  internal static class Program
  {
    /// <summary>
    /// Creates the shared client and starts the console example.
    /// </summary>
    private static async Task Main()
    {
      // Use UTF-8 so API data and console labels are printed consistently.
      Console.OutputEncoding = Encoding.UTF8;

      // Wire the small example components around one authenticated client instance.
      using McHost24Client client = new McHost24Client();
      ConsoleUi ui = new ConsoleUi();
      AuthenticationConsoleActions authenticationActions = new AuthenticationConsoleActions(client, ui);
      McHost24ExampleApplication application = new McHost24ExampleApplication(client, ui, authenticationActions);

      await application.RunAsync().ConfigureAwait(false);
    }
  }
}
