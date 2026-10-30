using System.Runtime.CompilerServices;

namespace BLTools.Core;

/// <summary>
/// Variant B : non generic extension block on object?, only the runtime type is used
/// </summary>
public static class ObjectExtensionObject {

  public static TObjectDumpOptions DEFAULT_DUMP_OPTIONS { get; set; } = TObjectDumpOptions.Default;

  public const string VALUE_NULL = TObjectDumper.VALUE_NULL;
  public const string VALUE_UNKNOWN = TObjectDumper.VALUE_UNKNOWN;
  public const string VALUE_MORE = TObjectDumper.VALUE_MORE;

  extension(string source) {
    public string OrUnknown() => source ?? VALUE_UNKNOWN;
  }

  extension(object? source) {
    public string OrNull() => source?.ToString() ?? VALUE_NULL;

    public string Dump([CallerArgumentExpression(nameof(source))] string dataName = "") => source.Dump(DEFAULT_DUMP_OPTIONS, dataName);

    public string Dump(TObjectDumpOptions options, [CallerArgumentExpression(nameof(source))] string dataName = "") {
      return TObjectDumper.Dump(source, source?.GetType(), dataName, options);
    }
  }

}
