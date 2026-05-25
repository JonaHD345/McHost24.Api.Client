using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  public sealed class TicketClientTests
  {
    [Fact]
    public async Task GetInfoAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson("{\"canCreateTicket\":true,\"categories\":[],\"services\":[]}");

      // Act
      var info = await context.Client.Tickets.GetInfoAsync();

      // Assert
      Assert.True(info.CanCreateTicket);
      Assert.NotNull(info.Categories);
      Assert.NotNull(info.Services);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "support/tickets/info");
    }

    [Fact]
    public async Task GetAllAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[]"));

      // Act
      var response = await context.Client.Tickets.GetAllAsync();

      // Assert
      Assert.True(response.Success);
      Assert.NotNull(response.Data);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "support/tickets");
    }

    [Fact]
    public async Task GetAllAsync_WithSystemUserId_DeserializesUserIdAsNull()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[{\"id\":12,\"user_id\":\"SYSTEM\"}]"));

      // Act
      var response = await context.Client.Tickets.GetAllAsync();

      // Assert
      var ticket = Assert.Single(response.Data!);
      Assert.Equal(12, ticket.Id);
      Assert.Null(ticket.UserId);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "support/tickets");
    }

    [Fact]
    public async Task CreateAsync_WithValues_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"id\":12,\"betr\":\"Subject\"}"));

      // Act
      var response = await context.Client.Tickets.CreateAsync("Subject", "Message", 55, 4);

      // Assert
      Assert.Equal(12, response.Data?.Id);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "support/tickets");
      Assert.Equal("application/json", request.ContentType);

      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("Subject", body.GetProperty("betr").GetString());
      Assert.Equal("Message", body.GetProperty("text").GetString());
      Assert.Equal(55, body.GetProperty("service").GetInt32());
      Assert.Equal(4, body.GetProperty("ticket_category_id").GetInt32());
    }

    [Fact]
    public async Task CreateAsync_WithRequest_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var ticketRequest = new CreateTicketRequest
      {
        Betr = "Subject",
        Text = "Message",
        Service = 55,
        TicketCategoryId = 4
      };
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("{\"id\":12,\"betr\":\"Subject\"}"));

      // Act
      var response = await context.Client.Tickets.CreateAsync(ticketRequest);

      // Assert
      Assert.Equal(12, response.Data?.Id);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "support/tickets");
      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("Subject", body.GetProperty("betr").GetString());
      Assert.Equal("Message", body.GetProperty("text").GetString());
      Assert.Equal(55, body.GetProperty("service").GetInt32());
      Assert.Equal(4, body.GetProperty("ticket_category_id").GetInt32());
    }

    [Fact]
    public async Task CreateAsync_WithNullRequest_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => context.Client.Tickets.CreateAsync(null!));

      // Assert
      Assert.Equal("request", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task CreateAsync_WithMissingSubject_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var ticketRequest = new CreateTicketRequest
      {
        Text = "Message",
        Service = 55,
        TicketCategoryId = 4
      };

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => context.Client.Tickets.CreateAsync(ticketRequest));

      // Assert
      Assert.Equal(nameof(CreateTicketRequest.Betr), exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task GetAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[{\"id\":12,\"betr\":\"Subject\"}]"));

      // Act
      var response = await context.Client.Tickets.GetAsync(12);

      // Assert
      var ticket = Assert.Single(response.Data!);
      Assert.Equal("Subject", ticket.Betr);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "support/tickets/12");
    }

    [Fact]
    public async Task GetAsync_WithSystemAnswerUserId_DeserializesAnswerUserIdAsNull()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson("[{\"id\":12,\"answers\":[{\"id\":44,\"user_id\":\"SYSTEM\"}]}]"));

      // Act
      var response = await context.Client.Tickets.GetAsync(12);

      // Assert
      var ticket = Assert.Single(response.Data!);
      var answer = Assert.Single(ticket.Answers!);
      Assert.Equal(44, answer.Id);
      Assert.Null(answer.UserId);
      context.Handler.AssertSingleRequest(HttpMethod.Get, "support/tickets/12");
    }

    [Fact]
    public async Task GetAsync_WithInvalidTicketId_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => context.Client.Tickets.GetAsync(0));

      // Assert
      Assert.Equal("ticketId", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task ReplyAsync_WithReply_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Tickets.ReplyAsync(12, "Reply text");

      // Assert
      Assert.True(response.Success);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "support/tickets/12/reply");
      Assert.Equal("application/json", request.ContentType);

      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("Reply text", body.GetProperty("reply").GetString());
    }

    [Fact]
    public async Task ReplyAsync_WithRequest_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var replyRequest = new TicketReplyRequest
      {
        Reply = "Reply text"
      };
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Tickets.ReplyAsync(12, replyRequest);

      // Assert
      Assert.True(response.Success);

      var request = context.Handler.AssertSingleRequest(HttpMethod.Post, "support/tickets/12/reply");
      var body = ClientTestHost.GetBodyJson(request);
      Assert.Equal("Reply text", body.GetProperty("reply").GetString());
    }

    [Fact]
    public async Task ReplyAsync_WithNullRequest_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => context.Client.Tickets.ReplyAsync(12, (TicketReplyRequest)null!));

      // Assert
      Assert.Equal("request", exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task ReplyAsync_WithMissingReply_Throws()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      var replyRequest = new TicketReplyRequest();

      // Act
      var exception = await Assert.ThrowsAsync<ArgumentException>(() => context.Client.Tickets.ReplyAsync(12, replyRequest));

      // Assert
      Assert.Equal(nameof(TicketReplyRequest.Reply), exception.ParamName);
      Assert.Empty(context.Handler.Requests);
    }

    [Fact]
    public async Task ReopenAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Tickets.ReopenAsync(12);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "support/tickets/12/reopen");
    }

    [Fact]
    public async Task CloseAsync_SendsExpectedRequest()
    {
      // Arrange
      using var context = ClientTestHost.CreateClient();
      context.Handler.EnqueueJson(ClientTestHost.ApiResponseJson());

      // Act
      var response = await context.Client.Tickets.CloseAsync(12);

      // Assert
      Assert.True(response.Success);
      context.Handler.AssertSingleRequest(HttpMethod.Post, "support/tickets/12/close");
    }
  }
}
