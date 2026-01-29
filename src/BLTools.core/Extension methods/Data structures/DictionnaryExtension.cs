using System.Collections;

namespace BLTools.Core;

/// <summary>
/// Extension for dictionaries
/// </summary>
public static class DictionaryExtension {

  /// <summary>
  /// Get a value from a dictionay, with a default value in case of error
  /// </summary>
  /// <typeparam name="K">The type of the key</typeparam>
  /// <typeparam name="T">The type of the requested value</typeparam>
  /// <param name="source">The source dictionary</param>
  /// <param name="key">The key</param>
  /// <param name="defaultValue">The value to return in case of error</param>
  /// <returns>The requested value or the default value in case of error</returns>
  public static T? SafeGetValue<K, T>(this IDictionary<K, T> source, K? key, T? defaultValue) {

    if (key is null) {
      return defaultValue;
    }

    if (source is null || source.IsEmpty()) {
      return defaultValue;
    }

    if (source.ContainsKey(key)) {
      return source[key];
    } else {
      return defaultValue;
    }
  }

  ///// <summary>
  ///// Indicate if an IDictionary is empty
  ///// </summary>
  ///// <param name="source">The IDictionary to check</param>
  ///// <returns><see langword="true"/> when the IDictionary is empty, <see langword="false"/> otherwise</returns>
  //public static bool IsEmpty(this IDictionary source) {
  //  return source.Count == 0;
  //}

  public static string ListDictionaryItems(this IDictionary source, int indent = 2) {
    StringBuilder RetVal = new();
    int Index = 0;
    foreach (DictionaryEntry DictItem in source) {
      RetVal.AppendIndent($"[{Index++}] {GetDictionaryEntryAsString(DictItem)}", indent + 2);
      RetVal.AppendLine();
    }
    return RetVal.ToString();
  }

  private const string FIELDNAME_KEY = "Key";
  private const string FIELDNAME_VALUE = "Value";

  private static string GetPropertyValue(string fieldName, object source) {
    return source.GetType().GetProperty(fieldName)?.GetValue(source, null)?.ToString() ?? ILogger.VALUE_NULL;
  }

  public static string GetDictionaryEntryAsString(DictionaryEntry source) {

    StringBuilder RetVal = new("[");

    #region --- Key --------------------------------------------
    switch (source.Key) {
      case string:
        RetVal.Append(GetPropertyValue(FIELDNAME_KEY, source).WithQuotes());
        break;

      case char:
        RetVal.Append($"'{GetPropertyValue(FIELDNAME_KEY, source)}'");
        break;

      default:
        RetVal.Append(GetPropertyValue(FIELDNAME_KEY, source));
        break;

    }
    #endregion --- Key --------------------------------------------

    RetVal.Append(", ");

    #region --- Value --------------------------------------------
    switch (source.Value) {
      case null:
        RetVal.Append(ILogger.VALUE_NULL);
        break;

      case string:
        RetVal.Append(GetPropertyValue(FIELDNAME_VALUE, source).WithQuotes());
        break;

      case char:
        RetVal.Append($"'{GetPropertyValue(FIELDNAME_VALUE, source)}'");
        break;

      default:
        RetVal.Append(GetPropertyValue(FIELDNAME_VALUE, source));
        break;
    }
    #endregion --- Value --------------------------------------------

    RetVal.Append(']');
    return RetVal.ToString();
  }
}
