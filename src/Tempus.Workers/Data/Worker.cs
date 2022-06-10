using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tempus.Workers.Data;

[Table("Workers", Schema = "Tempus")]
public class Worker
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string UserName { get; set; } = "";
  [Precision(8,2)]
  public decimal BaseRate { get; set; }
  [MaxLength(50)]
  public string? FirstName { get; set; }
  [MaxLength(50)]
  public string? LastName { get; set; }
  [MaxLength(100)]
  public string? Email { get; set; }
  [MaxLength(25)]
  public string? Phone { get; set; }
}