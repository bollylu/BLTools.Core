namespace BLTools.Test.Diagnostic.Logging;

[TestClass]
public class LoggerExtendedTest {

  [TestMethod]
  public void BasicLog() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger() { SeverityLimit = ESeverity.DebugEx }) {
      Target.SourceWidth = 40;
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));

      Message("Send messages to Logger");
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.Log(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogWarning(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebug(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebugEx(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogError(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogFatal(MESSAGE)).Contains(MESSAGE));
    }

    Ok();
  }

  [TestMethod]
  public void LogWithCallerName() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger() {
      SeverityLimit = ESeverity.DebugEx
    }) {
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      string CallerName = nameof(LogWithCallerName);

      Message("Send messages to Logger");
      Message($"Must contain {CallerName.WithQuotes()}");
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.Log(MESSAGE)).Contains(CallerName));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogWarning(MESSAGE)).Contains(CallerName));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebug(MESSAGE)).Contains(CallerName));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebugEx(MESSAGE)).Contains(CallerName));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogError(MESSAGE)).Contains(CallerName));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogFatal(MESSAGE)).Contains(CallerName));
    }

    Ok();
  }

  [TestMethod]
  public void LogWithCallerTypeManual() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger() {
      SeverityLimit = ESeverity.DebugEx
    }) {
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      string CallerType = nameof(LoggerExtendedTest);

      Message("Send messages to Logger");
      Message($"Must contain {typeof(LoggerExtendedTest).Name.WithQuotes()}");
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.Log(MESSAGE, typeof(LoggerExtendedTest))).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogWarning(MESSAGE, typeof(LoggerExtendedTest))).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebug(MESSAGE, typeof(LoggerExtendedTest))).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebugEx(MESSAGE, typeof(LoggerExtendedTest))).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogError(MESSAGE, typeof(LoggerExtendedTest))).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogFatal(MESSAGE, typeof(LoggerExtendedTest))).Contains(CallerType));
    }

    Ok();
  }

  [TestMethod]
  public void LogWithCallerTypeAndCallerMethodManual() {
    const string MESSAGE = "Message to the log";
    const string CALLER_METHOD = "CallerMethod";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger() {
      SeverityLimit = ESeverity.DebugEx
    }) {
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      string CallerType = nameof(LoggerExtendedTest);

      Message("Send messages to Logger");
      Message($"Must contain {typeof(LoggerExtendedTest).Name.WithQuotes()}");
      Message($"Must contain {CALLER_METHOD.WithQuotes()}");
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.Log(MESSAGE, typeof(LoggerExtendedTest), CALLER_METHOD)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogWarning(MESSAGE, typeof(LoggerExtendedTest), CALLER_METHOD)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebug(MESSAGE, typeof(LoggerExtendedTest), CALLER_METHOD)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebugEx(MESSAGE, typeof(LoggerExtendedTest), CALLER_METHOD)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogError(MESSAGE, typeof(LoggerExtendedTest), CALLER_METHOD)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogFatal(MESSAGE, typeof(LoggerExtendedTest), CALLER_METHOD)).Contains(CallerType));
    }

    Ok();
  }

  [TestMethod]
  public void LogWithCallerTypeGeneric() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger<LoggerExtendedTest>() {
      SeverityLimit = ESeverity.DebugEx,
      SourceWidth = 60
    }) {
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<LoggerExtendedTest>));
      string CallerType = $"{nameof(LoggerExtendedTest)}.{nameof(LogWithCallerTypeGeneric)}";

      Message("Send messages to Logger");
      Message($"Must contain {CallerType.WithQuotes()}");
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.Log(MESSAGE)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogWarning(MESSAGE)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebug(MESSAGE)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogDebugEx(MESSAGE)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogError(MESSAGE)).Contains(CallerType));
      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogFatal(MESSAGE)).Contains(CallerType));
    }

    Ok();
  }


}
