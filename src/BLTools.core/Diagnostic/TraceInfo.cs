using System.Runtime.CompilerServices;

namespace BLTools.Core.Diagnostic;

/// <summary>
/// Helper to display messages on console
/// </summary>
public static partial class TraceInfo {

  private const string VALUE_NULL = "(null)";
  private const string VALUE_MISSING_TOSTRING = "Missing ToString()";
  private const string MESSAGE_OK = "Ok";
  private const string MESSAGE_FAILED = "Failed";

  private const string DUMP_METHOD = "Dump";

  /// <summary>
  /// How to output value
  /// </summary>
  public static Action<string> Output { get; set; } = (value) => Console.WriteLine(value);

  #region --- Message --------------------------------------------
  /// <summary>
  /// Display the message to the console
  /// </summary>
  /// <param name="message">The message content</param>
  public static void Message(string message) {
    Output($"==> {message}");
  }

  /// <summary>
  /// Display an OK message
  /// </summary>
  /// <param name="additionalInfo">Some additional info</param>
  public static void Ok(string additionalInfo = "") {
    if (additionalInfo == "") {
      Message(MESSAGE_OK);
    } else {
      Message($"{MESSAGE_OK} : {additionalInfo}");
    }
  }

  /// <summary>
  /// Display an Failed message
  /// </summary>
  /// <param name="additionalInfo">Some additional info</param>
  public static void Failed(string additionalInfo = "") {
    if (additionalInfo == "") {
      Message(MESSAGE_FAILED);
    } else {
      Message($"{MESSAGE_FAILED} : {additionalInfo}");
    }
  }

  /// <summary>
  /// Display the message to the console in a box
  /// </summary>
  /// <param name="title">The title of the box</param>
  /// <param name="message">The message content</param>
  public static void MessageBox(string? title, string? message) {
    message ??= "";
    Output(message.BoxFixedWidth(title ?? "", GlobalSettings.DEBUG_BOX_WIDTH));
  }
  #endregion --- Message --------------------------------------------

  /// <summary>
  /// Display info about item
  /// </summary>
  /// <param name="source">The item to display info for</param>
  /// <param name="maxDepth"></param>
  /// <param name="sourceName">The name of the item</param>
  public static void Dump<T>(T source, int maxDepth = 1, [CallerArgumentExpression(nameof(source))] string? sourceName = null) {

    if (source is null) {
      Output(VALUE_NULL.BoxFixedWidth(sourceName ?? "(unknown source)", GlobalSettings.DEBUG_BOX_WIDTH));
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
  public static void DumpWithMessage<T>(string title, T source, int maxDepth = 1) {

    if (source is null) {
      Output(VALUE_NULL.BoxFixedWidth(title, GlobalSettings.DEBUG_BOX_WIDTH));
      return;
    }

    Output(BuildDumpContent(source, maxDepth, 0).BoxFixedWidth(title ?? "", GlobalSettings.DEBUG_BOX_WIDTH));

  }








}
