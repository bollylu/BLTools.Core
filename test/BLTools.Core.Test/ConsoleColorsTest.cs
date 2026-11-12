namespace BLTools.Core.Test.Extensions;

public class ConsoleColorsTest {

  [Test]
  public void DisplayTextInColors() {
    using ILogger Logger = new TConsoleLogger<ConsoleColorsTest>() {
      Options = TLoggerOptions.MessageOnly
    };

    Logger.Message("Output of a message with ansi color codes");
    string Message = $"{"Hello dear ".FG_Red().BG_White()}{"Luc".FG_Green().BG_White()}.";
    Assert.That(Message, Is.EqualTo($"{AnsiColorExtensions.ESC}{AnsiColorExtensions.BG_WHITE}{AnsiColorExtensions.ESC}{AnsiColorExtensions.FG_RED}Hello dear {AnsiColorExtensions.ESC}{AnsiColorExtensions.RESET}{AnsiColorExtensions.ESC}{AnsiColorExtensions.BG_WHITE}{AnsiColorExtensions.ESC}{AnsiColorExtensions.FG_GREEN}Luc{AnsiColorExtensions.ESC}{AnsiColorExtensions.RESET}."));
    Logger.Message(Message);
    Logger.Ok();
  }

}
