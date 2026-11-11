namespace BLTools.Core;

/// <summary>
/// Extensions for tasks
/// </summary>
public static class TaskExtensions {
  extension(Task task) {

    /// <summary>
    /// Attempt to execute a task. When exception, retries several times
    /// </summary>
    /// <param name="task">The task to execute</param>
    /// <param name="attempt">The maximum number of times to try the task</param>
    /// <returns>A task</returns>
    public async Task Attempt(int attempt) {
      #region === Validate parameters ===
      if (attempt <= 0) {
        attempt = 1;
      }
      #endregion === Validate parameters ===

      int Counter = 1;
      while (Counter <= attempt) {
        try {
          Counter++;
          await task.ConfigureAwait(false);
          return;
        } catch (Exception ex) {
          Trace.WriteLine($"Error executing task on attempt {Counter} : {ex.Message}");
        }
      }
      return;
    }
  }
  extension<T>(Task<T?> task) {

    /// <summary>
    /// Attempt to execute a task&lt;T&gt;. When exception, retries several times
    /// </summary>
    /// <typeparam name="T">Type of parameter for the task</typeparam>
    /// <param name="task">The task to excecute</param>
    /// <param name="attempt">The maximum number of times to try the task</param>
    /// <returns>A task</returns>
    public async Task<T?> Attempt(int attempt) {
      #region === Validate parameters ===
      if (attempt <= 0) {
        attempt = 1;
      }
      #endregion === Validate parameters ===

      int Counter = 1;
      while (Counter <= attempt) {
        try {
          Counter++;
          return await task;
        } catch (Exception ex) {
          Trace.WriteLine($"Error executing task<T> on attempt {Counter} : {ex.Message}");
        }
      }
      return default(T?);
    }
  }
  extension(Task task) {

    /// <summary>
    /// Add a cancellation token to a non-cancellable task
    /// </summary>
    /// <param name="task">The source task</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A task</returns>
    /// <exception cref="OperationCanceledException"></exception>
    public async Task WithCancellation(CancellationToken cancellationToken) {

      TaskCompletionSource<bool> Tcs = new TaskCompletionSource<bool>();

      using (cancellationToken.Register(s => (s as TaskCompletionSource<bool>)?.TrySetResult(true), Tcs)) {
        if (task != await Task.WhenAny(task, Tcs.Task)) {
          throw new OperationCanceledException(cancellationToken);
        }
      }

    }



    /// <summary>
    /// Add a cancellation token (timeout) to a non-cancellable task
    /// </summary>
    /// <param name="task">The source task</param>
    /// <param name="timeoutInMs">The timeout in ms</param>
    /// <returns>A task</returns>
    /// <exception cref="OperationCanceledException"></exception>
    public async Task WithTimeout(int timeoutInMs) {

      CancellationToken CancelTimeout = new CancellationTokenSource(timeoutInMs).Token;

      TaskCompletionSource<bool> Tcs = new TaskCompletionSource<bool>();

      using (CancelTimeout.Register(s => (s as TaskCompletionSource<bool>)?.TrySetResult(true), Tcs)) {
        if (task != await Task.WhenAny(task, Tcs.Task)) {
          throw new OperationCanceledException(CancelTimeout);
        }
      }

    }
  }
  extension<T>(Task<T> task) {

    /// <summary>
    /// Add a cancellation token (timeout) to a non-cancellable task
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="task">The source task</param>
    /// <param name="cancellationToken">The cancellation token</param>
    /// <returns>A task</returns>
    /// <exception cref="OperationCanceledException"></exception>
    public async Task<T> WithCancellation(CancellationToken cancellationToken) {

      TaskCompletionSource<bool> Tcs = new TaskCompletionSource<bool>();

      using (cancellationToken.Register(s => (s as TaskCompletionSource<bool>)?.TrySetResult(true), Tcs)) {
        if (task != await Task.WhenAny(task, Tcs.Task)) {
          throw new OperationCanceledException(cancellationToken);
        }
      }

      return task.Result;
    }



    /// <summary>
    /// Add a cancellation token (timeout) to a non-cancellable task
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="task">The source task</param>
    /// <param name="timeoutInMs">The timeout in ms</param>
    /// <returns>A task</returns>
    /// <exception cref="OperationCanceledException"></exception>
    public async Task<T> WithTimeout(int timeoutInMs) {

      CancellationToken CancelTimeout = new CancellationTokenSource(timeoutInMs).Token;

      TaskCompletionSource<bool> Tcs = new TaskCompletionSource<bool>();

      using (CancelTimeout.Register(s => (s as TaskCompletionSource<bool>)?.TrySetResult(true), Tcs)) {
        if (task != await Task.WhenAny(task, Tcs.Task)) {
          throw new OperationCanceledException(CancelTimeout);
        }
      }

      return task.Result;
    }
  }
}
