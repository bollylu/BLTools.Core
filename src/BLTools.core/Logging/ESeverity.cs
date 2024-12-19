namespace BLTools.Core.Logging;

/// <summary>
/// Severity levels for log messages
/// </summary>
public enum ESeverity {
  /// <summary>
  /// Unknown
  /// </summary>
  Unknown,
  /// <summary>
  /// Debug extended. Messages from the deepest level.
  /// </summary>
  DebugEx,
  /// <summary>
  /// Debug. Messages for development purpose.
  /// </summary>
  Debug,
  /// <summary>
  /// Informational, default value for most messages
  /// </summary>
  Info,
  /// <summary>
  /// Warning. Non blocking error or serious information
  /// </summary>
  Warning,
  /// <summary>
  /// Error. The program is not working correctly, but it handles it
  /// </summary>
  Error,
  /// <summary>
  /// Fatal. The program is not working correctly and needs to stop.
  /// </summary>
  Fatal
}
