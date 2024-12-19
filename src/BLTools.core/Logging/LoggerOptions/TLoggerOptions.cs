namespace BLTools.Core.Logging;

public class TLoggerOptions : IDisposable, ILoggerOptions {

  public const int DEFAULT_TIMEOUT = 1000;
  public const ESeverity DEFAULT_SEVERITY_LIMIT = ESeverity.Info;
  public const int DEFAULT_SOURCE_WIDTH = 35;
  public const string DEFAULT_TIMESTAMP_FORMAT = "yyyy-MM-dd HH:mm:ss:fff";
  public const int DEFAULT_BOX_WIDTH = 132;

  #region --- Timeout --------------------------------------------
  public static int GlobalTimeout { get; set; } = DEFAULT_TIMEOUT;

  public int Timeout { get; set; } = GlobalTimeout;
  #endregion --- Timeout -----------------------------------------

  #region --- Severity --------------------------------------------
  public bool IncludeSeverity { get; set; } = true;

  public static ESeverity GlobalSeverityLimit {
    get {
      if (_GlobalSeverityLimit == ESeverity.Unknown) {
        return DEFAULT_SEVERITY_LIMIT;
      }
      return _GlobalSeverityLimit;
    }
    set {
      _GlobalSeverityLimit = value;
    }
  }
  [DoNotDump]
  private static ESeverity _GlobalSeverityLimit = ESeverity.Unknown;

  public ESeverity SeverityLimit {
    get {
      if (_SeverityLimit == ESeverity.Unknown) {
        return GlobalSeverityLimit;
      }
      return _SeverityLimit;
    }
    set {
      _SeverityLimit = value;
    }
  }
  [DoNotDump]
  private ESeverity _SeverityLimit = GlobalSeverityLimit;
  #endregion --- Severity -----------------------------------------

  #region --- Source --------------------------------------------
  public bool IncludeSource { get; set; } = true;

  public int SourceWidth { get; set; } = DEFAULT_SOURCE_WIDTH;
  #endregion --- Source -----------------------------------------

  #region --- Timestamp --------------------------------------------
  public bool IncludeTimestamp { get; set; } = true;
  public string TimestampFormat { get; set; } = DEFAULT_TIMESTAMP_FORMAT;
  #endregion --- Timestamp -----------------------------------------

  public bool IsVerbose { get; set; } = false;

  #region --- Box width --------------------------------------------
  public static int GlobalBoxWidth = DEFAULT_BOX_WIDTH;
  public int BoxWidth {
    get {
      return _BoxWidth;
    }
    set {
      if (value >= 3) {
        _BoxWidth = value;
      }
    }
  }

  [DoNotDump]
  private static int _BoxWidth = GlobalBoxWidth;
  #endregion --- Box width -----------------------------------------

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// Is the initialization done ?
  /// </summary>
  [DoNotDump]
  protected bool _IsInitialized = false;

  /// <summary>
  /// Initialize some internal parameters
  /// </summary>
  protected virtual void _Initialize() {
    _IsInitialized = true;
  }
  #region --- IDisposable --------------------------------------------
  [DoNotDump]
  private bool disposedValue = false; // To detect redundant calls

  /// <inheritdoc/>
  protected virtual void Dispose(bool disposing) {
    if (!disposedValue) {
      if (disposing) {
        // Insert here actions to dispose
      }

      disposedValue = true;
    }
  }

  public void Dispose() {
    Dispose(true);
  }
  #endregion --- IDisposable --------------------------------------------

  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"{nameof(Timeout)} = {Timeout} ms");
    RetVal.AppendLine($"{nameof(GlobalTimeout)} = {GlobalTimeout} ms");

    RetVal.AppendLine($"{nameof(IncludeSource)} = {IncludeSource}");
    RetVal.AppendLine($"{nameof(SourceWidth)} = {SourceWidth}");

    RetVal.AppendLine($"{nameof(IncludeTimestamp)} = {IncludeTimestamp}");
    RetVal.AppendLine($"{nameof(TimestampFormat)} = {TimestampFormat}");

    RetVal.AppendLine($"{nameof(IncludeSeverity)} = {IncludeSeverity}");
    RetVal.AppendLine($"{nameof(SeverityLimit)} = {SeverityLimit}");
    RetVal.AppendLine($"{nameof(GlobalSeverityLimit)} = {GlobalSeverityLimit}");

    RetVal.AppendLine($"{nameof(IsVerbose)} = {IsVerbose}");
    RetVal.AppendLine($"{nameof(BoxWidth)} = {BoxWidth} chars");
    return RetVal.ToString();
  }

  //public string Dump() {
  //  StringBuilder RetVal = new StringBuilder();
  //  RetVal.AppendLine($"{nameof(Timeout)} = {Timeout} ms,");
  //  RetVal.AppendLine($"{nameof(GlobalTimeout)} = {GlobalTimeout} ms,");

  //  RetVal.AppendLine($"{nameof(IncludeSource)} = {IncludeSource}");
  //  RetVal.AppendLine($"{nameof(SourceWidth)} = {SourceWidth}");

  //  RetVal.AppendLine($"{nameof(IncludeTimestamp)} = {IncludeTimestamp}");
  //  RetVal.AppendLine($"{nameof(TimestampFormat)} = {TimestampFormat}");

  //  RetVal.AppendLine($"{nameof(IncludeSeverity)} = {IncludeSeverity}");
  //  RetVal.AppendLine($"{nameof(SeverityLimit)} = {SeverityLimit}");
  //  RetVal.AppendLine($"{nameof(GlobalSeverityLimit)} = {GlobalSeverityLimit}");

  //  RetVal.AppendLine($"{nameof(IsVerbose)} = {IsVerbose}");
  //  RetVal.AppendLine($"{nameof(BoxWidth)} = {BoxWidth} chars");
  //  return RetVal.ToString();
  //}

  public static TLoggerOptions Default => new TLoggerOptions();
  public static TLoggerOptions MessageOnly => new TLoggerOptions() { IncludeSeverity = false, IncludeSource = false, IncludeTimestamp = false };
}
