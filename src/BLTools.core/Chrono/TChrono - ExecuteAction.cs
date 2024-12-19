namespace BLTools.Core;

/// <summary>
/// Calculate the time betwen start and stop
/// </summary>
public partial class TChrono {

  /// <summary>
  /// Execute an action with the chrono activated
  /// </summary>
  /// <param name="action">The action to execute</param>
  /// <returns>The duration of the action</returns>
  public void ExecuteAction(Action action) {
    Reset();
    action();
    Stop();
  }

  /// <summary>
  /// Execute an action with the chrono activated
  /// </summary>
  /// <param name="action">The action to execute</param>
  /// <param name="arg">The first parameters to the action</param>
  /// <returns>The duration of the action</returns>
  public void ExecuteAction<T>(Action<T> action, T arg) {
    Reset();
    action(arg);
    Stop();
  }

  /// <summary>
  /// Execute an action with the chrono activated
  /// </summary>
  /// <param name="action">The action to execute</param>
  /// <param name="arg1">The first parameters to the action</param>
  /// <param name="arg2">The second parameters to the action</param>
  /// <returns>The duration of the action</returns>
  public void ExecuteAction<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2) {
    Reset();
    action(arg1, arg2);
    Stop();
  }

}

