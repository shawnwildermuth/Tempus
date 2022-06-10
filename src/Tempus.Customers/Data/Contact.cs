using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tempus.Customers.Data;

[Table("Contacts", Schema = "Tempus")]
public class Contact
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  [MaxLength(100)]
  public string Title { get; set; } = "";
  [Required]
  [MaxLength(100)]
  public string FirstName { get; set; } = "";
  public string MiddleName { get; set; } = "";
  [Required]
  [MaxLength(100)]
  public string LastName { get; set; } = "";
  [MaxLength(50)]
  public string Phone { get; set; } = "";
  [MaxLength(100)]
  public string Email { get; set; } = "";

}