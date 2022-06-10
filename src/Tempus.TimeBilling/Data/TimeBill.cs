using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tempus.TimeBilling.Data;

[Table("TimeBills", Schema = "Tempus")]
public class TimeBill
{
  public int Id { get; set; }
  [Required]
  public int CustomerId { get; set; }
  [Required]
  public int WorkerId { get; set; }
  [Required]
  [MaxLength(200)]
  public string WorkPerformed { get; set; } = "";
  [MaxLength(4000)]
  public string? Notes { get; set; }
  [Required]
  [Precision(8,2)]
  public decimal Rate { get; set; }
  [Required]
  [Precision(8, 1)]
  public decimal Hours { get; set; }
}