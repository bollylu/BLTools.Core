namespace BLTools.Core.Test.Chrono;

public partial class TChronoTest {

  [Test]
  public void Chrono_ExecuteFunction() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute a function");
      Func<IEnumerable<string>> GetDataList = new Func<IEnumerable<string>>(() => GetData());
      IEnumerable<string> Result = Target.ExecuteFunction(GetDataList);
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

  [Test]
  public void Chrono_ExecuteFunctionWithOneArg() {
    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);
    Logger.Message("Creating TChrono");
    using (TChrono Target = new TChrono()) {
      Logger.Dump(Target);
      Logger.Message("Execute a function");
      Func<string, IEnumerable<string>> GetDataList = new Func<string, IEnumerable<string>>(x => GetData(x));
      IEnumerable<string> Result = Target.ExecuteFunction(GetDataList, " ");
      Logger.Dump(Target);
      Logger.Message($"Duration = {Target.ElapsedTime.DisplayTime()}");
    }
    Logger.Ok();
  }

  private static string[] DataSource = new string[] {
    "My first data",
    "Another one",
    "And more"
  };

  private IEnumerable<string> GetData(string text = "") {
    foreach (string TextItem in DataSource.Where(x => x.Contains(text, StringComparison.InvariantCultureIgnoreCase))) {
      yield return TextItem;
      Thread.Sleep(100);
    }
  }
}
