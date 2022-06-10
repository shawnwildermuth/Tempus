using System.ComponentModel.DataAnnotations;

namespace Tempus.Workers.Data;

public class Worker
{
  public int Id { get; set; }
  [Required]
  [MaxLength(100)]
  public string UserName { get; set; } = "";
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