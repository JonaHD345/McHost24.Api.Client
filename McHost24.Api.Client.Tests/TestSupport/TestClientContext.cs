using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  internal sealed class TestClientContext : IDisposable
  {
    private readonly HttpClient _httpClient;

    internal TestClientContext(McHost24Client client, RecordingHttpMessageHandler handler, HttpClient httpClient)
    {
      Client = client;
      Handler = handler;
      _httpClient = httpClient;
    }

    internal McHost24Client Client { get; }

    internal RecordingHttpMessageHandler Handler { get; }

    public void Dispose()
    {
      Client.Dispose();
      _httpClient.Dispose();
    }
  }
}
