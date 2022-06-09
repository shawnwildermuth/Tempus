using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Tempus.Common;
using Tempus.Customers.Data;

namespace Tempus.Customers.Apis;

public class CustomerApi : IApi
{
  public void Register(WebApplication app)
  {
    app.MapGet("/api/customers/", GetCustomer);
  }

  public async Task<IResult> GetCustomer(CustomerContext ctx)
  {
    var results = await ctx.Customers
      .Include(c => c.Location)
      .Include(c => c.Contacts)
      .OrderBy(c => c.CompanyName)
      .ToListAsync();

    if (results is null || results.Count() == 0) return Results.NotFound();
    return Results.Ok(results);
  }

}
