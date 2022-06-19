namespace Tempus.Workers.Models;

public class WorkersResult
{
  public int Count { get; set; }
  public IEnumerable<Worker> Results { get; set; } = new List<Worker>();
}