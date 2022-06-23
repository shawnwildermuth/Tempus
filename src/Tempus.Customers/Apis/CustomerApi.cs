namespace Tempus.Customers.Apis;

public class CustomerApi : IApi
{
  private readonly ILogger<CustomerApi> _logger;
  
  public CustomerApi(ILogger<CustomerApi> logger)
  {
    _logger = logger;
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/customers/", GetCustomers);
    app.MapGet("/api/customers/{id:int}", GetCustomer);
    app.MapPost("/api/customers/", CreateCustomer);
    app.MapPut("/api/customers/{id:int}", UpdateCustomer);
    app.MapDelete("/api/customers/{id:int}", DeleteCustomer);
  }

  public async Task<IResult> GetCustomers(CustomerContext ctx)
  {
    var results = await ctx.Customers
      .Include(c => c.Location)
      .Include(c => c.Contacts)
      .OrderBy(c => c.CompanyName)
      .ToListAsync();

    return Results.Ok(new ResultSet<Customer>() { Count = results.Count(), Results = results });
  }

  public async Task<IResult> GetCustomer(CustomerContext ctx, int id)
  {
    var result = await LoadCustomer(ctx, id);

    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  async Task<Customer?> LoadCustomer(CustomerContext ctx, int id)
  {
    return await ctx.Customers
      .Include(c => c.Location)
      .Include(c => c.Contacts)
      .Where(c => c.Id == id)
      .FirstOrDefaultAsync();
  }

  public async Task<IResult> CreateCustomer(CustomerContext ctx, Customer customer)
  {
    try
    {
      ctx.Add(customer);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Created($"/api/customers/{customer.Id}", customer);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to create customer: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> UpdateCustomer(CustomerContext ctx, int id, Customer customer)
  {
    try
    {
      var old = await LoadCustomer(ctx, id);
      if (old is null) return Results.NotFound();

      ShallowCopier.Copy(customer, old);

      foreach (var contact in customer.Contacts)
      {
        var oldContact = old.Contacts.FirstOrDefault(c => c.Id == contact.Id);
        if (oldContact is not null)
        {
          if (!ShallowCopier.Copy(contact, oldContact))
          {
            throw new InvalidOperationException("Failed to copy contacts");
          }  
        }
        else
        {
          old.Contacts.Add(contact);
        }
      }

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok(old);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to update customer: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> DeleteCustomer(CustomerContext ctx, int id)
  {
    try
    {
      var old = await LoadCustomer(ctx, id);
      if (old is null) return Results.NotFound();

      ctx.Remove(old);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok();
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to delete customer: {ex}");
    }

    return Results.BadRequest();
  }

}
