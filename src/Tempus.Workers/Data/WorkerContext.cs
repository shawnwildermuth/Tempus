using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.Workers.Data;

public class WorkerContext : DbContext
{
  public WorkerContext(DbContextOptions<WorkerContext> opt) : base(opt)
  {
  }

  public DbSet<Worker> Workers => Set<Worker>();
  
}
