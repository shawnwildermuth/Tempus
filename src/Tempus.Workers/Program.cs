var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkerContext>(
  opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WorkerDb")));

builder.Services.AddApis();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapApis();

app.Run();


public partial class Program { }