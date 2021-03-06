using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.WorkTypes.Data;

public class WorkTypeContext : DbContext
{
  public WorkTypeContext(DbContextOptions<WorkTypeContext> opt) : base(opt)
  {
  }

  public DbSet<WorkType> WorkTypes => Set<WorkType>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    bldr.Entity<WorkType>()
      .HasData(new WorkType[]
      {
        new WorkType()
        {
          Id = 1,
          Name = "Consulting",
          DefaultRate = 100m,
          Description = "Discussions with client."
        }
      });
  }

}
