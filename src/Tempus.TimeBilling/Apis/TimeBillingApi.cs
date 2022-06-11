namespace Tempus.TimeBilling.Apis;

public class TimeBillingApi : IApi
{
  private readonly ILogger<TimeBillingApi> _logger;

  public TimeBillingApi(ILogger<TimeBillingApi> logger)
  {
    _logger = logger;
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/timebills/", GetTimeBills);
    app.MapGet("/api/timebills/{id:int}", GetTimeBill);
    app.MapPost("/api/timebills/", CreateTimeBill);
    app.MapPut("/api/timebills/{id:int}", UpdateTimeBill);
    app.MapDelete("/api/timebills/{id:int}", DeleteTimeBill);
  }

  public async Task<IResult> GetTimeBills(TimeBillingContext ctx)
  {
    var results = await ctx.TimeBills
      .OrderBy(c => c.WorkerId)
      .ToListAsync();

    return Results.Ok(results);
  }

  public async Task<IResult> GetTimeBill(TimeBillingContext ctx, int id)
  {
    var result = await LoadTimeBill(ctx, id);

    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  async Task<TimeBill?> LoadTimeBill(TimeBillingContext ctx, int id)
  {
    return await ctx.TimeBills
      .Where(c => c.Id == id)
      .FirstOrDefaultAsync();
  }

  public async Task<IResult> CreateTimeBill(TimeBillingContext ctx, TimeBill timeBill)
  {
    try
    {
      ctx.Add(timeBill);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Created($"/api/timebills/{timeBill.Id}", timeBill);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to create time bill: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> UpdateTimeBill(TimeBillingContext ctx, int id, TimeBill timeBill)
  {
    try
    {
      var old = await LoadTimeBill(ctx, id);
      if (old is null) return Results.NotFound();

      ShallowCopier.Copy(timeBill, old);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok(old);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to update time bill: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> DeleteTimeBill(TimeBillingContext ctx, int id)
  {
    try
    {
      var old = await LoadTimeBill(ctx, id);
      if (old is null) return Results.NotFound();

      ctx.Remove(old);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok();
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to delete time bill: {ex}");
    }

    return Results.BadRequest();
  }

}
