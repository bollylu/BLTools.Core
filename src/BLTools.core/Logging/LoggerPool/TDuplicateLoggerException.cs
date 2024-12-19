namespace BLTools.Core.Logging;

/// <summary>
/// A duplicate logger is attempted to add to the pool
/// </summary>
public class TDuplicateLoggerException : Exception {

  /// <summary>
  /// A new duplicate logger exception
  /// </summary>
  /// <param name="message">The message</param>
  public TDuplicateLoggerException(string message) : base(message) { }
}
