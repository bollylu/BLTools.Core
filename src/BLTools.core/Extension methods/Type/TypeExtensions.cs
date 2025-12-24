using System.Collections;

using BLTools.Core.Logging;

namespace BLTools.Core;

/// <summary>
/// Extensions methods for Type
/// </summary>
public static class TypeExtensions {

  /// <summary>
  /// Get the name of the type, including generic arguments if needed
  /// </summary>
  /// <param name="type">The type to evaluate</param>
  /// <returns>The generic name with arguments or simple name if not generic</returns>
  public static string GetNameEx(this Type type) {
    if (!type.IsGenericType) {
      return type.Name;
    }

    return $"{type.Name.Before('`')}<{string.Join(", ", type.GetGenericArguments().Select(a => a.Name))}>";
  }

  /// <summary>
  /// Indicate if the type IEnumerable
  /// </summary>
  /// <param name="type">The type to evaluate</param>
  /// <returns></returns>
  public static bool IsIEnumerable(this Type type) {
    return type.GetInterface(nameof(IEnumerable)) is not null;
  }

  /// <summary>
  /// Indicate if the type can contain sub-infos
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  public static bool CanDigDeeper(this Type type) {

    if (type.IsClass || type.IsInterface) { return true; }

    if (type == typeof(string)) {
      return false;
    }

    if (type == typeof(int) || type == typeof(long) || type == typeof(float) || type == typeof(double) || type == typeof(decimal) || type == typeof(byte)) {
      return false;
    }

    if (type == typeof(uint) || type == typeof(ulong) || type == typeof(sbyte)) {
      return false;
    }

    if (type == typeof(DateTime) || type == typeof(DateOnly) || type == typeof(TimeOnly)) {
      return false;
    }

    if (type.IsValueType) {

      if (type.IsEnum) {
        return false;
      }

      if (type.IsPrimitive) {
        return false;
      }

    }

    return true;
  }



  public static bool CanDump(this Type type) {
    return type.GetCustomAttribute<DoNotDumpAttribute>() is null;
  }

}
