using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tempus.Customers.Data;

[Table("Customers", Schema = "Tempus")]
public class Customer
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string CompanyName { get; set; } = "";
  [Required]
  [MaxLength(25)]
  public string CompanyPhone { get; set; } = "";
  public Location Location { get; set; } = new Location();
  public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

}
