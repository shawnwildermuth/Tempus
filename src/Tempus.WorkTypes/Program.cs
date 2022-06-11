var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkTypeContext>(
  opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("WorkTypeDb")));

builder.Services.AddApis();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapApis();

app.Run();

public partial class Program { };

