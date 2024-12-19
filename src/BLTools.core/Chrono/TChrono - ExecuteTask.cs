namespace BLTools.Core;

public partial class TChrono {

  /// <summary>
  /// Execute a task with the chrono activated
  /// </summary>
  /// <param name="task">The action to execute</param>
  /// <returns>The duration of the task</returns>
  public async Task ExecuteTaskAsync(Task task) {
    Reset();
    await task;
    Stop();
  }

  /// <summary>
  /// Execute a task with the chrono activated
  /// </summary>
  /// <param name="task">The action to execute</param>
  /// <returns>The duration of the task</returns>
  public async Task<T> ExecuteTaskAsync<T>(Task<T> task) {
    Reset();
    T RetVal = await task;
    Stop();
    return RetVal;
  }
}

