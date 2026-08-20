namespace BLTools.Core.Test.Extensions;

public class ConsoleColorsTest {

  [Test]
  public void DisplayTextInColors() {
    using ILogger Logger = new TConsoleLogger<ConsoleColorsTest>() {
      Options = TLoggerOptions.MessageOnly
    };

    Logger.Message("Output of a message with ansi color codes");
    string Message = $"{"Hello dear ".FG_Red().BG_White()}{"Luc".FG_Green().BG_White()}.";
    Assert.That(Message, Is.EqualTo("\u001b[47m\u001b[31mHello dear \u001b[0m\u001b[47m\u001b[32mLuc\u001b[0m."));
    Logger.Message(Message);
    Logger.Ok();
  }

}
