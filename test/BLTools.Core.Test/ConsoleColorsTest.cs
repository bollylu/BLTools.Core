using BLTools.Core.Logging;

namespace BLTools.Core.Test;

public class ConsoleColorsTest {

  [Test]
  public void DisplayTextInColors() {
    using ILogger Logger = new TTextWriterLogger<ConsoleColorsTest>(TestContext.Out);

    Logger.Log($"{"Hello dear ".Style_FG_Red().Style_BG_White()} {"Luc".Style_FG_Green().Style_BG_White()}.");
  }

}
