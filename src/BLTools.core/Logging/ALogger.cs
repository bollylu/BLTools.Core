using System.Collections;
using System.Runtime.CompilerServices;

namespace BLTools.Core.Logging;

public abstract class ALogger<TSource> : ILogger, ILogger<TSource> where TSource : class {

  private const string DUMP_METHOD = "Dump";

  public string Name { get; init; } = string.Empty;

  public ILoggerOptions Options { get; private set; } = new TLoggerOptions();

  /// <summary>
  /// Is the logger busy ?
  /// </summary>
  [DoNotDump]
  protected bool IsBusy = false;

  /// <summary>
  /// Locking mechanism
  /// </summary>
  [DoNotDump]
  protected readonly Lock _Lock = new();

  #region --- Caller --------------------------------------------
  /// <summary>
  /// The type name of the caller
  /// </summary>
  [DoNotDump]
  public string CallerTypeName {
    get {
      if (_CallerTypeName is null) {
        if (typeof(TSource).IsGenericType) {
          _CallerTypeName = typeof(TSource).GetGenericArguments()[0].Name;
        } else {
          _CallerTypeName = typeof(TSource).GetNameEx();
        }
      }
      return _CallerTypeName;
    }
  }
  [DoNotDump]
  private string? _CallerTypeName;

  private string Caller(string caller) => $"{CallerTypeName}.{caller}";
  #endregion --- Caller -----------------------------------------

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  protected ALogger() { }
  protected ALogger(ILoggerOptions options) {
    Options = options;
  }
  protected ALogger(ILogger logger) {
    Name = logger.Name;
    Options = logger.Options;
  }

  private bool disposedValue;

  protected virtual void Dispose(bool disposing) {
    if (!disposedValue) {
      if (disposing) {
      }

      // TODO: free unmanaged resources (unmanaged objects) and override finalizer
      // TODO: set large fields to null
      disposedValue = true;
    }
  }

  // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
  // ~ALogger()
  // {
  //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
  //     Dispose(disposing: false);
  // }

  public void Dispose() {
    // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    Dispose(disposing: true);
    GC.SuppressFinalize(this);
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"{nameof(Name)} = {Name.WithQuotes()}");
    RetVal.AppendLine($"{nameof(Options)} = {{");
    RetVal.AppendIndent(Options?.ToString() ?? ILogger.VALUE_NULL, 2);
    RetVal.AppendLine("}");
    return RetVal.ToString();
  }

  #region --- Internal processing --------------------------------------------
  /// <summary>
  /// Build a line of log using components
  /// </summary>
  /// <param name="text">The text to log</param>
  /// <param name="source">The source of the log</param>
  /// <param name="severity">The severity</param>
  /// <returns></returns>
  protected virtual string BuildLogLine(string text, string source = "", ESeverity severity = ESeverity.Info) {

    string CleanedText = text.Replace("\r\n", "\n").Trim('\r', '\n', ' ', '\t');
    StringBuilder Builder = new StringBuilder();

    foreach (string LineItem in CleanedText.Split('\n')) {
      if (Options.IncludeTimestamp) {
        Builder.Append(DateTime.Now.ToString(Options.TimestampFormat)).Append('|');
      }

      if (Options.IncludeSeverity) {
        Builder.Append(severity.ToString().AlignedLeft(10, '.')).Append('|');
      }

      if (Options.IncludeSource) {
        Builder.Append(source.AlignedLeft(Options.SourceWidth, '.')).Append('|');
      }

      Builder.AppendLine(LineItem);
    }

    return Builder.ToString();
  }

  /// <summary>
  /// Log a text
  /// </summary>
  /// <param name="text">The text to log</param>
  /// <param name="source">The source of the text</param>
  /// <param name="severity">The severity of the log</param>
  /// <exception cref="TimeoutException"></exception>
  protected abstract void LogText(string text = "", string source = "", ESeverity severity = ESeverity.Info);

  protected int GetWidth(int width) => width <= 0 ? Options.BoxWidth : width;
  #endregion --- Internal processing --------------------------------------------

  #region --- Log --------------------------------------------
  public void Log<TData>(TData something, [CallerMemberName] string callerName = "", ESeverity severity = ESeverity.Info) {
    switch (something) {
      case string SomeString:
        LogText(SomeString ?? ILogger.VALUE_NULL, Caller(callerName), severity);
        break;
      default:
        LogText(something?.ToString() ?? ILogger.VALUE_NULL, Caller(callerName), severity);
        break;
    }
  }

  public void LogBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH, ESeverity severity = ESeverity.Info) {
    switch (something) {
      case string SomeString:
        LogText(SomeString.BoxFixedWidth(title, GetWidth(width)), Caller(callerName), severity);
        break;
      case IDictionary SomeDictionary:
        LogText(SomeDictionary.ListDictionaryItems().BoxFixedWidth(title, GetWidth(width)), Caller(callerName), severity);
        break;
      case IEnumerable SomeEnumerable:
        LogText(SomeEnumerable.CombineToString(Environment.NewLine).BoxFixedWidth(title, GetWidth(width)), Caller(callerName), severity);
        break;
      default:
        LogText(something?.ToString() ?? ILogger.VALUE_NULL.BoxFixedWidth(title, GetWidth(width)), Caller(callerName), severity);
        break;
    }
  }
  #endregion --- Log -----------------------------------------

  #region --- LogVerbose --------------------------------------------
  public void LogVerbose<TData>(TData something, [CallerMemberName] string callerName = "", ESeverity severity = ESeverity.Info) {
    if (Options.IsVerbose) {
      Log(something, callerName, severity);
    }
  }

  public void LogVerboseBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH, ESeverity severity = ESeverity.Info) {
    if (Options.IsVerbose) {
      LogBox(title, something, callerName, width, severity);
    }
  }
  #endregion --- LogVerbose --------------------------------------------

  #region --- Log warning --------------------------------------------
  public void LogWarning<TData>(TData something, [CallerMemberName] string callerName = "") =>
    Log(something, callerName, ESeverity.Warning);

  public void LogWarningBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH) =>
    LogBox(title, something, callerName, width, ESeverity.Warning);
  #endregion --- Log warning --------------------------------------------

  #region --- Log debug --------------------------------------------
  public void LogDebug<TData>(TData something, [CallerMemberName] string callerName = "") =>
    Log(something, callerName, ESeverity.Debug);

  public void LogDebugBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH) =>
    LogBox(title, something, callerName, width, ESeverity.Debug);
  #endregion --- Log debug --------------------------------------------

  #region --- Log DebugEx --------------------------------------------
  public void LogDebugEx<TData>(TData something, [CallerMemberName] string callerName = "") =>
     Log(something, callerName, ESeverity.DebugEx);

  public void LogDebugExBox<TData>(string title, TData something, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH) =>
    LogBox(title, something, callerName, width, ESeverity.DebugEx);
  #endregion --- Log debug --------------------------------------------

  #region --- Log error --------------------------------------------
  public void LogError<TData>(TData something, [CallerMemberName] string callerName = "") =>
    Log(something, callerName, ESeverity.Error);

  public void LogErrorBox<TData>(string title, TData something, ELogErrorOptions options = ELogErrorOptions.None, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH) {
    switch (something) {
      case Exception ex:
        bool WithStackTrace = options.HasFlag(ELogErrorOptions.WithStackTrace);
        bool WithInnerExceptionMessage = options.HasFlag(ELogErrorOptions.WithInnerException);
        LogBox(title, ex.GetExceptionInfo(WithStackTrace, WithInnerExceptionMessage), callerName, width, ESeverity.Error);
        break;
      default:
        LogBox(title, something, callerName, width, ESeverity.Error);
        break;
    }
  }

  #endregion --- Log warning --------------------------------------------

  #region --- Log fatal --------------------------------------------
  public void LogFatal<TData>(TData something, [CallerMemberName] string callerName = "") =>
    Log(something, callerName, ESeverity.Fatal);

  public void LogFatalBox<TData>(string title, TData something, ELogErrorOptions options = ELogErrorOptions.None, [CallerMemberName] string callerName = "", int width = ILogger.NO_WIDTH) {
    switch (something) {
      case Exception ex:
        bool WithStackTrace = options.HasFlag(ELogErrorOptions.WithStackTrace);
        bool WithInnerExceptionMessage = options.HasFlag(ELogErrorOptions.WithInnerException);
        LogBox(title, ex.GetExceptionInfo(WithStackTrace, WithInnerExceptionMessage), callerName, width, ESeverity.Fatal);
        break;
      default:
        LogBox(title, something, callerName, width, ESeverity.Fatal);
        break;
    }
  }
  #endregion --- Log fatal --------------------------------------------


  #region --- Special for tests --------------------------------------------
  protected const string MESSAGE_OK = "### Ok ###";
  protected const string MESSAGE_FAILED = "--- Failed ---";

  public virtual void Message(string message, [CallerMemberName] string caller = "") {
    Log($"==> {message}", caller);
  }

  public virtual void Ok(string additionalInfo = "", [CallerMemberName] string caller = "") {
    Log(MESSAGE_OK, caller);
  }

  public virtual void Failed(Exception ex, [CallerMemberName] string caller = "") {
    LogErrorBox(MESSAGE_FAILED, ex, ELogErrorOptions.WithStackTrace, caller);
  }

  public virtual void Failed(string additionalInfo = "", [CallerMemberName] string caller = "") {
    LogError($"{MESSAGE_FAILED} : {additionalInfo}", caller);
  }

  public virtual void Dump<TData>(TData data, SObjectDumpOptions options, [CallerArgumentExpression(nameof(data))] string sourceName = "", [CallerMemberName] string caller = "") {
    Log(data.Dump(options, sourceName), caller);
  }

  public virtual void Dump<TData>(TData data, int maxDepth, [CallerArgumentExpression(nameof(data))] string sourceName = "", [CallerMemberName] string caller = "") {
    Log(data.Dump(new SObjectDumpOptions() { MaxDepth = maxDepth}, sourceName), caller);
  }

  public virtual void DumpBox<TData>(TData data, SObjectDumpOptions options, [CallerArgumentExpression(nameof(data))] string sourceName = "", [CallerMemberName] string caller = "") {
    TTextBox TextBox = new TTextBox() {
      Title = data.GetDisplayName(sourceName),
      Content = data.Dump(options, sourceName)
    };

    Log(TextBox.Render(), caller);
  }

  public virtual void DumpBox<TData>(TData data, int maxDepth, [CallerArgumentExpression(nameof(data))] string sourceName = "", [CallerMemberName] string caller = "") {
    TTextBox TextBox = new TTextBox() {
      Title = data.GetDisplayName(sourceName),
      Content = data.Dump(new SObjectDumpOptions() { MaxDepth = maxDepth}, sourceName)
    };

    Log(TextBox.Render(), caller);
  }


  #endregion --- Special for tests -----------------------------------------

  public abstract ILogger CreateLoggerFor<TNewSource>() where TNewSource : class;
}
