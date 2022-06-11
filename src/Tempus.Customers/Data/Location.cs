using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tempus.Customers.Data;

[Table("Locations", Schema = "Tempus")]
public class Location
{
  public int Id { get; set; }
  public int CustomerId { get; set; }
  [Required]
  [MaxLength(255)]
  public string LineOne { get; set; } = "";
  [MaxLength(255)]
  public string? LineTwo { get; set; }
  [MaxLength(255)]
  public string? LineThree { get; set; }
  [MaxLength(100)]
  public string? City { get; set; }
  [MaxLength(100)]
  public string StateProvince { get; set; } = "";
  [MaxLength(50)]
  public string PostalCode { get; set; } = "";
  [MaxLength(100)]
  public string? Country { get; set; }
}