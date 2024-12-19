using System.Runtime.CompilerServices;

namespace BLTools.Core.Logging;

/// <summary>
/// Add the possibility to log information through an ILogger
/// </summary>
public abstract class ALoggable : ILoggable {

  [DoNotDump()]
  public virtual ILogger Logger { get; set; } = new TConsoleLogger();

}

/// <summary>
/// Add the possibility to log information through an ILogger&lt;T&gt;
/// </summary>
public abstract class ALoggable<T> : ALoggable where T : class {

  [DoNotDump()]
  public override ILogger Logger { get; set; } = new TConsoleLogger<T>();

}
