using System.Collections;

using BLTools.Core.Logging;

namespace BLTools.Core;

/// <summary>
/// Extensions methods for Type
/// </summary>
public static class TypeExtensions {

  extension(Type type) {
    /// <summary>
    /// Get the name of the type, including generic arguments if needed
    /// </summary>
    /// <returns>The generic name with arguments or simple name if not generic</returns>
    public  string GetNameEx() {
      if (!type.IsGenericType) {
        return type.Name;
      }

      return $"{type.Name.Before('`')}<{string.Join(", ", type.GetGenericArguments().Select(a => a.Name))}>";
    }

    /// <summary>
    /// Indicate if the type IEnumerable
    /// </summary>
    /// <returns>true if the type implements IEnumerable, false otherwise</returns>
    public  bool IsIEnumerable() {
      return type.GetInterface(nameof(IEnumerable)) is not null;
    }

    /// <summary>
    /// Indicate if the type can contain sub-infos
    /// </summary>
    /// <returns>true if the type can contain sub-infos, false otherwise</returns>
    public bool CanDigDeeper() {

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

    /// <summary>
    /// Indicates if the type can be dumped or not (DoNotDumpAttribute)
    /// </summary>
    /// <returns>true if the type can be dumped, false otherwise</returns>
    public bool CanDump() {
      return type.GetCustomAttribute<DoNotDumpAttribute>() is null;
    }
  }
}
