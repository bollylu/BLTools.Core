namespace BLTools.core.Logging.Loggers;

public static class TLogger {
  public static ILogger CreateLoggerFor<TSource>(ILogger sourceLogger) where TSource : class {
    return sourceLogger.CreateLoggerFor<TSource>();
  }
}
