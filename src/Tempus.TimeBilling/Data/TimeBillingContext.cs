using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.TimeBilling.Data;

public class TimeBillingContext : DbContext
{
  public TimeBillingContext(DbContextOptions<TimeBillingContext> opt) : base(opt) 
  { 
  }


  public DbSet<TimeBill> TimeBills => Set<TimeBill>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {
    bldr.Entity<TimeBill>()
      .HasData(new TimeBill[]
      {
        new TimeBill()
        {
          Id = 1,
          CustomerId = 1,
          Hours = 1.3m,
          Rate = 135m,
          WorkerId = 1,
          WorkPerformed = "Consulting",
          Notes = "Worked with the client on architecture."
        }
      });
  }

}
