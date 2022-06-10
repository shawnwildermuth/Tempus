var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WorkTypeContext>();

builder.Services.AddApis();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

var app = builder.Build();

app.UseHttpsRedirection();

app.MapApis();

app.Run();

