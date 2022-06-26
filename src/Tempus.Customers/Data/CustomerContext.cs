namespace Tempus.Customers.Data;

public class CustomerContext : DbContext
{
  public CustomerContext(DbContextOptions<CustomerContext> opt) : base(opt)
  { }

  public DbSet<Customer> Customers => Set<Customer>();

  protected override void OnModelCreating(ModelBuilder bldr)
  {

    bldr.Entity<Customer>(c =>
    {
      c.HasData(new []
      {
        new 
        {
          Id = 1,
          CompanyName = "Smith Cleaning",
          CompanyPhone = "(404) 555-2121"
        }
      });

      c.OwnsOne(c => c.Location).HasData(new
      {
        Id = 1,
        CustomerId = 1,
        LineOne = "123 Main Street",
        LineTwo = "Suite 400",
        City = "Austell",
        StateProvince = "GA",
        PostalCode = "30303",
        Country = "USA"
      });

      c.OwnsMany(d => d.Contacts).HasData(new []
      { 
        new 
        {
          CustomerId = 1,
          FirstName = "Sarah",
          LastName = "Smith",
          Title = "President",
          Id = 1
        }
      });

    });


  }
}
