using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to service endpoints.
  /// </summary>
  public sealed class ServiceClient
  {
    private readonly McHost24ApiTransport _transport;

    internal ServiceClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets renew prices for a service.
    /// </summary>
    /// <param name="serviceId">The service database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing service renew prices.</returns>
    public Task<ApiResponse<ServiceRenewPrice>> GetRenewPriceAsync(int serviceId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(serviceId, nameof(serviceId));

      return _transport.SendAsync<ApiResponse<ServiceRenewPrice>>(
        HttpMethod.Get,
        $"service/{McHost24ApiTransport.FormatId(serviceId)}/price",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Renews a service for the specified runtime.
    /// </summary>
    /// <param name="serviceId">The service database id.</param>
    /// <param name="runtime">The requested renew runtime.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the renew endpoint.</returns>
    public Task<ApiResponse> RenewAsync(int serviceId, string runtime, CancellationToken cancellationToken = default)
    {
      return RenewAsync(serviceId, new ServiceRenewRequest { Runtime = runtime }, cancellationToken);
    }

    /// <summary>
    /// Renews a service.
    /// </summary>
    /// <param name="serviceId">The service database id.</param>
    /// <param name="request">The service renew payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the renew endpoint.</returns>
    public Task<ApiResponse> RenewAsync(int serviceId, ServiceRenewRequest request, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(serviceId, nameof(serviceId));

      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      McHost24ApiTransport.ValidateRequiredString(request.Runtime, nameof(request.Runtime));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Post,
        $"service/{McHost24ApiTransport.FormatId(serviceId)}/renew",
        request,
        true,
        cancellationToken);
    }
  }
}
