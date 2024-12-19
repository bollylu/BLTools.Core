using System.Runtime.CompilerServices;

using BLTools.Core.Text;

namespace BLTools.Core.Diagnostic;

public partial class TMessageLogger {

  private const string VALUE_MISSING_TOSTRING = "Missing ToString()";
  private const string DUMP_METHOD = "Dump";

  public const int DEFAULT_DEBUG_BOX_WIDTH = 132;

  public static int DebugBoxWidth { get; set; } = DEFAULT_DEBUG_BOX_WIDTH;

  /// <summary>
  /// Display info about item
  /// </summary>
  /// <param name="source">The item to display info for</param>
  /// <param name="maxDepth"></param>
  /// <param name="sourceName">The name of the item</param>
  public void Dump<T>(T source, int maxDepth = 1, [CallerArgumentExpression(nameof(source))] string? sourceName = null) {

    if (source is null) {
      Logger.Log(TMessageLoggerSupport.VALUE_NULL.BoxFixedWidth(sourceName ?? "(unknown source)", DebugBoxWidth));
      return;
    }

    DumpWithMessage(MakeTitle(source, sourceName), source, maxDepth);
  }



  /// <summary>
  /// Display the object ToString() to the console in a box
  /// </summary>
  /// <param name="title">The title of the box</param>
  /// <param name="source">The object converted into a string</param>
  /// <param name="maxDepth"></param>
  public void DumpWithMessage<T>(string title, T source, int maxDepth = 1) {

    if (source is null) {
      Logger.Log(TMessageLoggerSupport.VALUE_NULL.BoxFixedWidth(title, DebugBoxWidth));
      return;
    }

    Logger.Log(BuildDumpContent(source, maxDepth, 0).BoxFixedWidth(title ?? "", DebugBoxWidth));

  }
}
