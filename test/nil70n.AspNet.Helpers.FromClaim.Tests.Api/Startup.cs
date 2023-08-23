using AspNet.Helpers.FromClaim;

namespace nil70n.AspNet.Helpers.FromClaim.Tests.Api;

public class Startup
{
  public IConfiguration Configuration { get; }

  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public void ConfigureServices(IServiceCollection services)
  {
    services.AddControllers(ClaimValueProviderFactory.AddInstance);
  }

  public void Configure(IApplicationBuilder app, IWebHostEnvironment _)
  {
    app.UseDeveloperExceptionPage();
    app.UseAuthentication();
    app.UseRouting();
    app.UseEndpoints(endpoints => endpoints.MapControllers());
  }
}
