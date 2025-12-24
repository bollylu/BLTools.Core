using System;
using System.Collections;
using System.Runtime.CompilerServices;

using BLTools.Extension_methods.Reflection;

namespace BLTools.Core;

public static class ObjectExtension {

  public static SObjectDumpOptions DEFAULT_DUMP_OPTIONS { get; set; } = SObjectDumpOptions.Default;

  public const string VALUE_NULL = "(null)";
  public const string VALUE_UNKNOWN = "(unknown)";
  public const string VALUE_MORE = "{...}";

  public static string OrUnknown(this string? data) => data ?? VALUE_UNKNOWN;
  public static string OrNull(this object? data) => data?.ToString() ?? VALUE_NULL;

  public static string Dump<T>(this T data, [CallerArgumentExpression(nameof(data))] string? dataName = "") =>
    Dump(data, DEFAULT_DUMP_OPTIONS, dataName);

  public static string Dump<T>(this T data, SObjectDumpOptions options, [CallerArgumentExpression(nameof(data))] string? dataName = "") {

    if (data is null) {
      return $"{dataName.OrUnknown()} = {VALUE_NULL}";
    }

    if (options.MaxDepth <= 0) {
      return string.Empty;
    }

    try {

      Type DataClassType = typeof(T);

      if (!DataClassType.CanDump()) {
        return string.Empty;
      }

      switch (data) {

        case string StringValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {StringValue.WithQuotes()}";

        case char CharValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {CharValue.WithQuotes()}";

        case Enum EnumValue:
          TypeCode ValueTypeCode = EnumValue.GetTypeCode();
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {EnumValue.GetType().Name}.{EnumValue} ({ValueTypeCode}:{Convert.ChangeType(EnumValue, ValueTypeCode)})";

        case IDictionary DictItems: {
            StringBuilder Builder = new StringBuilder();
            if (options.MaxDepth > 1) {
              Builder.AppendLine($"{data.GetType().GetNameEx()} {dataName.OrUnknown()} [{DictItems.Count}] {{");
              foreach (DictionaryEntry Item in DictItems) {
                Builder.AppendIndent(Item.Key.Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }), 2);
                Builder.AppendIndent(Item.Value.Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }), 2);
              }
              Builder.Append('}');
            } else {
              Builder.AppendLine($"{data.GetType().GetNameEx()} {dataName.OrUnknown()} [{DictItems.Count}] {VALUE_MORE}");
            }
            return Builder.ToString();
          }

        case IEnumerable EnumerableItems: {
            StringBuilder Builder = new StringBuilder();

            if (options.MaxDepth > 1) {
              Builder.AppendLine($"{data.GetType().GetNameEx()} {dataName.OrUnknown()} [{((IEnumerable<object>)EnumerableItems).Count()}] {{");
              foreach (var Item in EnumerableItems) {
                Builder.AppendIndent(Item.Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }), 2);
              }
              Builder.Append('}');
            } else {
              Builder.AppendLine($"{data.GetType().GetNameEx()} {dataName.OrUnknown()} [{((IEnumerable<object>)EnumerableItems).Count()}] {VALUE_MORE}");
            }
            return Builder.ToString();
          }

        case float FloatValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {FloatValue.ToString(options.Culture.NumberFormat)}";

        case double DoubleValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {DoubleValue.ToString(options.Culture.NumberFormat)}";

        case decimal DecimalValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {DecimalValue.ToString(options.Culture.NumberFormat)}";

        case object Primitive when data.GetType().IsPrimitive: {
            return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {data}";
          }

        case Int128:
        case UInt128:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {data}";

        case DateTime DateTimeValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {DateTimeValue.ToString(options.Culture.DateTimeFormat)}";

        case TimeSpan TimeSpanValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {TimeSpanValue}";

        case DateOnly DateOnlyValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {DateOnlyValue.ToString(options.Culture.DateTimeFormat)}";

        case TimeOnly TimeOnlyValue:
          return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {TimeOnlyValue.ToString(options.Culture.DateTimeFormat)}";

        case object Class when DataClassType.IsClass || DataClassType.IsInterface || DataClassType.IsAbstract: {

            if (options.MaxDepth > 1) {
              StringBuilder Builder = new();

              if (options.WithTitle) {
                Builder.AppendLine($"{data.GetDisplayName(dataName)} {{");
              }

              Type LocalDataClassType = Class.GetType();

              if (options.DumpPrivateFields) {
                IEnumerable<FieldInfo> Fields = LocalDataClassType?
                  .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                  .Where(x => !x.Name.StartsWith('<'))
                  .Where(f => f.CanDump()) ?? [];
                foreach (var FieldInfoItem in Fields) {
                  Builder.AppendIndent(FieldInfoItem.GetValue(data).Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }, FieldInfoItem.Name), options.WithTitle ? 2 : 0);
                }
              }

              if (options.DumpPublicFields) {
                IEnumerable<FieldInfo> Fields = LocalDataClassType?
                  .GetFields(BindingFlags.Public | BindingFlags.Instance)
                  .Where(x => !x.Name.StartsWith('<'))
                  .Where(f => f.CanDump())
                  ?? [];
                foreach (var FieldInfoItem in Fields) {
                  Builder.AppendIndent(FieldInfoItem.GetValue(data).Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }, FieldInfoItem.Name), options.WithTitle ? 2 : 0);
                }
              }

              if (options.DumpPrivateProperties) {
                IEnumerable<PropertyInfo> Properties = LocalDataClassType?
                  .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                  .Where(p => p.CanDump())
                  ?? [];
                foreach (var PropertyInfoItem in Properties) {
                  Builder.AppendIndent(PropertyInfoItem.GetValue(data).Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }, PropertyInfoItem.Name), options.WithTitle ? 2 : 0);
                }
              }

              if (options.DumpPublicProperties) {
                IEnumerable<PropertyInfo> Properties = LocalDataClassType?
                  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                  .Where(p => p.CanDump())
                  ?? [];
                foreach (var PropertyInfoItem in Properties) {
                  string Text = PropertyInfoItem.GetValue(data).Dump(options with { MaxDepth = options.MaxDepth - 1, WithTitle = true }, PropertyInfoItem.Name);
                  Builder.AppendIndent(Text, options.WithTitle ? 2 : 0);
                }
              }

              if (options.WithTitle) {
                Builder.AppendLine("}");
              }

              return Builder.ToString();

            } else {
              return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {VALUE_MORE}";
            }
          }

        default: {
            return $"{data.GetType().GetNameEx()} {dataName.OrUnknown()} = {data}";
          }
      }

    } catch (Exception ex) {
      return $"Unable to dump {dataName?.WithQuotes().OrNull()} : {ex.Message}";
    }

  }

  public static string GetDisplayName<T>(this T source, string? sourceName) {

    if (source is null) {
      return $"{sourceName} => {source.OrNull()}";
    }

    Type SourceType = typeof(T);
    if (SourceType == typeof(object)) {
      SourceType = source.GetType();
    }

    StringBuilder RetVal = new(SourceType.GetNameEx());

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

    RetVal.Append($" {sourceName}");

    return RetVal.ToString();
  }
}
