using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

var proxyBuilder = builder.Services.AddReverseProxy();
proxyBuilder.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddCors(opt =>
{

  opt.AddPolicy("tempus", cfg =>
  {
    //if (builder.Environment.IsDevelopment())
    //{
      // Change for production!
      cfg.AllowAnyHeader();
      cfg.AllowAnyMethod();
      cfg.AllowAnyOrigin();
    //}
  });
});

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();

app.Run();

