using static BLTools.Core.Test._Support_.ConsoleHelper;

namespace BLTools.Core.Logging.Test;

public class LoggerTest {

  [Test]
  public void InstanciateLogger() {

    using (ILogger Logger = new TConsoleLogger<LoggerTest>()) {

      Logger.Message("Created TConsoleLogger Default");

      Logger.Dump(Logger);

      Assert.That(Logger, Is.InstanceOf<ILogger>());
      Assert.That(Logger, Is.InstanceOf<ILogger<LoggerTest>>());
      Assert.That(Logger, Is.InstanceOf<ALogger<LoggerTest>>());
      Assert.That(Logger, Is.InstanceOf<TConsoleLogger<LoggerTest>>());

      ILogger LoggerMO = new TConsoleLogger<LoggerTest>(TLoggerOptions.MessageOnly);
      LoggerMO.Message("Created TConsoleLogger MessageOnly");


      LoggerMO.Dump(LoggerMO);

      Assert.That(LoggerMO, Is.InstanceOf<ILogger>());
      Assert.That(LoggerMO, Is.InstanceOf<ILogger<LoggerTest>>());
      Assert.That(LoggerMO, Is.InstanceOf<ALogger<LoggerTest>>());
      Assert.That(LoggerMO, Is.InstanceOf<TConsoleLogger<LoggerTest>>());

      Logger.Ok();
    }
  }

  [Test]
  public void BasicLoggerTest() {
    using (ILogger Logger = new TConsoleLogger<LoggerTest>(TLoggerOptions.Default)) {
      Logger.Message("Create TConsoleLogger");

      const string MESSAGE = "Message to the log";

      Logger.Dump(Logger);

      string CallerTypeName = nameof(LoggerTest);
      string CallerName = nameof(BasicLoggerTest);
      string FullCallerName = $"{CallerTypeName}.{CallerName}";



      Logger.Message($"Send {MESSAGE.WithQuotes()} to Logger");
      Logger.Message($"All rows must contain \"{CallerTypeName}.{CallerName}\" in source field");

      string TestMessage = "";

      TestMessage = CaptureConsoleOutput(() => Logger.Log(MESSAGE));
      Logger.Log(MESSAGE);
      Assert.That(TestMessage, Does.Contain(FullCallerName));

      TestMessage = CaptureConsoleOutput(() => Logger.LogWarning(MESSAGE));
      Logger.LogWarning(MESSAGE);
      Assert.That(TestMessage, Does.Contain(FullCallerName));

      TestMessage = CaptureConsoleOutput(() => Logger.LogDebug(MESSAGE));
      Logger.LogDebug(MESSAGE);
      Assert.That(TestMessage, Does.Contain(FullCallerName));

      TestMessage = CaptureConsoleOutput(() => Logger.LogDebugEx(MESSAGE));
      Logger.LogDebugEx(MESSAGE);
      Assert.That(TestMessage, Does.Contain(FullCallerName));

      TestMessage = CaptureConsoleOutput(() => Logger.LogError(MESSAGE));
      Logger.LogError(MESSAGE);
      Assert.That(TestMessage, Does.Contain(FullCallerName));

      TestMessage = CaptureConsoleOutput(() => Logger.LogFatal(MESSAGE));
      Logger.LogFatal(MESSAGE);
      Assert.That(TestMessage, Does.Contain(FullCallerName));

      try {
        throw new ApplicationException("fake ex");
      } catch (Exception ex) {
        TestMessage = CaptureConsoleOutput(() => Logger.LogErrorBox("Exception", ex, ELogErrorOptions.WithStackTrace));
        Logger.LogErrorBox("Exception", ex, ELogErrorOptions.WithStackTrace);
        Assert.That(TestMessage, Does.Contain(FullCallerName));
      }

      try {
        throw new ApplicationException("fake fatal ex");
      } catch (Exception ex) {
        TestMessage = CaptureConsoleOutput(() => Logger.LogFatalBox("Exception", ex, ELogErrorOptions.WithStackTrace));
        Logger.LogFatalBox("Exception", ex, ELogErrorOptions.WithStackTrace);
        Assert.That(TestMessage, Does.Contain(FullCallerName));
      }

      Logger.Ok();
    }
  }

}
