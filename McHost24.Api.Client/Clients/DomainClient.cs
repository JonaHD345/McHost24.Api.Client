using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace McHost24.Api.Client
{
  /// <summary>
  /// Provides access to domain endpoints.
  /// </summary>
  public sealed class DomainClient
  {
    private readonly McHost24ApiTransport _transport;

    internal DomainClient(McHost24ApiTransport transport)
    {
      _transport = transport ?? throw new ArgumentNullException(nameof(transport));
    }

    /// <summary>
    /// Gets all domains owned by the authenticated account.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing domains.</returns>
    public Task<ApiResponse<List<Domain>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<ApiResponse<List<Domain>>>(
        HttpMethod.Get,
        "domain",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets all DNS record types supported by the API.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing available DNS record types.</returns>
    public Task<ApiResponse<Dictionary<string, string>>> GetAvailableRecordTypesAsync(CancellationToken cancellationToken = default)
    {
      return _transport.SendAsync<ApiResponse<Dictionary<string, string>>>(
        HttpMethod.Get,
        "domain/availableRecords",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Gets additional information, DNS records, and emails for a domain.
    /// </summary>
    /// <param name="domainId">The domain database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing domain details.</returns>
    public Task<ApiResponse<DomainInfo>> GetInfoAsync(int domainId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(domainId, nameof(domainId));

      return _transport.SendAsync<ApiResponse<DomainInfo>>(
        HttpMethod.Get,
        $"domain/{McHost24ApiTransport.FormatId(domainId)}/info",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Creates a DNS record for a domain.
    /// </summary>
    /// <param name="domainId">The domain database id.</param>
    /// <param name="record">The DNS record payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the created DNS record.</returns>
    public Task<ApiResponse<DomainRecord>> CreateDnsRecordAsync(int domainId, DomainRecord record, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(domainId, nameof(domainId));

      if (record == null)
      {
        throw new ArgumentNullException(nameof(record));
      }

      return _transport.SendAsync<ApiResponse<DomainRecord>>(
        HttpMethod.Post,
        $"domain/{McHost24ApiTransport.FormatId(domainId)}/dns",
        record,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Deletes a DNS record from a domain.
    /// </summary>
    /// <param name="domainId">The domain database id.</param>
    /// <param name="domainRecordId">The DNS record database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the delete endpoint.</returns>
    public Task<ApiResponse> DeleteDnsRecordAsync(int domainId, int domainRecordId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(domainId, nameof(domainId));
      McHost24ApiTransport.ValidatePositiveId(domainRecordId, nameof(domainRecordId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Delete,
        $"domain/{McHost24ApiTransport.FormatId(domainId)}/dns/{McHost24ApiTransport.FormatId(domainRecordId)}",
        null,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Creates an email account for a domain.
    /// </summary>
    /// <param name="domainId">The domain database id.</param>
    /// <param name="email">The domain email payload.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response containing the created domain email.</returns>
    public Task<ApiResponse<DomainEmail>> CreateEmailAsync(int domainId, DomainEmail email, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(domainId, nameof(domainId));

      if (email == null)
      {
        throw new ArgumentNullException(nameof(email));
      }

      return _transport.SendAsync<ApiResponse<DomainEmail>>(
        HttpMethod.Post,
        $"domain/{McHost24ApiTransport.FormatId(domainId)}/email",
        email,
        true,
        cancellationToken);
    }

    /// <summary>
    /// Deletes an email account from a domain.
    /// </summary>
    /// <param name="domainId">The domain database id.</param>
    /// <param name="domainEmailId">The domain email database id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The API response returned by the delete endpoint.</returns>
    public Task<ApiResponse> DeleteEmailAsync(int domainId, int domainEmailId, CancellationToken cancellationToken = default)
    {
      McHost24ApiTransport.ValidatePositiveId(domainId, nameof(domainId));
      McHost24ApiTransport.ValidatePositiveId(domainEmailId, nameof(domainEmailId));

      return _transport.SendAsync<ApiResponse>(
        HttpMethod.Delete,
        $"domain/{McHost24ApiTransport.FormatId(domainId)}/email/{McHost24ApiTransport.FormatId(domainEmailId)}",
        null,
        true,
        cancellationToken);
    }
  }
}
