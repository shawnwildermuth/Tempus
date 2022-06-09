using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerContext>();

builder.Services.AddApis();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapApis();

app.Run();

