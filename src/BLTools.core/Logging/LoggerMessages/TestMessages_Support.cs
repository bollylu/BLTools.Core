using System.Collections;
using System.Reflection;
using System.Text;

using BLTools.Core;

namespace BLTools.Core.Diagnostic;

internal static class TMessageLoggerSupport {

  public const string VALUE_NULL = "(null)";

  private const string FIELDNAME_KEY = "Key";
  private const string FIELDNAME_VALUE = "Value";

  private static readonly string TYPE_STRING = typeof(string).Name;

  internal static string GetPropertyValue(string fieldName, object source) {
    return source.GetType().GetProperty(fieldName)?.GetValue(source, null)?.ToString() ?? VALUE_NULL;
  }

  internal static string GetFieldValue(string fieldName, object source) {
    return source.GetType().GetField(fieldName)?.GetValue(source)?.ToString() ?? VALUE_NULL;
  }

  internal static string GetDictionaryEntryAsString(DictionaryEntry source) {

    StringBuilder RetVal = new("[");

    #region --- Key --------------------------------------------
    Type TypeOfKey = source.Key.GetType();

    if (TypeOfKey == typeof(string)) {
      RetVal.Append(GetPropertyValue(FIELDNAME_KEY, source).WithQuotes());

    } else if (TypeOfKey == typeof(char)) {
      RetVal.Append($"'{GetPropertyValue(FIELDNAME_KEY, source)}'");

    } else {
      RetVal.Append(GetPropertyValue(FIELDNAME_KEY, source));

    }
    #endregion --- Key --------------------------------------------

    RetVal.Append(", ");

    #region --- Value --------------------------------------------
    if (source.Value is not null) {
      Type TypeOfValue = source.Value.GetType();

      if (TypeOfValue == typeof(string)) {
        RetVal.Append(GetPropertyValue(FIELDNAME_KEY, source).WithQuotes());

      } else if (TypeOfValue == typeof(char)) {
        RetVal.Append($"'{GetPropertyValue(FIELDNAME_KEY, source)}'");

      } else {
        RetVal.Append(GetPropertyValue(FIELDNAME_KEY, source));
      }
    } else {
      RetVal.Append(VALUE_NULL);
    }
    #endregion --- Value --------------------------------------------

    RetVal.Append(']');
    return RetVal.ToString();
  }

  internal static string ListDictionaryItems(IDictionary source, int depth = 1, int indent = 2) {
    StringBuilder RetVal = new();
    int Index = 0;
    foreach (DictionaryEntry DictItem in source) {
      RetVal.AppendIndent($"[{Index++}] {GetDictionaryEntryAsString(DictItem)}", indent + 2);
      RetVal.AppendLine();
    }
    return RetVal.ToString();
  }

  /// <summary>
  /// Indicate if the type is a class or an interface
  /// </summary>
  /// <param name="type"></param>
  /// <returns></returns>
  internal static bool CanDigDeeper(this Type type) {

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

      return true;
    }

    return type.IsClass || type.IsInterface;
  }

  internal static bool IsIEnumerable(this Type type) {
    return type.GetInterface(nameof(IEnumerable)) is not null;
  }

  internal static bool CanDump(this Type type) {
    return type.GetCustomAttribute(typeof(DoNotDumpAttribute)) is null;
  }

  internal static bool CanDump(this PropertyInfo propertyInfo) {
    return propertyInfo.GetCustomAttribute(typeof(DoNotDumpAttribute)) is null;
  }

  internal static bool CanDump(this FieldInfo fieldInfo) {
    return fieldInfo.GetCustomAttribute(typeof(DoNotDumpAttribute)) is null;
  }

}
