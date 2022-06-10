using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tempus.WorkTypes.Data;

[Table("WorkTypes", Schema = "Tempus")]
public class WorkType
{
  public int Id { get; set; }
  [Required]
  [MaxLength(50)]
  public string Name { get; set; } = "";
  [MaxLength(4000)]
  public string? Description { get; set; }
  [Required]
  [Precision(8,2)]
  public decimal DefaultRate { get; set; }
}