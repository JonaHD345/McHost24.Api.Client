using System.Net.Http;

namespace McHost24.Api.Client.Tests
{
  internal sealed class RecordedHttpRequest
  {
    internal RecordedHttpRequest(
      HttpMethod method,
      string pathAndQuery,
      string? content,
      string? contentType,
      IReadOnlyList<string> accept,
      string? authorization)
    {
      Method = method;
      PathAndQuery = pathAndQuery;
      Content = content;
      ContentType = contentType;
      Accept = accept;
      Authorization = authorization;
    }

    internal HttpMethod Method { get; }

    internal string PathAndQuery { get; }

    internal string? Content { get; }

    internal string? ContentType { get; }

    internal IReadOnlyList<string> Accept { get; }

    internal string? Authorization { get; }
  }
}
