using System.Text;

namespace BLTools.Core.Test._Support_;

public static class ConsoleHelper {

  #region --- Support --------------------------------------------
  private static readonly TextWriter ConsoleOut = Console.Out;

  private static readonly Lock ConsoleLock = new Lock();

  internal static string CaptureConsoleOutput(Action action) {
    StringBuilder RetVal = new();
    using (TextWriter CaptureOut = new StringWriter(RetVal)) {
      lock (ConsoleLock) {
        Console.SetOut(CaptureOut);
        action();
        Console.SetOut(ConsoleOut);
      }
      return RetVal.ToString();
    }
  }
  #endregion --- Support --------------------------------------------

}
