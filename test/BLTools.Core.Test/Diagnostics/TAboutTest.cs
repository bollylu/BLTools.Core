using BLTools.Core.Diagnostic;

namespace BLTools.Core.Test.Diagnostics;

public class TAboutTest {

  [Test]
  public void TAboutInit() {
    using (ILogger Logger = new TConsoleLogger<TAboutTest>()) {

      Logger.Message("Instanciate a new TAbout");
      TAbout About = TAbout.Calling;
      About.Initialize();
      Logger.Message($"{nameof(About.Name)} = {About.Name}");
      Logger.Message($"{nameof(About.Description)} = {About.Description}");
      Logger.Message($"{nameof(About.CurrentVersion)} = {About.CurrentVersion}");
      Logger.Message($"{nameof(About.ChangeLog)} = \n{About.ChangeLog}");

      Logger.Ok();
    }
  }
}
