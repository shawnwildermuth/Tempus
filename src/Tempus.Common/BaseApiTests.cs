using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tempus.Common;

public class BaseApiTests<T,C> : IDisposable where T : class where C : DbContext
{
  protected readonly WebApplicationFactory<T> _application;
  protected readonly HttpClient _client;

  protected BaseApiTests()
  {
    _application = new WebApplicationFactory<T>();
    _application.WithWebHostBuilder(bldr =>
    {
      bldr.ConfigureServices(c =>
      {
        var descriptor = c.FirstOrDefault(s => s.ServiceType == typeof(C));
        if (descriptor is null) throw new InvalidOperationException("Failed to replace Context Object in Service Collection");
        c.Remove(descriptor);
        c.AddDbContext<C>(opt =>
        {
          opt.UseInMemoryDatabase(Guid.NewGuid().ToString());
        });
      });
    });
    _client = _application.CreateClient();

    using var scope = _application.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetService<C>();
    ctx?.Database.EnsureCreated();
  }

  protected virtual void Disposing()
  {

  }

  public void Dispose()
  {
    using var scope = _application.Services.CreateScope();
    var ctx = scope.ServiceProvider.GetService<C>();
    ctx?.Database.EnsureDeleted();
    ctx?.Dispose();

    _client.Dispose();
    _application.Dispose();
    this.Disposing();

  }
}
