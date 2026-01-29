namespace BLTools.Core;

public static partial class ConsoleExt {

  private const string DEFAULT_PAUSE_MESSAGE = "Press any key to continue...";
  private const string DEFAULT_TIMEOUT_FORMAT = @"hh\:mm\:ss\:ff";
  private const int DEFAULT_ANIMATION_DELAY = 200;

  /// <summary>
  /// Display "Press any key to continue..." on console, then wait for a key, possibly with a timeout in msec
  /// </summary>
  /// <param name="timeout">The timeout if no key is pressed</param>
  /// <param name="isAnimated">Will animation run during the pause</param>
  /// <param name="displayTimeout">Display the timeout counter during the pause</param>
  public static async Task PauseAsync(double timeout = 0, bool isAnimated = false, bool displayTimeout = false) {
    await PauseAsync(DEFAULT_PAUSE_MESSAGE, timeout, isAnimated, displayTimeout);
  }

  /// <summary>
  /// Display "Press any key to continue..." on console, then wait for a key, possibly with a timeout in msec
  /// </summary>
  /// <param name="timeout">The timeout if no key is pressed</param>
  /// <param name="isAnimated">Will animation run during the pause</param>
  /// <param name="displayTimeout">Display the timeout counter during the pause</param>
  public static void Pause(double timeout = 0, bool isAnimated = false, bool displayTimeout = false) {
    Pause(DEFAULT_PAUSE_MESSAGE, timeout, isAnimated, displayTimeout);
  }

  /// <summary>
  /// Display a message on console, then wait for a key, possibly with a timeout in msec
  /// </summary>
  /// <param name="message">The message to be displayed</param>
  /// <param name="timeout">The timeout if no key is pressed</param>
  /// <param name="isAnimated">Will animation run during the pause</param>
  /// <param name="displayTimeout">Display the timeout counter during the pause</param>
  public static async Task PauseAsync(string message, double timeout = 0, bool isAnimated = false, bool displayTimeout = false) {

    char[] Roll = ['|', '/', '-', '\\'];

    System.Console.Write(message);

    int SaveCursorLeft = System.Console.CursorLeft;
    int SaveCursorTop = System.Console.CursorTop;

    if (timeout == 0) {
      System.Console.ReadKey(true);
    } else {
      DateTime StartTime = DateTime.Now;
      int i = 0;
      while ((DateTime.Now - StartTime) < TimeSpan.FromMilliseconds(timeout) && !System.Console.KeyAvailable) {
        if (isAnimated) {
          System.Console.SetCursorPosition(SaveCursorLeft + 1, SaveCursorTop);
          System.Console.Write(Roll[i++ % 4]);
        }

        if (displayTimeout) {
          System.Console.SetCursorPosition(SaveCursorLeft + 3, SaveCursorTop);
          double ElapsedTime = (DateTime.Now - StartTime).TotalMilliseconds;
          System.Console.Write(TimeSpan.FromMilliseconds(timeout - ElapsedTime).ToString(DEFAULT_TIMEOUT_FORMAT));
        }

        await Task.Delay(DEFAULT_ANIMATION_DELAY);
      }
      if (System.Console.KeyAvailable) {
        System.Console.ReadKey(true);
      }
      System.Console.WriteLine();
    }
  }

  /// <summary>
  /// Display a message on console, then wait for a key, possibly with a timeout in msec
  /// </summary>
  /// <param name="message">The message to be displayed</param>
  /// <param name="timeout">The timeout if no key is pressed</param>
  /// <param name="isAnimated">Will animation run during the pause</param>
  /// <param name="displayTimeout">Display the timeout counter during the pause</param>
  public static void Pause(string message, double timeout = 0, bool isAnimated = false, bool displayTimeout = false) {

    char[] Roll = ['|', '/', '-', '\\'];

    System.Console.Write(message);

    int SaveCursorLeft = System.Console.CursorLeft;
    int SaveCursorTop = System.Console.CursorTop;

    if (timeout == 0) {
      System.Console.ReadKey(true);
    } else {
      DateTime StartTime = DateTime.Now;
      int i = 0;
      while ((DateTime.Now - StartTime) < TimeSpan.FromMilliseconds(timeout) && !System.Console.KeyAvailable) {
        if (isAnimated) {
          System.Console.SetCursorPosition(SaveCursorLeft + 1, SaveCursorTop);
          System.Console.Write(Roll[i++ % 4]);
        }

        if (displayTimeout) {
          System.Console.SetCursorPosition(SaveCursorLeft + 3, SaveCursorTop);
          double ElapsedTime = (DateTime.Now - StartTime).TotalMilliseconds;
          System.Console.Write(TimeSpan.FromMilliseconds(timeout - ElapsedTime).ToString(DEFAULT_TIMEOUT_FORMAT));
        }

        Thread.Sleep(DEFAULT_ANIMATION_DELAY);
      }

      if (System.Console.KeyAvailable) {
        System.Console.ReadKey(true);
      }

      System.Console.WriteLine();
    }
  }
}

