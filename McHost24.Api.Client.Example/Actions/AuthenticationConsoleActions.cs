using System;
using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for login and logout workflows.
  /// </summary>
  internal sealed class AuthenticationConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthenticationConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call authentication endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public AuthenticationConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for authentication actions.
    }

    /// <summary>
    /// Prompts for credentials until the API returns a token stored on the client.
    /// </summary>
    public async Task LoginAsync()
    {
      while (true)
      {
        // Collect the required login values before sending the token request.
        Ui.WriteHeader("Login");

        string username = Ui.ReadRequired("Username");
        string password = Ui.ReadPassword("Password");
        int? tfa = Ui.ReadOptionalInt("2FA code (optional)");

        try
        {
          // Login stores the returned API token on the shared client.
          ApiResponse<LoginResult> response = await Client.Authentication.LoginAsync(username, password, tfa).ConfigureAwait(false);
          Ui.PrintJson(response);

          // Continue only when a token is available for authenticated follow-up calls.
          if (!string.IsNullOrWhiteSpace(Client.ApiToken))
          {
            Ui.WriteSuccess("Login succeeded. The returned token is stored on the client.");
            return;
          }

          Ui.WriteError("Login response did not contain an API token.");
        }
        catch (McHost24ApiException ex)
        {
          Ui.WriteApiException(ex);
        }
        catch (Exception ex)
        {
          Ui.WriteError(ex.Message);
        }

        Ui.WaitForRetry();
        Ui.ClearScreen();
      }
    }

    /// <summary>
    /// Logs out the current token and starts a fresh login flow.
    /// </summary>
    public async Task LogoutAndLoginAgainAsync()
    {
      // Logout clears the current token on success before the next login starts.
      await PrintAsync(Client.Authentication.LogoutAsync()).ConfigureAwait(false);
      Console.WriteLine();
      await LoginAsync().ConfigureAwait(false);
    }
  }
}
