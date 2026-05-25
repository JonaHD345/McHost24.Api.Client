using System;
using System.Threading.Tasks;
using McHost24.Api.Client;

namespace McHost24.Api.Client.Example
{
  /// <summary>
  /// Contains console actions for domain, DNS, and domain email endpoints.
  /// </summary>
  internal sealed class DomainConsoleActions : ConsoleActionBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainConsoleActions"/> class.
    /// </summary>
    /// <param name="client">The API client used to call domain endpoints.</param>
    /// <param name="ui">The console UI helper.</param>
    public DomainConsoleActions(McHost24Client client, ConsoleUi ui)
      : base(client, ui)
    {
      // No extra state is needed for domain actions.
    }

    /// <summary>
    /// Retrieves and prints all domains.
    /// </summary>
    public Task ListAsync()
    {
      // List domains first so users can find ids for detail and mutation calls.
      return PrintAsync(Client.Domains.GetAllAsync());
    }

    /// <summary>
    /// Retrieves and prints the DNS record types supported by the API.
    /// </summary>
    public Task ListRecordTypesAsync()
    {
      // Print API-provided record types for comparison with the local constants.
      return PrintAsync(Client.Domains.GetAvailableRecordTypesAsync());
    }

    /// <summary>
    /// Prompts for a domain id and prints domain details.
    /// </summary>
    public Task GetInfoAsync()
    {
      // Domain info includes records and email accounts for the selected domain.
      int domainId = Ui.ReadPositiveInt("Domain id");
      return PrintAsync(Client.Domains.GetInfoAsync(domainId));
    }

    /// <summary>
    /// Prompts for DNS record data and creates a record for a domain.
    /// </summary>
    public Task CreateDnsRecordAsync()
    {
      // Build the API payload from console prompts before asking for confirmation.
      int domainId = Ui.ReadPositiveInt("Domain id");
      DomainRecord record = new DomainRecord
      {
        Sld = Ui.ReadRequired("Record sld"),
        Type = ReadDnsRecordType(),
        Target = Ui.ReadRequired("Record target")
      };

      if (!Ui.Confirm("Create this DNS record?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Send the confirmed create request and print the created record response.
      return PrintAsync(Client.Domains.CreateDnsRecordAsync(domainId, record));
    }

    /// <summary>
    /// Prompts for record identifiers and deletes a DNS record.
    /// </summary>
    public Task DeleteDnsRecordAsync()
    {
      // Both the domain id and record id are needed for the delete route.
      int domainId = Ui.ReadPositiveInt("Domain id");
      int recordId = Ui.ReadPositiveInt("DNS record id");

      if (!Ui.Confirm("Delete this DNS record?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed delete request.
      return PrintAsync(Client.Domains.DeleteDnsRecordAsync(domainId, recordId));
    }

    /// <summary>
    /// Prompts for email account data and creates a domain email account.
    /// </summary>
    public Task CreateEmailAsync()
    {
      // Read the email payload, masking the password in interactive consoles.
      int domainId = Ui.ReadPositiveInt("Domain id");
      DomainEmail email = new DomainEmail
      {
        Username = Ui.ReadRequired("Email username"),
        Password = Ui.ReadPassword("Email password")
      };

      if (!Ui.Confirm("Create this email account?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed email account create request.
      return PrintAsync(Client.Domains.CreateEmailAsync(domainId, email));
    }

    /// <summary>
    /// Prompts for email identifiers and deletes a domain email account.
    /// </summary>
    public Task DeleteEmailAsync()
    {
      // The API expects both the parent domain id and the email account id.
      int domainId = Ui.ReadPositiveInt("Domain id");
      int emailId = Ui.ReadPositiveInt("Domain email id");

      if (!Ui.Confirm("Delete this email account?"))
      {
        Ui.WriteWarning("Cancelled.");
        return Task.CompletedTask;
      }

      // Execute the confirmed email account delete request.
      return PrintAsync(Client.Domains.DeleteEmailAsync(domainId, emailId));
    }

    /// <summary>
    /// Reads a DNS record type from the console.
    /// </summary>
    /// <returns>The entered DNS record type in uppercase.</returns>
    private string ReadDnsRecordType()
    {
      // Show the known constants before accepting free-form input for forward compatibility.
      string[] knownTypes = new string[]
      {
        DomainRecordTypes.A,
        DomainRecordTypes.Aaaa,
        DomainRecordTypes.Cname,
        DomainRecordTypes.Mx,
        DomainRecordTypes.Ns,
        DomainRecordTypes.Srv,
        DomainRecordTypes.Txt,
        DomainRecordTypes.Caa,
        DomainRecordTypes.HttpFrame,
        DomainRecordTypes.HttpsFrame,
        DomainRecordTypes.HttpHeader,
        DomainRecordTypes.HttpsHeader
      };

      Console.WriteLine($"Known record types: {string.Join(", ", knownTypes)}");
      return Ui.ReadRequired("Record type").ToUpperInvariant();
    }
  }
}
