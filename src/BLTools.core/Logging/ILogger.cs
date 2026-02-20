using System.Runtime.CompilerServices;

namespace BLTools.Core.Logging;

/// <summary>
/// Allows to log information
/// </summary>
public interface ILogger : IDisposable {

  const int NO_WIDTH = -1;
  const string VALUE_NULL = "(null)";

  /// <summary>
  /// Options of the logger
  /// </summary>
  ILoggerOptions Options { get; }

  /// <summary>
  /// The name of the logger
  /// </summary>
  string Name { get; }

  #region --- Log --------------------------------------------
  void Log<TData>(TData something, [CallerMemberName] string callerName = "", ESeverity severity = ESeverity.Info);
  void LogBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = NO_WIDTH, ESeverity severity = ESeverity.Info);
  #endregion --- Log -----------------------------------------

  #region --- LogVerbose --------------------------------------------
  void LogVerbose<TData>(TData something, [CallerMemberName] string callerName = "", ESeverity severity = ESeverity.Info);
  void LogVerboseBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = NO_WIDTH, ESeverity severity = ESeverity.Info);
  #endregion --- LogVerbose -----------------------------------------

  #region --- LogWarning --------------------------------------------
  void LogWarning<TData>(TData something, [CallerMemberName] string callerName = "");
  void LogWarningBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = NO_WIDTH);
  #endregion --- LogWarning -----------------------------------------

  #region --- LogError --------------------------------------------
  void LogError<TData>(TData something, [CallerMemberName] string callerName = "");
  void LogErrorBox<TData>(string title, TData something, ELogErrorOptions options = ELogErrorOptions.None, [CallerMemberName] string callerName = "", int width = NO_WIDTH);
  #endregion --- LogError -----------------------------------------

  #region --- LogDebug --------------------------------------------
  void LogDebug<TData>(TData something, [CallerMemberName] string callerName = "");
  void LogDebugBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = NO_WIDTH);
  #endregion --- LogDebug -----------------------------------------

  #region --- LogDebugEx --------------------------------------------
  void LogDebugEx<TData>(TData something, [CallerMemberName] string callerName = "");
  void LogDebugExBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = NO_WIDTH);
  #endregion --- LogDebugEx -----------------------------------------

  #region --- LogFatal --------------------------------------------
  void LogFatal<TData>(TData something, [CallerMemberName] string callerName = "");
  void LogFatalBox<TData>(string title, TData something, ELogErrorOptions options = ELogErrorOptions.None, [CallerMemberName] string callerName = "", int width = NO_WIDTH);
  #endregion --- LogFatal -----------------------------------------

  #region --- Special for tests --------------------------------------------
  void Message(string message, [CallerMemberName] string caller = "");
  void Ok(string additionalInfo = "", [CallerMemberName] string caller = "");
  void Failed(Exception ex, [CallerMemberName] string caller = "");
  void Failed(string additionalInfo = "", [CallerMemberName] string caller = "");

  void Dump<TData>(TData source, [CallerArgumentExpression(nameof(source))] string sourceName = "", [CallerMemberName] string caller = "") =>
    Dump(source, SObjectDumpOptions.Default, sourceName, caller);

  void Dump<TData>(TData source, SObjectDumpOptions options, [CallerArgumentExpression(nameof(source))] string sourceName = "", [CallerMemberName] string caller = "");
  void Dump<TData>(TData source, int maxDepth, [CallerArgumentExpression(nameof(source))] string sourceName = "", [CallerMemberName] string caller = "");

  void DumpBox<TData>(TData source, [CallerArgumentExpression(nameof(source))] string sourceName = "", [CallerMemberName] string caller = "") =>
    DumpBox(source, SObjectDumpOptions.Default, sourceName, caller);
  void DumpBox<TData>(TData source, SObjectDumpOptions options, [CallerArgumentExpression(nameof(source))] string sourceName = "", [CallerMemberName] string caller = "");
  void DumpBox<TData>(TData source, int maxDepth, [CallerArgumentExpression(nameof(source))] string sourceName = "", [CallerMemberName] string caller = "");
  #endregion --- Special for tests -----------------------------------------

  public ILogger CreateLoggerFor<TSource>() where TSource : class;
}

public interface ILogger<TSource> : ILogger where TSource : class {

}
