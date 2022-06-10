using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.WorkTypes.Data;

public class WorkTypeContext : DbContext
{
  private readonly ILogger<WorkTypeContext> _logger;
  private readonly IConfiguration _config;

  public WorkTypeContext(ILogger<WorkTypeContext> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
  }

  public DbSet<WorkType> WorkTypes => Set<WorkType>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
    => builder.UseSqlServer(_config.GetConnectionString("WorkTypeDb"));
  
}
