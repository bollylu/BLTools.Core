namespace BLTools.Core.Test.Chrono;

public partial class TChronoTest {

  [Test]
  public async Task Chrono_ExecuteTask() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);

    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute 1 sec task");
      await Target.ExecuteTaskAsync(Task.Delay(1000));
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

}
