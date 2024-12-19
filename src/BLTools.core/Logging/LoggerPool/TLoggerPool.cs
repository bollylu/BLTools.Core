namespace BLTools.Core.Logging;

/// <summary>
/// A pool of logger
/// </summary>
public class TLoggerPool {

  /// <summary>
  /// The default logger name (when nothing is provided)
  /// </summary>
  public const string DEFAULT_LOGGER_NAME = "(default)";

  /// <summary>
  /// A logger by default
  /// </summary>
  protected ILogger DefaultLogger { get; set; } = new TConsoleLogger();

  /// <summary>
  /// The type of the default logger
  /// </summary>
  protected Type DefaultLoggerType => DefaultLogger.GetType();


  /// <summary>
  /// Internal list of loggers
  /// </summary>
  protected readonly Dictionary<string, ILogger> _Items = new();
  /// <summary>
  /// Locks the loggers
  /// </summary>
  protected readonly object _Lock = new object();

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new logger pool
  /// </summary>
  public TLoggerPool() { }

  /// <summary>
  /// A new logger pool from a list of logger
  /// </summary>
  /// <param name="Items"></param>
  public TLoggerPool(IEnumerable<ILogger> Items) {
    foreach (ILogger LoggerItem in Items) {
      _Items.Add(LoggerItem.Name, LoggerItem);
    }
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  /// <inheritdoc/>
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"- {nameof(DefaultLogger)} = {DefaultLogger.GetType().GetNameEx()}");
    RetVal.AppendLine($"- {_Items.Count} logger in pool");
    if (_Items.Any()) {
      foreach (var LoggerItem in _Items) {
        RetVal.AppendIndent($"- {LoggerItem.Key} = {LoggerItem.Value.GetType().GetNameEx()}", 2);
      }
    }
    return RetVal.ToString();
  }

  /// <summary>
  /// Add a logger to the pool
  /// </summary>
  /// <param name="logger">Ther logger to add</param>
  /// <exception cref="ApplicationException"></exception>
  public virtual void AddLogger(ILogger logger) {
    lock (_Lock) {

      //if (logger.Name.IsEmpty()) {
      //  logger.Name = logger.GetType().GetNameEx();
      //}

      if (_Items.Keys.Contains(logger.Name)) {
        throw new TDuplicateLoggerException($"Attempt to create two loggers with the same name : {logger.Name}");
      }

      _Items.Add(logger.Name, logger);
    }
  }

  /// <summary>
  /// Add a logger as the default logger
  /// </summary>
  /// <param name="logger"></param>
  /// <exception cref="ApplicationException"></exception>
  public virtual void AddDefaultLogger(ILogger logger) {
    //logger.Name = DEFAULT_LOGGER_NAME;
    if (_Items.Keys.Contains(DEFAULT_LOGGER_NAME)) {
      throw new ApplicationException($"Attempt to create two default loggers : {logger.Name}");
    }
    _Items.Add(DEFAULT_LOGGER_NAME, logger);
  }

  /// <summary>
  /// Retrieve a logger of type ILogger&lt;T&gt;
  /// </summary>
  /// <typeparam name="T">The type of logger to get</typeparam>
  /// <returns>A logger of type ILogger&lt;T&gt; or the DefaultLogger</returns>
  /// <exception cref="ApplicationException"></exception>
  public virtual ILogger GetLogger<T>() where T : class {
    lock (_Lock) {

      if (_Items.IsEmpty()) {
        return _MakeLogger<T>(DefaultLogger);
      }

      try {
        return _Items[typeof(T).GetNameEx()];
      } catch {
        return _MakeLogger<T>(DefaultLogger);
      }

    }
  }

  /// <summary>
  /// Clear the pool
  /// </summary>
  public void Clear() {
    lock (_Lock) {
      _Items.Clear();
    }
  }

  /// <summary>
  /// Set a new default logger
  /// </summary>
  /// <param name="logger">The new default logger</param>
  public void SetDefaultLogger(ILogger logger) {
    lock (_Lock) {
      _Items.Remove(DEFAULT_LOGGER_NAME);
      _Items.Add(DEFAULT_LOGGER_NAME, logger);
    }
  }

  /// <summary>
  /// Get the default logger
  /// </summary>
  public virtual ILogger GetDefaultLogger() {
    lock (_Lock) {
      if (_Items.IsEmpty()) {
        throw new ApplicationException("Unable to get the default logger : default logger is missing");
      }

      try {
        return _Items[DEFAULT_LOGGER_NAME];
      } catch {
        throw new ApplicationException("Unable to get the default logger : default logger is missing");
      }
    }
  }

  /// <summary>
  /// Duplicate a logger
  /// </summary>
  /// <typeparam name="T">The type associated with the logger</typeparam>
  /// <param name="source">The logger to duplicate</param>
  /// <returns>A new logger</returns>
  /// <exception cref="ApplicationException"></exception>
  protected virtual ILogger _MakeLogger<T>(ILogger source) where T : class {
    return source switch {
      TFileLogger<T> FileLogger => new TFileLogger<T>(FileLogger, FileLogger.Filename),
      TConsoleLogger<T> ConsoleLogger => new TConsoleLogger<T>(ConsoleLogger),
      _ => throw new ApplicationException($"Invalid logger<T> type : {source?.GetType().GetNameEx() ?? "(unknown)"}")
    };
  }
}
