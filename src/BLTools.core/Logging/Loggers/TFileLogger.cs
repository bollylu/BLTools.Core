namespace BLTools.Core.Logging;

/// <summary>
/// Log to the console, set the source to the type of T
/// </summary>
/// <typeparam name="T">The type mentioned in source</typeparam>
public class TFileLogger<TSource> : ALogger<TSource> where TSource : class {

  private const string CRLF = "\r\n";
  private const string CR = "\r";

  /// <summary>
  /// The full filename receiving the log
  /// </summary>
  public string Filename { get; }

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new TFileLogger of type T
  /// </summary>
  public TFileLogger(string filename) : base(TLoggerOptions.Default) {
    Filename = filename;
    Name = typeof(TSource).GetNameEx();
    Initialize();
  }

  /// <summary>
  /// A new TFileLogger of type T
  /// </summary>
  public TFileLogger(string filename, ILoggerOptions options) : base(options) {
    Filename = filename;
    Name = typeof(TSource).GetNameEx();
    Initialize();
  }

  /// <summary>
  /// A new TFileLogger of type T from another TConsoleLogger
  /// </summary>
  /// <param name="logger">The source TConsoleLogger</param>
  public TFileLogger(ILogger logger, string filename) : base(logger) {
    Filename = filename;
    Name = typeof(TSource).GetNameEx();
    Initialize();
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  #region --- Converters -------------------------------------------------------------------------------------
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder(base.ToString());
    RetVal.AppendLine($"{nameof(Filename)} = {Filename.WithQuotes()}");
    return RetVal.ToString();
  }
  #endregion --- Converters -------------------------------------------------------------------------------------

  protected bool IsInitialized = false;

  protected virtual void Initialize() {
    if (IsInitialized) {
      return;
    }

    if (string.IsNullOrWhiteSpace(Filename)) {
      throw new ArgumentException($"Unable to create {nameof(TFileLogger<TSource>)} : filename is null or empty");
    }

    try {
      string? DirectoryName = Path.GetDirectoryName(Filename);
      if (string.IsNullOrWhiteSpace(DirectoryName)) {
        return;
      }
      if (Directory.Exists(DirectoryName)) {
        return;
      }
      Directory.CreateDirectory(DirectoryName);
    } catch (Exception ex) {
      Debug.WriteLine($"Unable to create log directory for filename {Filename.WithQuotes()} : {ex.Message}");
      throw;
    }

    IsInitialized = true;
  }

  #region --- ALogger abstract methods --------------------------------------------
  protected override void LogText(string text = "", string source = "", ESeverity severity = ESeverity.Info) {
    #region === Validate parameters ===
    if (text.IsEmpty()) {
      return;
    }

    if (severity < Options.SeverityLimit) {
      return;
    }
    #endregion === Validate parameters ===

    lock (_Lock) {

      #region --- Ensure we can safely write --------------------------------------------
      DateTime StartTime = DateTime.Now;
      while (IsBusy && (DateTime.Now - StartTime).TotalMilliseconds < Options.Timeout) {
        Thread.Sleep(2);
      }
      if (IsBusy) {
        throw new TimeoutException($"Timeout writing to log");
      }
      #endregion --- Ensure we can safely write --------------------------------------------

      IsBusy = true;
      string ProcessedText = text.Replace(CRLF, CR);
      StringBuilder Builder = new StringBuilder();
      foreach (string TextItem in ProcessedText.Split(CR, StringSplitOptions.None)) {
        Builder.AppendLine(BuildLogLine(TextItem, source, severity));
      }
      try {
        File.AppendText(Builder.ToString());
      } catch (Exception ex) {
        Trace.WriteLine(ex.Message);
      }
      IsBusy = false;
    }
  }
  #endregion --- ALogger abstract methods -----------------------------------------


  #region --- Log maintenance --------------------------------------------
  /// <summary>
  /// Delete the current log file.
  /// A new log file will be created the first time a new log line is added
  /// </summary>
  public virtual void ResetLog() {
    lock (_Lock) {
      try {
        File.Delete(Filename);
      } catch (Exception ex) {
        Debug.WriteLine($"Unable to ResetLog for {Filename.WithQuotes()} : {ex.Message}");
      }
    }
  }

  /// <summary>
  /// Will close the current log file, and rename it as oldfilename + datetime + .log.
  /// A new log file will be created the first time a new log line is added
  /// </summary>
  public virtual string? Rollover() {
    return Rollover($"{Path.GetFileNameWithoutExtension(Filename)}+{DateTime.Now:yyyy-MM-dd HH-mm-ss}{Path.GetExtension(Filename)}");
  }

  /// <summary>
  /// Will close the current log file, and rename it as newName.
  /// A new log file will be created the first time a new log line is added
  /// </summary>
  /// <param name="newName">Name of the renamed (only the filename+extension part, not the path) version</param>
  /// <returns>The name of the new log file or null if error</returns>
  public virtual string? Rollover(string newName) {
    #region Validate parameters
    if (newName == null) {
      string Msg = $"Unable to rollover {Filename.WithQuotes()} because the new name is null";
      Trace.WriteLine(Msg, ESeverity.Fatal.ToString());
      throw new ArgumentNullException(nameof(newName), Msg);
    }
    #endregion Validate parameters

    lock (_Lock) {
      try {
        string ActualDirectory = Path.GetDirectoryName(Filename) ?? throw new ApplicationException($"Unable to get actual directory for {Filename.WithQuotes()} : something's wrong with the parameters");
        string Destination = Path.Combine(ActualDirectory, newName);
        File.Move(Filename, Destination);
        return Destination;
      } catch (Exception ex) {
        Debug.WriteLine($"Unable to Rollover for {Filename.WithQuotes()} to {newName.WithQuotes()} : {ex.Message}");
        return null;
      }
    }
  }
  #endregion --- Log maintenance --------------------------------------------

}
