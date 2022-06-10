namespace Tempus.TimeBilling.Apis;

public class TimeBillingApi : IApi
{
  private readonly ILogger<TimeBillingApi> _logger;
  private readonly IMapper _mapper;

  public TimeBillingApi(ILogger<TimeBillingApi> logger, IMapper mapper)
  {
    _logger = logger;
    _mapper = mapper;
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/timebills/", GetWorkTypes);
    app.MapGet("/api/timebills/{id:int}", GetWorkType);
    app.MapPost("/api/timebills/", CreateWorkType);
    app.MapPut("/api/timebills/{id:int}", UpdateWorkType);
    app.MapDelete("/api/timebills/{id:int}", DeleteWorkType);
  }

  public async Task<IResult> GetWorkTypes(TimeBillingContext ctx)
  {
    var results = await ctx.TimeBills
      .OrderBy(c => c.WorkerId)
      .ToListAsync();

    return Results.Ok(results);
  }

  public async Task<IResult> GetWorkType(TimeBillingContext ctx, int id)
  {
    var result = await LoadWorkType(ctx, id);

    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  async Task<TimeBill> LoadWorkType(TimeBillingContext ctx, int id)
  {
    return await ctx.TimeBills
      .Where(c => c.Id == id)
      .FirstAsync();
  }

  public async Task<IResult> CreateWorkType(TimeBillingContext ctx, TimeBill timeBill)
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

  public async Task<IResult> UpdateWorkType(TimeBillingContext ctx, int id, TimeBill timeBill)
  {
    try
    {
      var old = await LoadWorkType(ctx, id);
      if (old is null) return Results.NotFound();

      _mapper.Map(timeBill, old);

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

  public async Task<IResult> DeleteWorkType(TimeBillingContext ctx, int id)
  {
    try
    {
      var old = await LoadWorkType(ctx, id);
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
