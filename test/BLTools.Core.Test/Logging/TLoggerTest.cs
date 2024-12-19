namespace BLTools.Test.Diagnostic.Logging;

[TestClass()]
public class TLoggerTest {

  #region --- TFileLogger --------------------------------------------
  [TestMethod]
  public void Instanciate_TFileLogger_ConstructorWithValidFilename() {
    string LogFile = Path.GetTempFileName();

    Message("Create TFileLogger");
    using (ILogger Target = new TFileLogger(LogFile)) {
      Dump(Target);
      Assert.IsInstanceOfType(Target, typeof(TFileLogger));
      Assert.AreEqual(ESeverity.Info, Target.SeverityLimit);
      if (Target is TFileLogger FileLogger) {
        Assert.IsNotNull(FileLogger.Filename);
      }
    }

    Ok();
  }

  [TestMethod]
  public void Instanciate_TFileLogger_ConstructorWithValidFilename_Generic() {
    string LogFile = Path.GetTempFileName();

    Message("Create TFileLogger<SplitArgs>");
    using (ILogger Target = new TFileLogger<SplitArgs>(LogFile)) {
      Dump(Target);
      Assert.IsInstanceOfType(Target, typeof(TFileLogger));
      Assert.IsInstanceOfType(Target, typeof(TFileLogger<SplitArgs>));
      Assert.AreEqual(ESeverity.Info, Target.SeverityLimit);
      if (Target is TFileLogger FileLogger) {
        Assert.IsNotNull(FileLogger.Filename);
      }
    }

    Ok();
  }
  #endregion --- TFileLogger --------------------------------------------

  #region --- TConsoleLogger --------------------------------------------
  [TestMethod]
  public void Instanciate_TConsoleLogger_ConstructorEmpty() {
    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger()) {
      Dump(Target);
      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      Assert.AreEqual(ESeverity.Info, Target.SeverityLimit);
    }
    Ok();
  }

  [TestMethod]
  public void Instanciate_TConsoleLogger_ConstructorGeneric() {
    Message("Create TConsoleLogger<SplitArgs>");
    using (ILogger Target = new TConsoleLogger<SplitArgs>()) {
      Dump(Target);
      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<SplitArgs>));
      Assert.AreEqual(ESeverity.Info, Target.SeverityLimit);
    }
    Ok();
  }

  [TestMethod]
  public void TConsoleLogger_LogInfo() {
    const string MESSAGE = "Message to the log";
    const string MANUAL_SOURCE = "Manual source";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger()) {
      Target.SourceWidth = 40;
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));

      Message("Send messages to ILogger");

      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogText(MESSAGE)).Contains(MESSAGE));

      Assert.IsTrue(CaptureConsoleOutput("Message + source", () => Target.LogText(MESSAGE, MANUAL_SOURCE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message + source", () => Target.LogText(MESSAGE, MANUAL_SOURCE)).Contains($"|{ESeverity.Info}"));
      Assert.IsTrue(CaptureConsoleOutput("Message + source", () => Target.LogText(MESSAGE, MANUAL_SOURCE)).Contains(MANUAL_SOURCE));

      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains($"|{ESeverity.Info}"));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains($"{nameof(TConsoleLogger)}.{nameof(TConsoleLogger_LogInfo)}"));

    }

    Ok();
  }


  [TestMethod]
  public void TConsoleLogger_LogInfo_TypedLogger() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger<SplitArgs>()) {
      Target.SourceWidth = 50;
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<SplitArgs>));

      Message("Send messages to ILogger");

      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogText(MESSAGE)).Contains(MESSAGE));

      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains($"|{ESeverity.Info}"));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains($"{nameof(SplitArgs)}.{nameof(TConsoleLogger_LogInfo_TypedLogger)}"));

    }

    Ok();
  }

  [TestMethod]
  public void TConsoleLogger2_LogInfo() {
    const string MESSAGE = "Message to the log";
    const string MANUAL_SOURCE = "Manual source";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger<SplitArgs>()) {
      Target.SourceWidth = 45;
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<SplitArgs>));

      Message("Send messages to ILogger");

      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogText(MESSAGE)).Contains(MESSAGE));

      Assert.IsTrue(CaptureConsoleOutput("Message + source", () => Target.LogText(MESSAGE, MANUAL_SOURCE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message + source", () => Target.LogText(MESSAGE, MANUAL_SOURCE)).Contains($"|{ESeverity.Info}"));
      Assert.IsTrue(CaptureConsoleOutput("Message + source", () => Target.LogText(MESSAGE, MANUAL_SOURCE)).Contains(MANUAL_SOURCE));

      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains($"|{ESeverity.Info}"));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.Log(MESSAGE)).Contains($"{nameof(SplitArgs)}.{nameof(TConsoleLogger2_LogInfo)}"));

    }

    Ok();
  }

  [TestMethod]
  public void TConsoleLogger_LogWarning() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger()) {
      Target.SourceWidth = 40;
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));

      Message("Send messages to ILogger");

      Assert.IsTrue(CaptureConsoleOutput("Warning + source", () => Target.LogWarning(MESSAGE)).Contains($"|{ESeverity.Warning}"));
      Assert.IsTrue(CaptureConsoleOutput("Warning + source", () => Target.LogWarning(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Warning + source", () => Target.LogWarning(MESSAGE)).Contains($"{nameof(TConsoleLogger)}.{nameof(TConsoleLogger_LogWarning)}"));
    }

    Ok();
  }

  [TestMethod]
  public void TConsoleLoggerT_LogWarningTyped() {
    const string MESSAGE = "Message to the log";

    Message("Create TConsoleLogger");
    using (ILogger Target = new TConsoleLogger<SplitArgs>()) {
      Target.SourceWidth = 45;
      Dump(Target);

      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
      Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<SplitArgs>));

      Message("Send messages to ILogger");

      Assert.IsTrue(CaptureConsoleOutput("Message", () => Target.LogText(MESSAGE)).Contains(MESSAGE));

      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.LogWarning(MESSAGE)).Contains(MESSAGE));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.LogWarning(MESSAGE)).Contains($"|{ESeverity.Warning}"));
      Assert.IsTrue(CaptureConsoleOutput("Message + source auto", () => Target.LogWarning(MESSAGE)).Contains($"{nameof(SplitArgs)}.{nameof(TConsoleLoggerT_LogWarningTyped)}"));

    }

    Ok();
  }

  #endregion --- TConsoleLogger --------------------------------------------

  #region --- TTraceLogger --------------------------------------------
  [TestMethod]
  public void Intanciate_TTraceLogger_ConstructorEmpty() {
    Message("Create TTraceLogger");
    using (ILogger Target = new TTraceLogger()) {
      Dump(Target);
      Assert.IsInstanceOfType(Target, typeof(TTraceLogger));
      Assert.AreEqual(ESeverity.Info, Target.SeverityLimit);
    }

    Ok();
  }
  #endregion --- TTraceLogger --------------------------------------------
}
