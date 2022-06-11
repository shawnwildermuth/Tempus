using System.Collections;

namespace Tempus.Common;

public static class ShallowCopier
{
  /// <summary>
  /// Copies the specified source. Only non-collections and objects will be copied. You should use this to copy child objects as necessary.
  /// </summary>
  /// <param name="source">The source.</param>
  /// <param name="destination">The destination.</param>
  /// <returns>Success</returns>
  public static bool Copy<T>(T source, T destination)
  {
    try
    {
      var theType = typeof(T);
      var props = theType.GetProperties();
      foreach (var prop in props)
      {
        var propType = prop.GetType();
        if (!prop.CanRead)
        {
          continue;
        }
        if (prop == null)
        {
          continue;
        }
        if (!prop.CanWrite)
        {
          continue;
        }
        if (prop.GetSetMethod(true) != null && prop.GetSetMethod(true)?.IsPrivate == true)
        {
          continue;
        }
        if ((prop.GetSetMethod()?.Attributes & MethodAttributes.Static) != 0)
        {
          continue;
        }
        if (propType.IsAssignableFrom(typeof(IEnumerable)))
        {
          continue;
        }
        prop.SetValue(destination, prop.GetValue(source, null), null);
      }

      return true;
    }
    catch (Exception)
    {
      // NOOP
    }

    return false;
  }
}
