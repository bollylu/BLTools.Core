using static BLTools.Core.Test._Support_.ConsoleHelper;

namespace BLTools.Core.Logging.Test;

public class TFileLoggerTest {

  private const string LOG_FILENAME = "TFileLoggerTest.log";

  [Test]
  public void InstanciateLogger() {

    using (ILogger Logger = new TFileLogger<LoggerTest>(LOG_FILENAME)) {

      Logger.Message("Created TConsoleLogger Default");

      Logger.Dump(Logger);

      Assert.That(Logger, Is.InstanceOf<ILogger>());
      Assert.That(Logger, Is.InstanceOf<ILogger<LoggerTest>>());
      Assert.That(Logger, Is.InstanceOf<ALogger<LoggerTest>>());
      Assert.That(Logger, Is.InstanceOf<TFileLogger<LoggerTest>>());

      ILogger LoggerMO = new TFileLogger<LoggerTest>(LOG_FILENAME, TLoggerOptions.MessageOnly);
      LoggerMO.Message("Created TFileLogger MessageOnly");

      LoggerMO.Dump(LoggerMO);

      Assert.That(LoggerMO, Is.InstanceOf<ILogger>());
      Assert.That(LoggerMO, Is.InstanceOf<ILogger<LoggerTest>>());
      Assert.That(LoggerMO, Is.InstanceOf<ALogger<LoggerTest>>());
      Assert.That(LoggerMO, Is.InstanceOf<TFileLogger<LoggerTest>>());

      Logger.Ok();
    }
  }

  [Test]
  public void BasicLoggerTest() {


    using (ILogger Logger = new TFileLogger<LoggerTest>(LOG_FILENAME) { Options = TLoggerOptions.Default }) {

      TFileLogger<LoggerTest> FileLogger = (TFileLogger<LoggerTest>)Logger;
      FileLogger.ResetLog();
      Logger.Options.SeverityLimit = ESeverity.DebugEx;
      Logger.Message("Create TFileLogger");

      const string MESSAGE = "Message to the log";

      Logger.Dump(Logger);

      string CallerTypeName = nameof(LoggerTest);
      string CallerName = nameof(BasicLoggerTest);
      string FullCallerName = $"{CallerTypeName}.{CallerName}";

      Logger.Message($"Send {MESSAGE.WithQuotes()} to Logger");
      Logger.Message($"All rows must contain \"{CallerTypeName}.{CallerName}\" in source field");
      string TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
      Assert.That(TestMessage, Does.Contain(FullCallerName));
      FileLogger.ResetLog();

      Logger.LogWarning(MESSAGE);
      TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
      Assert.That(TestMessage, Does.Contain(FullCallerName));
      FileLogger.ResetLog();

      Logger.LogDebug(MESSAGE);
      TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
      Assert.That(TestMessage, Does.Contain(FullCallerName));
      FileLogger.ResetLog();

      Logger.LogDebugEx(MESSAGE);
      TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
      Assert.That(TestMessage, Does.Contain(FullCallerName));
      FileLogger.ResetLog();

      Logger.LogError(MESSAGE);
      TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
      Assert.That(TestMessage, Does.Contain(FullCallerName));
      FileLogger.ResetLog();

      Logger.LogFatal(MESSAGE);
      TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
      Assert.That(TestMessage, Does.Contain(FullCallerName));
      FileLogger.ResetLog();

      try {
        throw new ApplicationException("fake ex");
      } catch (Exception ex) {
        Logger.LogErrorBox("Exception", ex, ELogErrorOptions.WithStackTrace);
        TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
        Assert.That(TestMessage, Does.Contain(FullCallerName));
        FileLogger.ResetLog();
      }

      try {
        throw new ApplicationException("fake fatal ex");
      } catch (Exception ex) {
        Logger.LogFatalBox("Exception", ex, ELogErrorOptions.WithStackTrace);
        TestMessage = File.ReadAllLines(LOG_FILENAME).LastOrDefault() ?? string.Empty;
        Assert.That(TestMessage, Does.Contain(FullCallerName));
        FileLogger.ResetLog();
      }

      Logger.Ok();
    }
  }

}
