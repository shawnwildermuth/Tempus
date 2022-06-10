﻿using AutoMapper;

using Tempus.Workers.Data;

namespace Tempus.Workers.Apis;

public class WorkerApi : IApi
{
  private readonly ILogger<WorkerApi> _logger;
  private readonly IMapper _mapper;

  public WorkerApi(ILogger<WorkerApi> logger, IMapper mapper)
  {
    _logger = logger;
    _mapper = mapper;
  }

  public void Register(WebApplication app)
  {
    app.MapGet("/api/workers/", GetWorkers);
    app.MapGet("/api/workers/{id:int}", GetWorker);
    app.MapPost("/api/workers/", CreateWorker);
    app.MapPut("/api/workers/{id:int}", UpdateWorker);
    app.MapDelete("/api/workers/{id:int}", DeleteWorker);
  }

  public async Task<IResult> GetWorkers(WorkerContext ctx)
  {
    var results = await ctx.Workers
      .OrderBy(c => c.LastName)
      .ToListAsync();

    if (results is null || results.Count() == 0) return Results.NotFound();
    return Results.Ok(results);
  }

  public async Task<IResult> GetWorker(WorkerContext ctx, int id)
  {
    var result = await LoadWorker(ctx, id);

    if (result is null) return Results.NotFound();
    return Results.Ok(result);
  }

  async Task<Worker> LoadWorker(WorkerContext ctx, int id)
  {
    return await ctx.Workers
      .Where(c => c.Id == id)
      .FirstAsync();
  }

  public async Task<IResult> CreateWorker(WorkerContext ctx, Worker worker)
  {
    try
    {
      ctx.Add(worker);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Created($"/api/workers/{worker.Id}", worker);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to create worker: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> UpdateWorker(WorkerContext ctx, int id, Worker worker)
  {
    try
    {
      var old = await LoadWorker(ctx, id);
      if (old is null) return Results.NotFound();

      _mapper.Map(worker, old);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok(old);
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to update worker: {ex}");
    }

    return Results.BadRequest();
  }

  public async Task<IResult> DeleteWorker(WorkerContext ctx, int id)
  {
    try
    {
      var old = await LoadWorker(ctx, id);
      if (old is null) return Results.NotFound();

      ctx.Remove(old);

      if (await ctx.SaveChangesAsync() > 0)
      {
        return Results.Ok();
      }
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to delete worker: {ex}");
    }

    return Results.BadRequest();
  }

}
