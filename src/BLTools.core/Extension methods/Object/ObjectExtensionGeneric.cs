using System.Collections;
using System.Runtime.CompilerServices;

namespace BLTools.Core;

/// <summary>
/// Variant A : generic extension block, the static type T is kept (displayed as "IFoo : TFoo")
/// </summary>
public static class ObjectExtensionGeneric {

  public static TObjectDumpOptions DEFAULT_DUMP_OPTIONS { get; set; } = TObjectDumpOptions.Default;

  public const string VALUE_NULL = TObjectDumper.VALUE_NULL;
  public const string VALUE_UNKNOWN = TObjectDumper.VALUE_UNKNOWN;
  public const string VALUE_MORE = TObjectDumper.VALUE_MORE;

  extension(string source) {
    public string OrUnknown() => source ?? VALUE_UNKNOWN;
  }

  extension(object? source) {
    public string OrNull() => source?.ToString() ?? VALUE_NULL;
  }

  extension<T>(T source) {
    public string Dump([CallerArgumentExpression(nameof(source))] string dataName = "") => source.Dump(DEFAULT_DUMP_OPTIONS, dataName);

    public string Dump(TObjectDumpOptions options, [CallerArgumentExpression(nameof(source))] string dataName = "") {
      return TObjectDumper.Dump(source, typeof(T), dataName, options);
    }

    public string GetDisplayName(string? sourceName) {

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

      } else if (source is IDictionary DictionaryItems) {
        RetVal.Append($" ({DictionaryItems.Count})");

      } else if (source is IEnumerable EnumerableItems) {
        RetVal.Append($" ({EnumerableItems.Cast<object>().Count()})");

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

}
