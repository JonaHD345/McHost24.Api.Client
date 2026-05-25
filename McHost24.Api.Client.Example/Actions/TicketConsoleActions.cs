using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for support ticket endpoints.
  /// </summary>
  internal sealed class TicketConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="TicketConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call ticket endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public TicketConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for ticket actions.
    }

    /// <summary>
    /// Retrieves and prints ticket system metadata.
    /// </summary>
    public Task GetInfoAsync()
    {
      // Ticket info exposes categories and services needed to create tickets.
      return PrintAsync(Client.Tickets.GetInfoAsync());
    }

    /// <summary>
    /// Retrieves and prints all support tickets.
    /// </summary>
    public Task ListAsync()
    {
      // List tickets first so users can find ids for detail and status changes.
      return PrintAsync(Client.Tickets.GetAllAsync());
    }

    /// <summary>
    /// Prompts for a ticket id and prints the ticket details.
    /// </summary>
    public Task GetAsync()
    {
      // Fetch one ticket by id, including its answer history when returned by the API.
      int ticketId = Ui.ReadPositiveInt("Ticket id");
      return PrintAsync(Client.Tickets.GetAsync(ticketId));
    }

    /// <summary>
    /// Prompts for ticket data and creates a support ticket.
    /// </summary>
    public Task CreateAsync()
    {
      // Build the ticket request from required fields before confirming creation.
      CreateTicketRequest request = new CreateTicketRequest
      {
        Betr = Ui.ReadRequired("Subject"),
        Text = Ui.ReadRequired("Message"),
        Service = Ui.ReadPositiveInt("Service id"),
        TicketCategoryId = Ui.ReadPositiveInt("Ticket category id")
      };

      if (!Ui.Confirm("Create this ticket?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed ticket creation request.
      return PrintAsync(Client.Tickets.CreateAsync(request));
    }

    /// <summary>
    /// Prompts for a ticket id and reply text, then replies to the ticket.
    /// </summary>
    public Task ReplyAsync()
    {
      // Replies are submitted immediately because they require explicit message input.
      int ticketId = Ui.ReadPositiveInt("Ticket id");
      string reply = Ui.ReadRequired("Reply");

      return PrintAsync(Client.Tickets.ReplyAsync(ticketId, reply));
    }

    /// <summary>
    /// Prompts for a ticket id and reopens the ticket.
    /// </summary>
    public Task ReopenAsync()
    {
      // Confirm before changing the ticket state.
      int ticketId = Ui.ReadPositiveInt("Ticket id");

      if (!Ui.Confirm("Reopen this ticket?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed reopen request.
      return PrintAsync(Client.Tickets.ReopenAsync(ticketId));
    }

    /// <summary>
    /// Prompts for a ticket id and closes the ticket.
    /// </summary>
    public Task CloseAsync()
    {
      // Confirm before changing the ticket state.
      int ticketId = Ui.ReadPositiveInt("Ticket id");

      if (!Ui.Confirm("Close this ticket?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed close request.
      return PrintAsync(Client.Tickets.CloseAsync(ticketId));
    }
  }
}
