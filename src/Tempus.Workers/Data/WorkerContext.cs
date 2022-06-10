using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.Workers.Data;

public class WorkerContext : DbContext
{
  private readonly ILogger<WorkerContext> _logger;
  private readonly IConfiguration _config;

  public WorkerContext(ILogger<WorkerContext> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
  }

  public DbSet<Worker> Workers => Set<Worker>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
    => builder.UseSqlServer(_config.GetConnectionString("WorkerDb"));
  
}
