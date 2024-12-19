//namespace BLTools.Core.Logging;

///// <summary>
///// A Trace logger
///// </summary>
//public class TTraceLogger : ALogger {

//  #region --- Constructor(s) ---------------------------------------------------------------------------------
//  /// <summary>
//  /// A new trace logger
//  /// </summary>
//  public TTraceLogger() {
//    Name = GetType().GetNameEx();
//    _Initialize();
//  }

//  /// <summary>
//  /// A new trace logger from another logger
//  /// </summary>
//  /// <param name="logger"></param>
//  public TTraceLogger(ILogger logger) {
//    Name = logger.GetType().GetNameEx();
//    _Initialize();
//  }

//  /// <inheritdoc/>
//  protected override void _Initialize() {
//    if (_IsInitialized) {
//      return;
//    }

//    _IsInitialized = true;
//  }
//  #endregion --- Constructor(s) ------------------------------------------------------------------------------

//  /// <inheritdoc/>
//  public override string ToString() {
//    StringBuilder RetVal = new StringBuilder(base.ToString());
//    RetVal.AppendLine($"{nameof(Trace.Listeners)}");
//    foreach (TraceListener ListenerItem in Trace.Listeners) {
//      RetVal.AppendLine($" - {ListenerItem}");
//    }
//    return RetVal.ToString();
//  }

//  protected override void _LogText(string text = "", string source = "", ESeverity severity = ESeverity.Info) {
//    Trace.WriteLine(_BuildLogLine(text, source, severity));
//  }
//}

///// <summary>
///// Log to the trace listeners, set the source to the type of T
///// </summary>
///// <typeparam name="T">The type mentioned in source</typeparam>
//public class TTraceLogger<T> : TTraceLogger where T : class {

//  #region --- Constructor(s) ---------------------------------------------------------------------------------
//  /// <summary>
//  /// A new TTraceLogger of type T
//  /// </summary>
//  public TTraceLogger() : base() {
//    Name = typeof(T).Name;
//  }

//  /// <summary>
//  /// A new TTraceLogger of type T from another TConsoleLogger
//  /// </summary>
//  /// <param name="logger">The source TConsoleLogger</param>
//  public TTraceLogger(TTraceLogger logger) : base(logger) {
//    Name = typeof(T).Name;
//  }
//  #endregion --- Constructor(s) ------------------------------------------------------------------------------


//}
