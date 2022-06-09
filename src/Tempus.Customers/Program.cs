var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();


app.MapGet("/api/customers/", () =>
{
  return new[] { new { id = 1, name = "Acme Cannon Company" } };
});

app.Run();

