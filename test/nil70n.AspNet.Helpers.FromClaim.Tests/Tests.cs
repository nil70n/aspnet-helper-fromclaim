using System.Net;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using nil70n.AspNet.Helpers.FromClaim.Tests.Api;
using nil70n.AspNet.Helpers.FromClaim.Tests.Api.Models;
using WebMotions.Fake.Authentication.JwtBearer;

namespace nil70n.AspNet.Helpers.FromClaim.Tests;

[TestClass]
public class Tests
{
    private readonly IHost _host;
    private JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };

    private static readonly Func<string, string, Dictionary<string, object>> GetClaims
        = (claimType, claimValue) => new Dictionary<string, object> { { claimType, claimValue } };

    private static readonly Action<Dictionary<string, object>, string, string> AddClaim
        = (claims, claimType, claimValue) =>
        {
            if (claimValue != null)
            {
                claims.Add(claimType, claimValue);
            }
        };

    public Tests()
    {
        _host = new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();

                webBuilder
                .UseTestServer()
                .ConfigureTestServices(collection =>
                {
                    collection
                        .AddAuthentication(FakeJwtBearerDefaults.AuthenticationScheme)
                        .AddFakeJwtBearer();
                });
            })
            .Build();
    }

    [TestMethod]
    [DataRow("email", ClaimTypes.Email, "john.doe@host.com")]
    [DataRow("name", ClaimTypes.Name, "John")]
    [DataRow("last_name", "last_name", "Doe")]
    public async Task FromClaimAttribute_RetrievesValue_WhenRespectiveClaimIsProvided(string path, string claim, string content)
    {
        await _host.StartAsync();

        var httpClient = _host.GetTestServer().CreateClient();

        httpClient.SetFakeBearerToken(GetClaims(claim, content));

        var response = await httpClient.GetAsync($"api/claims/{path}");
        var result = await response.Content.ReadAsStringAsync();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(content, result);
    }

    [TestMethod]
    [DataRow(null, null, null)]
    [DataRow("john.doe@host.com", null, null)]
    [DataRow(null, "John", null)]
    [DataRow(null, null, "Doe")]
    [DataRow(null, "John", "Doe")]
    [DataRow("john.doe@host.com", "John", "Doe")]
    public async Task FromClaimAttribute_GetCorrectValue_WhenMultipleClaimsAreUsed(string email, string name, string last_name)
    {
        await _host.StartAsync();

        var claims = new Dictionary<string, object>();

        AddClaim(claims, ClaimTypes.Email, email);
        AddClaim(claims, ClaimTypes.Name, name);
        AddClaim(claims, nameof(last_name), last_name);

        var httpClient = _host.GetTestServer().CreateClient();

        httpClient.SetFakeBearerToken(claims);

        var response = await httpClient.GetAsync($"api/claims/multiple");
        var result = await response.Content.ReadAsStringAsync();
        var person = JsonSerializer.Deserialize<Person>(result, _serializerOptions);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.AreEqual(email, person!.Email);
        Assert.AreEqual(name, person!.Name);
        Assert.AreEqual(last_name, person!.LastName);
    }
}