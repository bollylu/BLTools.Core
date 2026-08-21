using System.Reflection;

using BLTools.Core.Diagnostic;

using NUnit.Framework.Constraints;

namespace BLTools.Core.Test.Diagnostics;

public class TAboutTest {

  private ILogger Logger = new TConsoleLogger<TAboutTest>() { Options = TLoggerOptions.MessageOnly};

  [OneTimeTearDown]
  public void OneTimeTearDown() {
    Logger.Dispose();
  }

  [SetUp]
  public void Setup() {
  
  }

  [Test]
  public void TAboutInit() {
    Logger.Message("Instanciate a new TAbout");
    TAbout About = TAbout.Calling;
    About.Initialize();
    string ExpectedName = Assembly.GetExecutingAssembly().GetName().Name.OrNull();
    Assert.That(About.Name, Is.EqualTo(ExpectedName));
    Logger.Message($"{nameof(About.Name)} = {About.Name}");
    Assert.That(About.Description.StartsWith(ExpectedName), Is.True);
    Logger.Message($"{nameof(About.Description)} = {About.Description}");
    Assert.That(About.CurrentVersion, Is.Not.EqualTo(new Version(0, 0)));
    Logger.Message($"{nameof(About.CurrentVersion)} = {About.CurrentVersion}");
    Assert.That(About.ChangeLog.Any() , Is.True);
    Logger.Message($"{nameof(About.ChangeLog)} = \n{About.ChangeLog}");

    Logger.Ok();
  }
}
