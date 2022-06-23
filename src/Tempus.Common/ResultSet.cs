
namespace Tempus.Common;

public class ResultSet<T> where T : new()
{
  public int Count { get; set; }
  public IEnumerable<T> Results { get; set; } = new List<T>();
}
