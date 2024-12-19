namespace BLTools.Core;

public partial class TChrono {

  /// <summary>
  /// Execute a function with chrono activated
  /// </summary>
  /// <typeparam name="T">The type of the return value</typeparam>
  /// <param name="func">The function to evaluate</param>
  /// <returns></returns>
  public T ExecuteFunction<T>(Func<T> func) {
    Reset();
    T RetVal = func();
    Stop();
    return RetVal;
  }

  /// <summary>
  /// Execute a function with chrono activated
  /// </summary>
  /// <typeparam name="I">The type of the source value</typeparam>
  /// <typeparam name="R">The type of the return value</typeparam>
  /// <param name="func">The function to evaluate</param>
  /// <param name="source">The source value</param>
  /// <returns></returns>
  public R ExecuteFunction<I, R>(Func<I, R> func, I source) {
    Reset();
    R RetVal = func(source);
    Stop();
    return RetVal;
  }

  /// <summary>
  /// Execute a function with chrono activated
  /// </summary>
  /// <typeparam name="I1">The type of the source1 value</typeparam>
  /// <typeparam name="I2">The type of the source2 value</typeparam>
  /// <typeparam name="R">The type of the return value</typeparam>
  /// <param name="func">The function to evaluate</param>
  /// <param name="source1">The source1 value</param>
  /// <param name="source2">The source2 value</param>
  /// <returns></returns>
  public R ExecuteFunction<I1, I2, R>(Func<I1, I2, R> func, I1 source1, I2 source2) {
    Reset();
    R RetVal = func(source1, source2);
    Stop();
    return RetVal;
  }

}

