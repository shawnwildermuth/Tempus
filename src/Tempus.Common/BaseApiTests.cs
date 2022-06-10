using Microsoft.AspNetCore.Mvc.Testing;

namespace Tempus.Common;

public class BaseApiTests<T> : IDisposable where T : class
{
  protected readonly WebApplicationFactory<T> _application;
  protected readonly HttpClient _client;

  protected BaseApiTests()
  {
    _application = new WebApplicationFactory<T>();
    _client = _application.CreateClient();
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
