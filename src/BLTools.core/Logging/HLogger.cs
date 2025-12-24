using System.Collections;

namespace BLTools.Core.Logging;
public static class HLogger {

  //private const string DUMP_METHOD = "Dump";

  public static string BuildTitle<T>(T source, string? sourceName) {
    Type SourceType = typeof(T);

    StringBuilder RetVal = new($"{sourceName} => {SourceType.GetNameEx()}");

    if (SourceType.IsEnum) {
      bool HasFlags = SourceType.GetCustomAttributes().Any(a => a.GetType() == typeof(FlagsAttribute));
      if (HasFlags) {
        RetVal.Append(" (Flags)");
      }

    } else if (SourceType.IsIEnumerable()) {
      IEnumerable<object> EnumerableItems = (source as IEnumerable ?? throw new ApplicationException("Conversion error")).Cast<object>();
      RetVal.Append($" ({EnumerableItems.Count()})");

    } else if (SourceType is IDictionary DictionaryItems) {
      RetVal.Append($" ({DictionaryItems.Count})");

    }

    if (SourceType.IsInterface || SourceType.IsAbstract) {
      Type? SourceClassType = source?.GetType();
      if (SourceClassType is not null) {
        if (SourceClassType.BaseType is not null && SourceClassType.BaseType != typeof(object)) {
          RetVal.Append($" : {SourceClassType.BaseType.GetNameEx()}");
        }
        RetVal.Append($" : {SourceClassType.GetNameEx()}");
      }
    }

    return RetVal.ToString();
  }

  /// <summary>
  /// Build a box with a title and the content of an enumeration
  /// </summary>
  /// <param name="title">The title</param>
  /// <param name="objects">The enumeration</param>
  /// <param name="width">The width of the box</param>
  /// <returns>A message in a box</returns>
  static internal string BuildBoxMessageFromEnumerable(string title, IEnumerable objects, int width = GlobalSettings.DEBUG_BOX_WIDTH) {

    if (objects is null) {
      return BuildBox(title, "IEnumerable objects is null", width);
    }

    if (((IEnumerable<object>)objects).IsEmpty()) {
      return BuildBox(title, $"{objects.GetType().GetNameEx()} is empty", width);
    }

    StringBuilder RetVal = new();
    foreach (object ObjectItem in objects) {
      RetVal.AppendLine(ObjectItem.ToString());
    }
    return BuildBox(title, RetVal.ToString(), width);
  }

  /// <summary>
  /// Build a box with a title and the content of a dictionary
  /// </summary>
  /// <param name="title">The title</param>
  /// <param name="objects">The dictionary</param>
  /// <param name="width">The width of the box</param>
  /// <returns>A message in a box</returns>
  static internal string BuildBoxMessageFromDictionary(string title, IDictionary objects, int width = ILogger.NO_WIDTH) {

    if (objects is null) {
      return BuildBox(title, ILogger.VALUE_NULL, width);
    }

    if (objects.IsEmpty()) {
      return BuildBox(title, $"{objects.GetType().GetNameEx()} is empty", width);
    }

    return BuildBox(title, objects.ListDictionaryItems(), width);
  }

  /// <summary>
  /// Build a box with a title and an exception as content
  /// </summary>
  /// <param name="title">The title of the box</param>
  /// <param name="ex">The exception</param>
  /// <param name="withStackTrace">Add the full stack trace when <see langword="true"/> (default=<see langword="false"/>)</param>
  /// <param name="width">The width of the box</param>
  /// <returns>A message in a box</returns>
  static internal string BuildBoxMessageFromException(string title, Exception ex, bool withStackTrace, int width = ILogger.NO_WIDTH) {

    if (ex is null) {
      return BuildBox(title, "Exception is null", width);
    }

    StringBuilder sb = new();
    sb.AppendLine($"Exception type : {ex.GetType().GetNameEx()}");
    sb.AppendLine($"Exception source : {ex.Source ?? "[No source available]"}");
    sb.AppendLine($"Message: {ex.Message}");
    sb.AppendLine($"TargetSite : {ex.TargetSite?.ToString() ?? ILogger.VALUE_NULL}");
    if (withStackTrace) {
      sb.AppendLine(ex.StackTrace);
    }
    return BuildBox(title, sb.ToString(), width);
  }

  /// <summary>
  /// Build a box with a title and a message
  /// </summary>
  /// <param name="title">The title of the box</param>
  /// <param name="message">The message inside the box</param>
  /// <param name="width">The width of the box</param>
  /// <returns>A message in a box</returns>
  static internal string BuildBox(string title, string message, int width = ILogger.NO_WIDTH) {
    return message.BoxFixedWidth(title ?? "", width);
  }

  /// <summary>
  /// Build a box with a title and the content of an object
  /// </summary>
  /// <param name="title">The title of the box</param>
  /// <param name="message">The object converted into a string</param>
  /// <param name="width">The width of the box</param>
  /// <returns>A message in a box</returns>
  static internal string BuildBox(string title, object? message, int width = ILogger.NO_WIDTH) {
    return (message?.ToString() ?? "(null)").BoxFixedWidth(title ?? "", width);
  }

  /***************************************************************************************************************************/

  /// <summary>
  /// Text for unkown type when logging
  /// </summary>
  internal const string UNKNOWN_TYPE = "(unknown type)";

  /// <summary>
  /// Build a string with caller information
  /// </summary>
  /// <param name="callerType">The type of the caller</param>
  /// <param name="callerMethod">The method that called</param>
  /// <returns>A combination of caller type and caller method</returns>
  static internal string BuildCaller(Type? callerType, string callerMethod) {
    if (callerType is null) {
      return $"{UNKNOWN_TYPE}.{callerMethod}";
    }

    if (callerType.IsGenericType) {
      return $"{callerType.GetGenericArguments()[0].Name}.{callerMethod}";
    }

    return $"{callerType.GetNameEx()}.{callerMethod}";
  }

}
