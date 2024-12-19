namespace BLTools.Core.Logging;

public class TTextWriterLogger : TTextWriterLogger<TConsoleLogger> { }

/// <summary>
/// Log to the console, set the source to the type of T
/// </summary>
/// <typeparam name="T">The type mentioned in source</typeparam>
public class TTextWriterLogger<TSource> : ALogger<TSource> where TSource : class {

  protected TextWriter _TextWriter = Console.Out;

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new TConsoleLogger of type T
  /// </summary>
  public TTextWriterLogger(TextWriter textWriter) : base(new TLoggerOptions() {
    IncludeSource = true,
    IncludeTimestamp = true
  }) {
    _TextWriter = textWriter;
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new TConsoleLogger of type T (default to Console.Out)
  /// </summary>
  public TTextWriterLogger() : base(new TLoggerOptions() {
    IncludeSource = true,
    IncludeTimestamp = true
  }) {
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new TConsoleLogger of type T
  /// </summary>
  public TTextWriterLogger(TextWriter textWriter, ILoggerOptions options) : base(options) {
    _TextWriter = textWriter;
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new TConsoleLogger of type T (default to Console.Out)
  /// </summary>
  public TTextWriterLogger(ILoggerOptions options) : base(options) {
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new TConsoleLogger of type T from another ILogger
  /// </summary>
  /// <param name="logger">The source ILogger</param>
  public TTextWriterLogger(ILogger logger) : base(logger) {
    Name = typeof(TSource).GetNameEx();
    if (logger is TTextWriterLogger textWriterLogger) {
      _TextWriter = textWriterLogger._TextWriter;
    }
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  protected override void _LogText(string text = "", string source = "", ESeverity severity = ESeverity.Info) {
    _TextWriter.Write(_BuildLogLine(text, source, severity));
  }
}
