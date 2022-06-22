var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TimeBillingContext>(
  opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("TimeBillingDb")));

builder.Services.AddApis();

var app = builder.Build();

app.MapApis();

app.Run();

public partial class Program { }

