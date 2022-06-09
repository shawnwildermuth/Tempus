using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Tempus.Common;

using Tempus.Customers.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CustomerContext>();

builder.Services.AddApis();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapApis();

app.Run();

