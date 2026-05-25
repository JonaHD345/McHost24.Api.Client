using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for account-related endpoints.
  /// </summary>
  internal sealed class AccountConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call account endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public AccountConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for account actions.
    }

    /// <summary>
    /// Retrieves and prints the authenticated account profile.
    /// </summary>
    public Task ShowProfileAsync()
    {
      // The profile endpoint returns a direct profile model instead of an API response wrapper.
      return PrintAsync(Client.User.GetProfileAsync());
    }
  }
}
