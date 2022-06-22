using Tempus.Proxy;

using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Setup Proxy
builder.Services.AddTransient<IProxyConfigProvider, YarpConfig>();
var proxyBuilder = builder.Services.AddReverseProxy();

builder.Services.AddCors(opt =>
{

  opt.AddPolicy("tempus", cfg =>
  {
    if (builder.Environment.IsDevelopment())
    {
      // Change for production!

      cfg.AllowAnyHeader();
      cfg.AllowAnyMethod();
      cfg.AllowAnyOrigin();
    }
  });
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors();

app.MapReverseProxy();

app.Run();

