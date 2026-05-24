using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to user endpoints.
  /// </summary>
  public sealed class UserClient
  {
    private readonly McHost24ApiTransport _transport;

    internal UserClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets the profile of the authenticated account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The profile response returned by the API.</returns>
    public Task<Profile> GetProfileAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<Profile>(
        HttpMethod.Get,
        "profile",
        null,
        true,
        cancellationToken);
    }
  }
}
