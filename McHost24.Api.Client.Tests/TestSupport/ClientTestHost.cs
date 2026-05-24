using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace McHost24.Api.Client.Tests
{
  internal static class ClientTestHost
  {
    internal const string ApiToken = "test-api-token";

    internal static TestClientContext CreateClient(string? apiToken = ApiToken)
    {
      var handler = new RecordingHttpMessageHandler();
      var httpClient = new HttpClient(handler);
      var client = new McHost24Client(
        httpClient,
        new McHost24ClientOptions
        {
          BaseAddress = new Uri("https://api.test"),
          ApiToken = apiToken
        });

      return new TestClientContext(client, handler, httpClient);
    }

    internal static string ApiResponseJson(string data = "{}")
    {
      return "{\"success\":true,\"data\":" + data + "}";
    }

    internal static string ApiResponseJson(bool success, string data = "{}")
    {
      return "{\"success\":" + success.ToString().ToLowerInvariant() + ",\"data\":" + data + "}";
    }

    internal static void EnqueueJson(this RecordingHttpMessageHandler handler, string json, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
      handler.Enqueue(
        new HttpResponseMessage(statusCode)
        {
          Content = new StringContent(json, Encoding.UTF8, "application/json")
        });
    }

    internal static RecordedHttpRequest AssertSingleRequest(
      this RecordingHttpMessageHandler handler,
      HttpMethod method,
      string pathAndQuery,
      bool authenticated = true,
      string? authorization = null)
    {
      var request = Assert.Single(handler.Requests);

      Assert.Equal(method, request.Method);
      Assert.Equal("/" + pathAndQuery.TrimStart('/'), request.PathAndQuery);
      Assert.Contains("application/json", request.Accept);

      if (authenticated)
      {
        Assert.Equal(authorization ?? ApiToken, request.Authorization);
      }
      else
      {
        Assert.Null(request.Authorization);
      }

      return request;
    }

    internal static JsonElement GetBodyJson(RecordedHttpRequest request)
    {
      Assert.NotNull(request.Content);

      using (var document = JsonDocument.Parse(request.Content!))
      {
        return document.RootElement.Clone();
      }
    }
  }
}
