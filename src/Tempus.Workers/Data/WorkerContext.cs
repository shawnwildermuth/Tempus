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

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    bldr.Entity<Worker>()
      .HasData(new Worker[]
      {
        new Worker()
        {
          Id = 1,
          FirstName = "Shawn",
          LastName = "Wildermuth",
          BaseRate = 150m,
          Email = "shawn@aol.com",
          UserName = "shawnwildermuth",
          Phone = "(404) 555-1212"
        }
      });
  }

}
