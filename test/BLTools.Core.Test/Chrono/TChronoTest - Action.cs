namespace BLTools.Core.Test.Chrono;

public partial class TChronoTest {

  [Test]
  public void Chrono_ExecuteAction() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute 1 sec action");
      Target.ExecuteAction(() => Thread.Sleep(1000));
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

  [Test]
  public void Chrono_ExecuteActionWithOneArg() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute 1 sec action");
      Target.ExecuteAction<int>(x => Thread.Sleep(x), 1000);
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

  [Test]
  public void Chrono_ExecuteActionWithTwoArgs() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute 1 sec action");
      Target.ExecuteAction<int, int>((x, y) => Thread.Sleep(x + y), 500, 250);
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

  [Test]
  public void Chrono_ExecuteAction_WithEvents() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Message("Setting events");
      Target.ChronoStarted += (sender, args) => Logger.Message("EVENT: Chrono started");
      Target.ChronoStopped += (sender, args) => Logger.Message($"EVENT: Chrono stopped : duration = {args.DisplayTime()}");
      Logger.Dump(Target);
      Logger.Message("Execute 1 sec action");
      Target.ExecuteAction(() => Thread.Sleep(1000));
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

  [Test]
  public async Task Chrono_ExecuteTask_WithEvents() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Target.ChronoStarted += (sender, args) => Logger.Message("EVENT: Chrono started");
      Target.ChronoStopped += (sender, args) => Logger.Message($"EVENT: Chrono stopped : duration = {args.DisplayTime()}");
      Logger.Dump(Target);
      Logger.Message("Execute 1 sec task");
      await Target.ExecuteTaskAsync(Task.Delay(1000));
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }
}
