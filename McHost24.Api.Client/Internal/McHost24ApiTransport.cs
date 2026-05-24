using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  internal sealed class McHost24ApiTransport : IDisposable
  {
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerSettings _jsonSerializerSettings;
    private readonly bool _disposeHttpClient;
    private bool _disposed;

    internal McHost24ApiTransport(HttpClient httpClient, McHost24ClientOptions options, bool disposeHttpClient)
    {
      if (httpClient == null)
      {
        throw new ArgumentNullException(nameof(httpClient));
      }

      if (options == null)
      {
        throw new ArgumentNullException(nameof(options));
      }

      _httpClient = httpClient;
      _disposeHttpClient = disposeHttpClient;
      _jsonSerializerSettings = McHost24JsonSerializerSettings.Create(options.JsonSerializerSettings);

      if (_httpClient.BaseAddress == null)
      {
        _httpClient.BaseAddress = EnsureTrailingSlash(options.BaseAddress);
      }

      ApiToken = options.ApiToken;
    }

    internal string? ApiToken { get; private set; }

    internal void SetApiToken(string apiToken)
    {
      ValidateRequiredString(apiToken, nameof(apiToken));
      ApiToken = apiToken;
    }

    internal void ClearApiToken()
    {
      ApiToken = null;
    }

    public void Dispose()
    {
      if (_disposed)
      {
        return;
      }

      if (_disposeHttpClient)
      {
        _httpClient.Dispose();
      }

      _disposed = true;
    }

    internal async Task<TResponse> SendAsync<TResponse>(
      HttpMethod method,
      string path,
      object? requestBody,
      bool authenticate,
      CancellationToken cancellationToken)
    {
      EnsureNotDisposed();

      using (var request = new HttpRequestMessage(method, path))
      {
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (authenticate)
        {
          ApplyAuthorization(request);
        }

        if (requestBody != null)
        {
          var json = JsonConvert.SerializeObject(requestBody, _jsonSerializerSettings);
          request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false))
        {
          var responseContent = response.Content == null
            ? null
            : await response.Content.ReadAsStringAsync().ConfigureAwait(false);

          if (!response.IsSuccessStatusCode)
          {
            throw new McHost24ApiException(
              $"The MC-HOST24 API returned HTTP {(int)response.StatusCode} ({response.ReasonPhrase}).",
              response.StatusCode,
              responseContent);
          }

          if (string.IsNullOrWhiteSpace(responseContent))
          {
            throw new McHost24ApiException("The MC-HOST24 API returned an empty response.");
          }

          try
          {
            var result = JsonConvert.DeserializeObject<TResponse>(responseContent, _jsonSerializerSettings);

            if (result == null)
            {
              throw new McHost24ApiException("The MC-HOST24 API response could not be deserialized.");
            }

            return result;
          }
          catch (JsonException exception)
          {
            throw new McHost24ApiException("The MC-HOST24 API response could not be deserialized.", exception);
          }
        }
      }
    }

    internal static void ValidatePositiveId(int value, string parameterName)
    {
      if (value <= 0)
      {
        throw new ArgumentOutOfRangeException(parameterName, value, "The value must be greater than zero.");
      }
    }

    internal static void ValidateRequiredString(string? value, string parameterName)
    {
      if (string.IsNullOrWhiteSpace(value))
      {
        throw new ArgumentException("The value is required.", parameterName);
      }
    }

    internal static string FormatId(int value)
    {
      return value.ToString(CultureInfo.InvariantCulture);
    }

    private void ApplyAuthorization(HttpRequestMessage request)
    {
      if (!string.IsNullOrWhiteSpace(ApiToken))
      {
        request.Headers.TryAddWithoutValidation("Authorization", ApiToken);
        return;
      }

      if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
      {
        return;
      }

      throw new InvalidOperationException("An API token is required for this MC-HOST24 API endpoint.");
    }

    private void EnsureNotDisposed()
    {
      if (_disposed)
      {
        throw new ObjectDisposedException(nameof(McHost24Client));
      }
    }

    private static Uri EnsureTrailingSlash(Uri uri)
    {
      if (uri == null)
      {
        throw new ArgumentNullException(nameof(uri));
      }

      if (!uri.IsAbsoluteUri)
      {
        throw new ArgumentException("The API base address must be absolute.", nameof(uri));
      }

      var value = uri.ToString();

      return value.EndsWith("/", StringComparison.Ordinal)
        ? uri
        : new Uri(value + "/");
    }
  }
}
