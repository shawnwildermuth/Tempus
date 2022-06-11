namespace Tempus.Customers.Data;

public class CustomerContext : DbContext
{
  public CustomerContext(DbContextOptions<CustomerContext> opt) : base(opt)
  { }

  public DbSet<Customer> Customers => Set<Customer>();

}
