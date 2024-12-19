namespace BLTools.Core.Logging;

/// <summary>
/// Add the possibility to log information through an ILogger
/// </summary>
public interface ILoggable {

  /// <summary>
  /// The ILogger to implement
  /// </summary>
  public ILogger Logger { get; }

}

/// <summary>
/// Add the possibility to log information through a named ILogger
/// </summary>
public interface ILoggable<T> where T : class {

  /// <summary>
  /// The ILogger&lt;T&gt; to implement
  /// </summary>
  public ILogger<T> Logger { get; }

}
