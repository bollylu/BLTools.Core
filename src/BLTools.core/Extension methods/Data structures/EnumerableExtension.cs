using System.Collections;
using System.Runtime.CompilerServices;

namespace BLTools.Core;

/// <summary>
/// Extensions for IEnumerable&lt;...&gt;
/// </summary>
public static class EnumerableExtension {

  /// <summary>
  /// Combine an enumeration of objects into a string where each items are separated by a separator (comma is default)
  /// </summary>
  /// <param name="source">The list of string items to combine</param>
  /// <param name="separator">The separator between items (", " as default)</param>
  /// <returns>The combined string</returns>
  public static string CombineToString(this IEnumerable source, string separator = ", ") {
    if (source is null || ((IEnumerable<object>)source).IsEmpty()) {
      return string.Empty;
    }

    StringBuilder RetVal = new StringBuilder();

    switch (source) {
      case IEnumerable<string> StringItems:
        foreach (var ItemItem in StringItems) {
          RetVal.Append(ItemItem).Append(separator);
        }
        break;
      default:
        foreach (var ItemItem in source) {
          RetVal.Append(ItemItem.ToString()).Append(separator);
        }
        break;
    }

    if (RetVal.Length > 0) {
      RetVal.Truncate(separator.Length);
    }

    return RetVal.ToString();
  }

  /// <summary>
  /// Indicate if an IDictionary is empty
  /// </summary>
  /// <param name="source">The IDictionary to check</param>
  /// <returns><see langword="true"/> when the IDictionary is empty, <see langword="false"/> otherwise</returns>
  public static bool IsEmpty(this IEnumerable source) {
    return ((IEnumerable<object>)source).IsEmpty();
  }
}
