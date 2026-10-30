using System.Collections;
using System.Net;
using System.Reflection;

using BLTools.Extension_methods.Reflection;

namespace BLTools.Core;

/// <summary>
/// Reflection based dump engine, used by the Dump extension methods.
/// Dispatch is done on the runtime type, with cycle detection and per-member error handling.
/// </summary>
internal static class TObjectDumper {

  public const string VALUE_NULL = "(null)";
  public const string VALUE_UNKNOWN = "(unknown)";
  public const string VALUE_MORE = "{...}";
  public const string VALUE_CIRCULAR = "(circular reference)";

  /// <summary>
  /// Dumps source. declaredType (static type) is optional and only used to display "IFoo : TFoo".
  /// </summary>
  public static string Dump(object? source, Type? declaredType, string dataName, TObjectDumpOptions options) {
    StringBuilder Builder = new StringBuilder();
    HashSet<object> Visited = new HashSet<object>(ReferenceEqualityComparer.Instance);
    try {
      DumpValue(Builder, source, declaredType, dataName, options, 0, 0, Visited);
    } catch (Exception ex) {
      Builder.AppendLine($"(unable to dump {dataName}: {ex.GetType().Name} - {ex.Message})");
    }
    return Builder.ToString().TrimEnd('\r', '\n');
  }

  #region --- Core --------------------------------------------
  private static void DumpValue(StringBuilder builder, object? value, Type? declaredType, string name, TObjectDumpOptions options, int level, int indent, HashSet<object> visited) {
    string Pad = new string(' ', indent);

    if (value is null) {
      builder.AppendLine($"{Pad}{name} = {VALUE_NULL}");
      return;
    }

    Type ValueType = value.GetType();
    string Header = $"{Pad}{GetTypeLabel(ValueType, declaredType)} {name}";

    if (TryFormatScalar(value, ValueType, options, out string Text)) {
      builder.AppendLine($"{Header} = {Text}");
      return;
    }

    if (!ValueType.CanDump()) {
      return;
    }

    if (ValueType.IsValueType) {
      builder.AppendLine($"{Header} = {value}");
      return;
    }

    if (level >= options.MaxDepth) {
      builder.AppendLine($"{Header} = {VALUE_MORE}");
      return;
    }

    // Path-based detection: the same instance referenced twice (not nested) is fine
    if (!visited.Add(value)) {
      builder.AppendLine($"{Header} = {VALUE_CIRCULAR}");
      return;
    }

    try {
      switch (value) {
        case IDictionary DictionaryValue:
          DumpDictionary(builder, DictionaryValue, Header, options, level, indent, visited);
          break;

        case IEnumerable EnumerableValue:
          DumpEnumerable(builder, EnumerableValue, Header, options, level, indent, visited);
          break;

        default:
          DumpObject(builder, value, Header, options, level, indent, visited);
          break;
      }
    } finally {
      visited.Remove(value);
    }
  }

  private static string GetTypeLabel(Type valueType, Type? declaredType) {
    if (declaredType is not null && declaredType != valueType && (declaredType.IsInterface || declaredType.IsAbstract)) {
      return $"{declaredType.GetNameEx()} : {valueType.GetNameEx()}";
    }
    return valueType.GetNameEx();
  }

  private static bool TryFormatScalar(object value, Type valueType, TObjectDumpOptions options, out string text) {
    switch (value) {
      case string StringValue:
        text = StringValue.WithQuotes();
        return true;

      case char CharValue:
        text = CharValue.WithQuotes();
        return true;

      case Enum EnumValue:
        TypeCode Code = EnumValue.GetTypeCode();
        text = $"{valueType.Name}.{EnumValue} ({Code}:{Convert.ChangeType(EnumValue, Code)})";
        return true;

      case float FloatValue:
        text = FloatValue.ToString(options.Culture.NumberFormat);
        return true;

      case double DoubleValue:
        text = DoubleValue.ToString(options.Culture.NumberFormat);
        return true;

      case decimal DecimalValue:
        text = DecimalValue.ToString(options.Culture.NumberFormat);
        return true;

      case DateTime DateTimeValue:
        text = DateTimeValue.ToString(options.Culture.DateTimeFormat);
        return true;

      case DateOnly DateOnlyValue:
        text = DateOnlyValue.ToString(options.Culture.DateTimeFormat);
        return true;

      case TimeOnly TimeOnlyValue:
        text = TimeOnlyValue.ToString(options.Culture.DateTimeFormat);
        return true;

      case CultureInfo CultureValue:
        text = string.IsNullOrEmpty(CultureValue.Name) ? "Invariant" : CultureValue.Name;
        return true;

      case Type TypeValue:
        text = TypeValue.GetNameEx();
        return true;

      case TimeSpan or DateTimeOffset or Guid or Uri or IPAddress or Int128 or UInt128:
        text = value.ToString() ?? VALUE_NULL;
        return true;

      default:
        text = value.ToString() ?? VALUE_NULL;
        return valueType.IsPrimitive;
    }
  }
  #endregion --- Core --------------------------------------------

  #region --- Containers --------------------------------------------
  private static void DumpDictionary(StringBuilder builder, IDictionary dictionary, string header, TObjectDumpOptions options, int level, int indent, HashSet<object> visited) {
    builder.AppendLine($"{header} [{dictionary.Count}] {{");
    foreach (DictionaryEntry EntryItem in dictionary) {
      DumpValue(builder, EntryItem.Key, null, "Key", options, level + 1, indent + 2, visited);
      DumpValue(builder, EntryItem.Value, null, "Value", options, level + 1, indent + 2, visited);
    }
    builder.AppendLine($"{new string(' ', indent)}}}");
  }

  private static void DumpEnumerable(StringBuilder builder, IEnumerable items, string header, TObjectDumpOptions options, int level, int indent, HashSet<object> visited) {
    // Enumerated once only
    List<object?> Items = [.. items.Cast<object?>()];
    builder.AppendLine($"{header} [{Items.Count}] {{");
    foreach (object? ItemValue in Items) {
      DumpValue(builder, ItemValue, null, "Item", options, level + 1, indent + 2, visited);
    }
    builder.AppendLine($"{new string(' ', indent)}}}");
  }
  #endregion --- Containers --------------------------------------------

  #region --- Objects (fields and properties) --------------------------------------------
  private static void DumpObject(StringBuilder builder, object source, string header, TObjectDumpOptions options, int level, int indent, HashSet<object> visited) {
    Type SourceType = source.GetType();
    bool WithHeader = level > 0 || options.WithTitle;
    int MemberIndent = WithHeader ? indent + 2 : indent;

    if (WithHeader) {
      builder.AppendLine($"{header} {{");
    }

    if (options.DumpPrivateFields) {
      DumpFields(builder, source, SourceType, BindingFlags.NonPublic | BindingFlags.Instance, options, level, MemberIndent, visited);
    }
    if (options.DumpPublicFields) {
      DumpFields(builder, source, SourceType, BindingFlags.Public | BindingFlags.Instance, options, level, MemberIndent, visited);
    }
    if (options.DumpPrivateProperties) {
      DumpProperties(builder, source, SourceType, BindingFlags.NonPublic | BindingFlags.Instance, options, level, MemberIndent, visited);
    }
    if (options.DumpPublicProperties) {
      DumpProperties(builder, source, SourceType, BindingFlags.Public | BindingFlags.Instance, options, level, MemberIndent, visited);
    }

    if (WithHeader) {
      builder.AppendLine($"{new string(' ', indent)}}}");
    }
  }

  private static void DumpFields(StringBuilder builder, object source, Type sourceType, BindingFlags flags, TObjectDumpOptions options, int level, int indent, HashSet<object> visited) {
    foreach (FieldInfo FieldItem in sourceType.GetFields(flags)) {
      // Skip compiler generated backing fields
      if (FieldItem.Name.StartsWith('<') || !FieldItem.CanDump()) {
        continue;
      }

      object? Child = ReadMember(() => FieldItem.GetValue(source), out string? Error);
      if (Error is not null) {
        builder.AppendLine($"{new string(' ', indent)}{FieldItem.Name} = (error: {Error})");
        continue;
      }
      DumpValue(builder, Child, FieldItem.FieldType, FieldItem.Name, options, level + 1, indent, visited);
    }
  }

  private static void DumpProperties(StringBuilder builder, object source, Type sourceType, BindingFlags flags, TObjectDumpOptions options, int level, int indent, HashSet<object> visited) {
    foreach (PropertyInfo PropertyItem in sourceType.GetProperties(flags)) {
      // Indexers (this[...]) need arguments: they cannot be read without them
      if (!PropertyItem.CanRead || PropertyItem.GetIndexParameters().Length > 0 || !PropertyItem.CanDump()) {
        continue;
      }

      object? Child = ReadMember(() => PropertyItem.GetValue(source), out string? Error);
      if (Error is not null) {
        builder.AppendLine($"{new string(' ', indent)}{PropertyItem.Name} = (error: {Error})");
        continue;
      }
      DumpValue(builder, Child, PropertyItem.PropertyType, PropertyItem.Name, options, level + 1, indent, visited);
    }
  }

  /// <summary>
  /// A failing getter must not kill the whole dump
  /// </summary>
  private static object? ReadMember(Func<object?> reader, out string? error) {
    try {
      error = null;
      return reader();
    } catch (Exception ex) {
      error = (ex.InnerException ?? ex).GetType().Name;
      return null;
    }
  }
  #endregion --- Objects (fields and properties) --------------------------------------------

}
