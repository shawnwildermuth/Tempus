using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerContext>(
  opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDb")));

builder.Services.AddApis();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapApis();

app.Run();

public partial class Program { }


