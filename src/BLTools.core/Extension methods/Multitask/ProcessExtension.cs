namespace BLTools.Core;

/// <summary>
/// Extensions for external processes
/// </summary>
public static class ProcessExtension {
  extension(Process process) {

    /// <summary>
    /// Wait for the end of execution of an external process, asynchronously
    /// </summary>
    /// <param name="process">The process to execute</param>
    /// <param name="cancellationToken">A possible cancellation token</param>
    /// <returns>A task</returns>
    public Task WaitForExitAsync(CancellationToken cancellationToken) {
      process.EnableRaisingEvents = true;
      var TCS = new TaskCompletionSource<object>();
      process.Exited += (sender, args) => TCS.TrySetResult(args);
      if (cancellationToken != default(CancellationToken)) {
        cancellationToken.Register(() => TCS.TrySetCanceled());
      }
      return TCS.Task;
    }



    /// <summary>
    /// Wait for the end of execution of an external process, asynchronously
    /// </summary>
    /// <param name="process">The process to execute</param>
    /// <param name="timeoutInMillisec">A number of ms</param>
    /// <returns>A task</returns>
    public Task WaitForExitAsync(int timeoutInMillisec) {
      CancellationTokenSource CTS = new CancellationTokenSource(timeoutInMillisec);
      return process.WaitForExitAsync(CTS.Token);
    }



    /// <summary>
    /// Wait for the end of execution of an external process, asynchronously
    /// </summary>
    /// <param name="process">The process to execute</param>
    /// <param name="timeSpan">A time span</param>
    public Task WaitForExitAsync(TimeSpan timeSpan) {
      CancellationTokenSource CTS = new CancellationTokenSource(timeSpan);
      return process.WaitForExitAsync(CTS.Token);
    }
  }
}
