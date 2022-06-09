using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Tempus.Customers.Data;

public class CustomerContext : DbContext
{
  private readonly ILogger<CustomerContext> _logger;
  private readonly IConfiguration _config;

  public CustomerContext(ILogger<CustomerContext> logger, IConfiguration config)
  {
    _logger = logger;
    _config = config;
  }

  public DbSet<Customer> Customers => Set<Customer>();

  protected override void OnConfiguring(DbContextOptionsBuilder builder)
    => builder.UseSqlServer(_config.GetConnectionString("CustomerDb"));
  
}
