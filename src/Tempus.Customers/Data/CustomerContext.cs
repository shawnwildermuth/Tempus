namespace Tempus.Customers.Data;

public class CustomerContext : DbContext
{
  public CustomerContext() { }

  public DbSet<Customer> Customers => Set<Customer>();

}
