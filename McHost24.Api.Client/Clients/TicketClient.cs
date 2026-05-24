using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to ticket endpoints.
  /// </summary>
  public sealed class TicketClient
  {
    private readonly McHost24ApiTransport _transport;

    internal TicketClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets ticket system metadata.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The ticket system metadata returned by the API.</returns>
    public Task<TicketSystemInfo> GetInfoAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<TicketSystemInfo>(
        HttpMethod.Get,
        "support/tickets/info",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets all support tickets.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing support tickets.</returns>
    public Task<ApiResponse<List<Ticket>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<ApiResponse<List<Ticket>>>(
        HttpMethod.Get,
        "support/tickets",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Creates a support ticket.
    /// </summary>
    /// <param name="subject">The ticket subject.</param>
    /// <param name="text">The ticket message.</param>
    /// <param name="serviceId">The related service id.</param>
    /// <param name="ticketCategoryId">The ticket category id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the created ticket.</returns>
    public Task<ApiResponse<Ticket>> CreateAsync(
      string subject,
      string text,
      int serviceId,
      int ticketCategoryId,
      CancellationToken cancellationToken = default)
    {
      return CreateAsync(
        new CreateTicketRequest
        {
          Betr = subject,
          Text = text,
          Service = serviceId,
          TicketCategoryId = ticketCategoryId
        },
        cancellationToken);
    }

    /// <summary>
    /// Creates a support ticket.
    /// </summary>
    /// <param name="request">The ticket creation payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the created ticket.</returns>
    public Task<ApiResponse<Ticket>> CreateAsync(CreateTicketRequest request, CancellationToken cancellationToken = default)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      McHost24ApiTransport.ValidateRequiredString(request.Betr, nameof(request.Betr));
      McHost24ApiTransport.ValidateRequiredString(request.Text, nameof(request.Text));
      McHost24ApiTransport.ValidatePositiveId(request.Service, nameof(request.Service));
      McHost24ApiTransport.ValidatePositiveId(request.TicketCategoryId, nameof(request.TicketCategoryId));

      return _transport.SendAsync<ApiResponse<Ticket>>(
        HttpMethod.Post,
        "support/tickets",
        request,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets a support ticket.
    /// </summary>
    /// <param name="ticketId">The ticket database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the ticket data.</returns>
    public Task<ApiResponse<List<Ticket>>> GetAsync(int ticketId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(ticketId, nameof(ticketId));

      return _transport.SendAsync<ApiResponse<List<Ticket>>>(
        HttpMethod.Get,
        $"support/tickets/{McHost24ApiTransport.FormatId(ticketId)}",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Replies to a support ticket.
    /// </summary>
    /// <param name="ticketId">The ticket database id.</param>
    /// <param name="reply">The reply message.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the reply endpoint.</returns>
    public Task<ApiResponse> ReplyAsync(int ticketId, string reply, CancellationToken cancellationToken = default)
    {
      return ReplyAsync(ticketId, new TicketReplyRequest { Reply = reply }, cancellationToken);
    }

    /// <summary>
    /// Replies to a support ticket.
    /// </summary>
    /// <param name="ticketId">The ticket database id.</param>
    /// <param name="request">The ticket reply payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the reply endpoint.</returns>
    public Task<ApiResponse> ReplyAsync(int ticketId, TicketReplyRequest request, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(ticketId, nameof(ticketId));

      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      McHost24ApiTransport.ValidateRequiredString(request.Reply, nameof(request.Reply));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"support/tickets/{McHost24ApiTransport.FormatId(ticketId)}/reply",
        request,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Reopens a support ticket.
    /// </summary>
    /// <param name="ticketId">The ticket database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the reopen endpoint.</returns>
    public Task<ApiResponse> ReopenAsync(int ticketId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(ticketId, nameof(ticketId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"support/tickets/{McHost24ApiTransport.FormatId(ticketId)}/reopen",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Closes a support ticket.
    /// </summary>
    /// <param name="ticketId">The ticket database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the close endpoint.</returns>
    public Task<ApiResponse> CloseAsync(int ticketId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(ticketId, nameof(ticketId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"support/tickets/{McHost24ApiTransport.FormatId(ticketId)}/close",
        null,
        true,
        cancellationToken);
    }
  }
}
