using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.TimeBilling.Data;

public class TimeBillingContext : DbContext
{
  private readonly ILogger<TimeBillingContext> _logger;
  private readonly IConfiguration _config;

  public TimeBillingContext(ILogger<TimeBillingContext> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
  }


  public DbSet<TimeBill> TimeBills => Set<TimeBill>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
    => builder.UseSqlServer(_config.GetConnectionString("TimeBillingDb"));
  
}
