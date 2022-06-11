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

}
