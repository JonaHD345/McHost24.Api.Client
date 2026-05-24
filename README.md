# McHost24.Api.Client

A strongly typed .NET wrapper for the public API of [MC-HOST24](https://mc-host24.de/). Built with `.NET Standard 2.1`, this library provides a simple and convenient way to manage your servers, domains, tickets, and services directly from your C# application.

## Features

- **Simple Authentication**: Supports login via username & password (including 2FA) as well as the use of persistent API tokens.
- **User Profile**: Retrieve profile data of the authenticated account (name, email, balance, etc.).
- **Minecraft Servers**: List all servers, check status (online players, RAM, CPU), start, stop, restart, and manage backups.
- **TeamSpeak Servers**: List, check status, start, stop, and restart.
- **Root Servers (vServer)**: List, check status, control power states (start, shutdown, stop, restart), manage backups (create, restore, delete), retrieve VNC console URLs, and load RRD statistics (CPU, RAM, HDD, Traffic).
- **Domains & DNS**: List domains, retrieve detailed DNS and email configurations, manage DNS records (create, delete), and manage email accounts (create, delete).
- **Service Management**: Retrieve renewal prices and extend service runtimes.
- **Support Tickets**: Retrieve ticket metadata (categories, etc.), list all tickets, get ticket details, create new tickets, reply to tickets, and close or reopen tickets.

---

## Installation

Add the project reference to your application or install the package (once available on NuGet):

```bash
dotnet add package McHost24.Api.Client
```

---

## Quick Start

### 1. Initialize the Client
You can instantiate the client either with an existing API token or by logging in with credentials:

```csharp
using McHost24.Api.Client;

// Option A: Initialize directly with a persistent API token
var client = new McHost24Client("your-api-token");

// Option B: Start without a token and login later
var client = new McHost24Client();
var loginResult = await client.Authentication.LoginAsync("Username", "Password");
if (loginResult.Success)
{
    // The client automatically stores the token for subsequent requests.
    Console.WriteLine($"Logged in! Token: {client.ApiToken}");
}
```

### 2. Control Minecraft Servers
Here is a simple example of how to list all Minecraft servers and restart the first one:

```csharp
var response = await client.MinecraftServers.GetAllAsync();

if (response.Success && response.Data != null)
{
    foreach (var server in response.Data)
    {
        Console.WriteLine($"Server: {server.Name} (ID: {server.Id})");
    }

    if (response.Data.Count > 0)
    {
        int serverId = response.Data[0].Id!.Value;
        Console.WriteLine($"Restarting server {serverId}...");
        await client.MinecraftServers.RestartAsync(serverId);
    }
}
```

---

## Modules & API Reference

### Authentication (`client.Authentication`)
Provides endpoints to sign in and out of the API.

```csharp
// Login with a 2FA code (if enabled)
var response = await client.Authentication.LoginAsync("Username", "Password", tfa: 123456);

// Logout (invalidates the current token server-side)
await client.Authentication.LogoutAsync();
```

### User Profile (`client.User`)
Retrieves details of the current user account.

```csharp
var profile = await client.User.GetProfileAsync();
Console.WriteLine($"User: {profile.Name}");
Console.WriteLine($"Email: {profile.Email}");
Console.WriteLine($"Balance: {profile.Money} EUR");
```

### Minecraft Servers (`client.MinecraftServers`)
Management of Minecraft servers.

```csharp
// List all Minecraft servers
var response = await client.MinecraftServers.GetAllAsync();

// Retrieve status
var status = await client.MinecraftServers.GetStatusAsync(serverId);
Console.WriteLine($"Status: {status.Data?.Status}");
Console.WriteLine($"Players: {status.Data?.OnlinePlayers}/{status.Data?.MaxPlayers}");

// Power controls
await client.MinecraftServers.StartAsync(serverId);
await client.MinecraftServers.StopAsync(serverId);
await client.MinecraftServers.RestartAsync(serverId);

// Backup management
var backups = await client.MinecraftServers.GetBackupsAsync(serverId);
await client.MinecraftServers.CreateBackupAsync(serverId);
```

### TeamSpeak Servers (`client.TeamSpeakServers`)
Management of TeamSpeak servers.

```csharp
// List all TeamSpeak servers
var tsServers = await client.TeamSpeakServers.GetAllAsync();

// Get status and control power states
var tsStatus = await client.TeamSpeakServers.GetStatusAsync(ts3ServerId);
await client.TeamSpeakServers.StartAsync(ts3ServerId);
await client.TeamSpeakServers.StopAsync(ts3ServerId);
await client.TeamSpeakServers.RestartAsync(ts3ServerId);
```

### Root Servers (`client.RootServers`)
Control and monitor dedicated or virtual root servers (vServers).

```csharp
// List servers & check status
var rootServers = await client.RootServers.GetAllAsync();
var status = await client.RootServers.GetStatusAsync(serverId);

// Power controls
await client.RootServers.StartAsync(serverId);
await client.RootServers.ShutdownAsync(serverId); // Soft shutdown
await client.RootServers.StopAsync(serverId);     // Hard stop (Power off)
await client.RootServers.RestartAsync(serverId);

// Manage backups
var backups = await client.RootServers.GetBackupsAsync(serverId);
await client.RootServers.CreateBackupAsync(serverId);
await client.RootServers.RestoreBackupAsync(serverId, backupId);
await client.RootServers.DeleteBackupAsync(serverId, backupId);

// Retrieve VNC console (for remote access)
var vnc = await client.RootServers.GetVncAsync(serverId);
Console.WriteLine($"VNC URL: {vnc.Data?.Url}");

// Retrieve RRD data (statistics such as CPU, RAM, Disk)
var rrd = await client.RootServers.GetRrdDataAsync(serverId, RootServerRrdTimeframes.Day);
```

### Domains & DNS (`client.Domains`)
Management of registered domains, DNS records, and email accounts.

```csharp
// Retrieve DNS records and email accounts
var info = await client.Domains.GetInfoAsync(domainId);
var records = info.Data?.Records;
var emails = info.Data?.Emails;

// Get available DNS record types
var recordTypes = await client.Domains.GetAvailableRecordTypesAsync();

// Create a new DNS record (e.g., A-Record)
var newRecord = new DomainRecord
{
    Name = "subdomain",
    Type = DomainRecordTypes.A,
    Content = "192.168.1.1",
    Ttl = 3600
};
var createdRecord = await client.Domains.CreateDnsRecordAsync(domainId, newRecord);

// Delete a DNS record
await client.Domains.DeleteDnsRecordAsync(domainId, recordId);

// Create an email account
var newEmail = new DomainEmail
{
    LocalPart = "info",
    Password = "SecurePassword123!"
};
await client.Domains.CreateEmailAsync(domainId, newEmail);

// Delete an email account
await client.Domains.DeleteEmailAsync(domainId, emailId);
```

### Services (`client.Services`)
Runtime extensions for active services.

```csharp
// Get renewal price
var priceInfo = await client.Services.GetRenewPriceAsync(serviceId);
Console.WriteLine($"Renewal costs: {priceInfo.Data?.Price} EUR for {priceInfo.Data?.Runtime} days");

// Renew service (e.g., for 30 days)
await client.Services.RenewAsync(serviceId, "30");
```

### Support Tickets (`client.Tickets`)
Communication with the MC-HOST24 customer support.

```csharp
// Get ticket categories and available services
var info = await client.Tickets.GetInfoAsync();

// List all support tickets
var tickets = await client.Tickets.GetAllAsync();

// Create a support ticket
var ticketResponse = await client.Tickets.CreateAsync(
    subject: "Connection Issues",
    text: "I am unable to access my server via SFTP.",
    serviceId: myServiceId,
    ticketCategoryId: categoryId
);

// Reply to a ticket
await client.Tickets.ReplyAsync(ticketId, "Here is the exact log excerpt: ...");

// Close or reopen a ticket
await client.Tickets.CloseAsync(ticketId);
await client.Tickets.ReopenAsync(ticketId);
```

---

## Configuration Options

Use `McHost24ClientOptions` to customize the client. This is useful for environments where a custom `HttpClient` should be used (e.g., when integrating into ASP.NET Core via `IHttpClientFactory`):

```csharp
var options = new McHost24ClientOptions
{
    ApiToken = "your-persistent-token",
    BaseAddress = new Uri("https://mc-host24.de/api/v1/"), // Default value
    JsonSerializerOptions = new JsonSerializerOptions { /* Custom JSON configuration */ }
};

// Instantiate with a custom HttpClient and options
var client = new McHost24Client(myCustomHttpClient, options);
```

### Dependency Injection Integration (ASP.NET Core)

Register the client in your `Program.cs`:

```csharp
builder.Services.AddHttpClient<McHost24Client>((httpClient, sp) =>
{
    var options = new McHost24ClientOptions
    {
        ApiToken = builder.Configuration["McHost24:ApiToken"]
    };
    return new McHost24Client(httpClient, options);
});
```

---

## Error Handling

By default, API calls return an `ApiResponse` or `ApiResponse<TData>` object containing fields such as `Success`, `Status`, and `Messages`.

If an HTTP communication error occurs (e.g., network issues or invalid JSON response), the client throws a `McHost24ApiException`.

```csharp
try
{
    var profile = await client.User.GetProfileAsync();
    Console.WriteLine($"Username: {profile.Name}");
}
catch (McHost24ApiException ex)
{
    Console.WriteLine($"API Error: {ex.Message}");
    if (ex.StatusCode.HasValue)
    {
        Console.WriteLine($"HTTP Status Code: {ex.StatusCode}");
        Console.WriteLine($"Response Content: {ex.ResponseContent}");
    }
}
```

---

## License

This project is licensed under the **MIT License**. See the LICENSE.txt file for details.

---

## Disclaimer

This is an unofficial .NET wrapper for the MC-HOST24 API. The project is not affiliated with MC-HOST24 GmbH. All brand names and logos are property of their respective owners.
