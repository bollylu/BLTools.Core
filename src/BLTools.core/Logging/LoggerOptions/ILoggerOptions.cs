namespace BLTools.Core.Logging;
public interface ILoggerOptions {

  /// <summary>
  /// The maximum ms allowed to write a request
  /// </summary>
  int Timeout { get; set; }

  #region --- Severity --------------------------------------------
  /// <summary>
  /// Do we include the severity field
  /// </summary>
  bool IncludeSeverity { get; set; }

  /// <summary>
  /// The maximum severity to log. Values higher than this will be ignored
  /// </summary>
  ESeverity SeverityLimit { get; set; }
  #endregion --- Severity -----------------------------------------

  #region --- Source --------------------------------------------
  /// <summary>
  /// Do we include the source field
  /// </summary>
  bool IncludeSource { get; set; }

  /// <summary>
  /// The width of the source field
  /// </summary>
  int SourceWidth { get; set; }
  #endregion --- Source -----------------------------------------

  #region --- Timestamp --------------------------------------------
  /// <summary>
  /// Do we include the timestamp field
  /// </summary>
  bool IncludeTimestamp { get; set; }

  /// <summary>
  /// The format of the timestamp
  /// </summary>
  string TimestampFormat { get; set; }
  #endregion --- Timestamp -----------------------------------------

  /// <summary>
  /// When it's true, write also the verbose level
  /// </summary>
  bool IsVerbose { get; set; }

  /// <summary>
  /// The width of a box for the LogxxxBox methods
  /// </summary>
  int BoxWidth { get; set; }

}
