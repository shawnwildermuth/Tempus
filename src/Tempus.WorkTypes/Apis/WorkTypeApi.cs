namespace Tempus.WorkTypes.Apis;

public class WorkTypeApi : IApi
{
  private readonly ILogger<WorkTypeApi> _logger;

  public WorkTypeApi(ILogger<WorkTypeApi> logger)
  {
    _logger = logger;
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/worktypes/", GetWorkTypes);
    app.MapGet("/api/worktypes/{id:int}", GetWorkType);
    app.MapPost("/api/worktypes/", CreateWorkType);
    app.MapPut("/api/worktypes/{id:int}", UpdateWorkType);
    app.MapDelete("/api/worktypes/{id:int}", DeleteWorkType);
  }

  public async Task<IResult> GetWorkTypes(WorkTypeContext ctx)
  {
    var results = await ctx.WorkTypes
      .OrderBy(c => c.Name)
      .ToListAsync();

    return Results.Ok(results);
  }

  public async Task<IResult> GetWorkType(WorkTypeContext ctx, int id)
  {
    var result = await LoadWorkType(ctx, id);

    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  async Task<WorkType?> LoadWorkType(WorkTypeContext ctx, int id)
  {
    return await ctx.WorkTypes
      .Where(c => c.Id == id)
      .FirstOrDefaultAsync();
  }

  public async Task<IResult> CreateWorkType(WorkTypeContext ctx, WorkType worker)
  {
    try
    {
      ctx.Add(worker);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Created($"/api/worktypes/{worker.Id}", worker);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to create work type: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> UpdateWorkType(WorkTypeContext ctx, int id, WorkType worker)
  {
    try
    {
      var old = await LoadWorkType(ctx, id);
      if (old is null) return Results.NotFound();

      ShallowCopier.Copy(worker, old);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok(old);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to update work type: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> DeleteWorkType(WorkTypeContext ctx, int id)
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
      _logger.LogError($"Failed to delete work type: {ex}");
    }

    return Results.BadRequest();
  }

}
