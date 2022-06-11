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
  public static bool Copy(object source, object destination)
  {
    try
    {
      var theType = source.GetType();
      var props = theType.GetProperties();
      foreach (var prop in props)
      {
        var propType = prop.PropertyType;
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
        if (typeof(IEnumerable).IsAssignableFrom(propType) && propType.IsGenericType)
        {
          continue;
        }
        var sourceValue = prop.GetValue(source, null);
        var destValue = prop.GetValue(destination, null);
        if (propType.GetConstructor(new Type[] { }) is not null)
        {
          // Class
          if ((sourceValue is null || destValue is null))
          {
            prop.SetValue(destination, sourceValue, null);
          }
          else
          {
            if (!Copy(sourceValue!, destValue!))
            {
              return false;
            };
          }

        }
        else
        {
          if ((sourceValue is null || destValue is null) 
            || !(sourceValue.Equals(destValue)))
          {
              prop.SetValue(destination, prop.GetValue(source, null), null);
          }
        }
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
