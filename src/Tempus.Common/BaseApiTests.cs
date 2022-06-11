using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Tempus.Common;

public class BaseApiTests<T, C> : IDisposable where T : class where C : DbContext
{
  protected readonly WebApplicationFactory<T> _application;
  protected readonly HttpClient _client;

  protected BaseApiTests()
  {
    _application = new WebApplicationFactory<T>()
    .WithWebHostBuilder(bldr =>
    {
      bldr.ConfigureServices(c =>
      {
        var ctxService = c.FirstOrDefault(s => s.ServiceType == typeof(C));
        if (ctxService is null) throw new InvalidOperationException("Failed to replace Context Object in Service Collection");
        c.Remove(ctxService);

        var options = new DbContextOptionsBuilder<C>()
          .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
          .Options;
        var dbCtx = (C?)Activator.CreateInstance(typeof(C), new object[] { options });
        if (dbCtx is not null) c.AddSingleton<C>(dbCtx);
      });
    });
    _client = _application.CreateClient();

    var ctx = _application.Services.GetService<C>();
    if (ctx?.Database.ProviderName?.Contains("SqlServer") is true)
    {
      throw new InvalidOperationException("Should never be SqlServer in a Test");
    }
    ctx?.Database.EnsureDeleted();
    ctx?.Database.EnsureCreated();
  }

  protected virtual void Disposing()
  {

  }

  public void Dispose()
  {
    _client.Dispose();
    _application.Dispose();
    this.Disposing();

  }
}
