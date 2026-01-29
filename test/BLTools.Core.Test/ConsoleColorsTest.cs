using BLTools.Core.Logging;
using BLTools.Core;

namespace BLTools.Core.Test;

public class ConsoleColorsTest {

  [Test]
  public void DisplayTextInColors() {
    using ILogger Logger = new TTextWriterLogger<ConsoleColorsTest>(TestContext.Out);

    Logger.Log($"{"Hello dear ".FG_Red().BG_White()} {"Luc".FG_Green().BG_White()}.");
    Console.WriteLine();
  }

}
