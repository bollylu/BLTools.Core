namespace BLTools.Core.Logging;

/// <summary>
/// A Trace logger
/// </summary>
public class TTraceLogger : TTraceLogger<TTraceLogger> { }

public class TTraceLogger<TSource> : ALogger<TSource> where TSource : class {

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new trace logger
  /// </summary>
  public TTraceLogger() : base(new TLoggerOptions() { 
    IncludeSource = true,
    IncludeTimestamp = true
  }) {
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// A new trace logger from another logger
  /// </summary>
  /// <param name="options"></param>
  public TTraceLogger(ILoggerOptions options) : base(options) {
    Name = typeof(TSource).GetNameEx();
  }

  /// <summary>
  /// Initializes a new instance of the TTraceLogger class using the specified logger for logging operations.
  /// </summary>
  /// <param name="logger">The logger instance used to record log messages and trace information.</param>
  public TTraceLogger(ILogger logger) : base(logger) {
    Name = typeof(TSource).GetNameEx();
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  /// <inheritdoc/>
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder(base.ToString());
    RetVal.AppendLine($"{nameof(Trace.Listeners)}");
    foreach (TraceListener ListenerItem in Trace.Listeners) {
      RetVal.AppendLine($" - {ListenerItem}");
    }
    return RetVal.ToString();
  }

  protected override void _LogText(string text = "", string source = "", ESeverity severity = ESeverity.Info) {
    Trace.WriteLine(_BuildLogLine(text, source, severity));
  }
}


