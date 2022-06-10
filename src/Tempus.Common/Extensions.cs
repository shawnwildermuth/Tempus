using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Tempus.Common;
public static class Extensions
{
  public static void AddApis(this IServiceCollection services, 
    Assembly? containerAssembly = null)
  {
    if (containerAssembly is null) containerAssembly = Assembly.GetCallingAssembly()!;
    var apis = containerAssembly.GetTypes().AsQueryable().Where(t => t.GetInterfaces().Contains(typeof(IApi)));
    foreach (var api in apis) services.AddTransient(typeof(IApi), api);
  }

  public static WebApplication MapApis(this WebApplication app)
  {
    var apis = app.Services.GetServices<IApi>();
    foreach (var api in apis) api.Register(app);
    return app;
  }
}
