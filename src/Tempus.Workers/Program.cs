using Tempus.Workers.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkerContext>();

builder.Services.AddApis();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapApis();

app.Run();


