using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for service renewal endpoints.
  /// </summary>
  internal sealed class ServiceConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call service endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public ServiceConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for service actions.
    }

    /// <summary>
    /// Prompts for a service id and prints the available renewal prices.
    /// </summary>
    public Task GetRenewPriceAsync()
    {
      // The price response includes the runtimes that can be passed to renew.
      int serviceId = Ui.ReadPositiveInt("Service id");
      return PrintAsync(Client.Services.GetRenewPriceAsync(serviceId));
    }

    /// <summary>
    /// Prompts for a service id and runtime, then renews the service.
    /// </summary>
    public Task RenewAsync()
    {
      // Read the service id and runtime exactly as expected by the API.
      int serviceId = Ui.ReadPositiveInt("Service id");
      string runtime = Ui.ReadRequired("Runtime");

      if (!Ui.Confirm("Renew this service?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed renew request.
      return PrintAsync(Client.Services.RenewAsync(serviceId, runtime));
    }
  }
}
