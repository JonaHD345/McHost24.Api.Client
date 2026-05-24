# McHost24.Api.Client

Strongly typed .NET wrapper for the MC-HOST24 public API.

```csharp
using McHost24.Api.Client;

using var client = new McHost24Client();

var login = await client.Authentication.LoginAsync("MyAccountName", "MyPassword");
var servers = await client.MinecraftServers.GetAllAsync();

await client.MinecraftServers.RestartAsync(servers.Data![0].Id!.Value);
```

The client can also be created with an existing `HttpClient`:

```csharp
var options = new McHost24ClientOptions
{
  ApiToken = "your-api-token"
};

var client = new McHost24Client(httpClient, options);
```
