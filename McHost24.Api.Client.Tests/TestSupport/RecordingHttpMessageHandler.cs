using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  internal sealed class RecordingHttpMessageHandler : HttpMessageHandler
  {
    private readonly Queue<HttpResponseMessage> _responses = new Queue<HttpResponseMessage>();

    internal List<RecordedHttpRequest> Requests { get; } = new List<RecordedHttpRequest>();

    internal void Enqueue(HttpResponseMessage response)
    {
      _responses.Enqueue(response);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
      if (_responses.Count == 0)
      {
        throw new InvalidOperationException("No HTTP response was queued for the test request.");
      }

      var content = request.Content == null
        ? null
        : await request.Content.ReadAsStringAsync(cancellationToken);
      var contentType = request.Content?.Headers.ContentType?.MediaType;
      var accept = request.Headers.Accept
        .Select(header => header.MediaType ?? header.ToString())
        .ToList();
      var authorization = request.Headers.TryGetValues("Authorization", out var values)
        ? values.SingleOrDefault()
        : null;

      Requests.Add(
        new RecordedHttpRequest(
          request.Method,
          request.RequestUri?.PathAndQuery ?? string.Empty,
          content,
          contentType,
          accept,
          authorization));

      return _responses.Dequeue();
    }
  }
}
