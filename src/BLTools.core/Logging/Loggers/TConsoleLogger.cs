namespace BLTools.Core.Logging;

public class TConsoleLogger : TConsoleLogger<TConsoleLogger> { }

/// <summary>
/// Log to the console, set the source to the type of T
/// </summary>
/// <typeparam name="T">The type mentioned in source</typeparam>
public class TConsoleLogger<TSource> : ALogger<TSource> where TSource : class {

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new TConsoleLogger of type T
  /// </summary>
  public TConsoleLogger() : base(new TLoggerOptions() {
    IncludeSource = true,
    IncludeTimestamp = true
  }) {
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new TConsoleLogger of type T
  /// </summary>
  public TConsoleLogger(ILoggerOptions options) : base(options) {
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new TConsoleLogger of type T from another ILogger
  /// </summary>
  /// <param name="logger">The source ILogger</param>
  public TConsoleLogger(ILogger logger) : base(logger) {
    Name = typeof(TSource).GetNameEx();
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  protected override void LogText(string text = "", string source = "", ESeverity severity = ESeverity.Info) {
    Console.Write(BuildLogLine(text, source, severity));
  }
}
