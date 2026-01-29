namespace BLTools.Core.Test.Chrono;

public partial class TChronoTest {

  [Test]
  public void Instanciate_TChrono() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Assert.That(Target, Is.Not.Null);
      Logger.Dump(Target);
    }
    Logger.Ok();
  }

  [Test]
  public async Task Chrono_Reset() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute something during 2s");
      await Task.Delay(2000);
      Target.Stop();
      Logger.Dump(Target);
      Logger.Message("Reset chrono");
      Target.Reset();
      Logger.Dump(Target);
    }
    Logger.Ok();
  }

  [Test]
  public async Task Chrono_Stop() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Wait 1s");
      await Task.Delay(1000);
      Target.Stop();
      Logger.Dump(Target);
      Logger.Message("Reset chrono");
      Target.Reset();
      Logger.Dump(Target);
    }
    Logger.Ok();
  }


}
