namespace BLTools.Test.Logging;

public class LoggableTests {

  [Test]
  public void BasicLoggableTest() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");

    Logger.SeverityLimit = ESeverity.DebugEx;
    Dump(Logger);

    Assert.IsInstanceOfType(Logger, typeof(TConsoleLogger));
    string CallerType = nameof(LoggableTests);
    string CallerName = nameof(BasicLoggableTest);

    Message("Send messages to Logger");
    Message($"Must contain {CallerType.WithQuotes()}");
    Message($"Must contain {CallerName.WithQuotes()}");

    Assert.IsTrue(CaptureConsoleOutput("Message", () => Log(MESSAGE)).Contains(CallerName));
    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogWarning(MESSAGE)).Contains(CallerName));
    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogDebug(MESSAGE)).Contains(CallerName));
    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogDebugEx(MESSAGE)).Contains(CallerName));
    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogError(MESSAGE)).Contains(CallerName));
    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogFatal(MESSAGE)).Contains(CallerName));

    Ok();
  }

  [Test]
  public void LoggableExceptionTest() {
    Message("Create TConsoleLogger");

    Logger.SeverityLimit = ESeverity.DebugEx;
    Dump(Logger);

    Assert.IsInstanceOfType(Logger, typeof(TConsoleLogger));
    string CallerType = nameof(LoggableTests);
    string CallerName = nameof(LoggableExceptionTest);

    Message("Send messages to Logger");
    Message($"Must contain {CallerType.WithQuotes()}");
    Message($"Must contain {CallerName.WithQuotes()}");

    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogErrorBox("Exception", new ApplicationException("fake ex"), 30)).Contains(CallerName));
    Assert.IsTrue(CaptureConsoleOutput("Message", () => LogFatalBox("Exception", new ApplicationException("fake ex"), 30)).Contains(CallerName));

    Ok();
  }
}
