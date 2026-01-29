using BLTools.Core.Diagnostic;

namespace BLTools.Core.Test.Diagnostics;

public class DiagnosticsTest {

  [Test]
  public void StartProg_DisplayInfo() {
    using ILogger Logger = new TConsoleLogger<DiagnosticsTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Building startup info");
    string Target = ApplicationInfo.BuildStartupInfo();
    Logger.Dump(Target);
    Assert.That(Target.Contains("BLTools"));
    Logger.Ok();
  }

}
