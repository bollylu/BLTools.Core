namespace BLTools.Core;

/// <summary>
/// Calculate the time betwen start and stop
/// </summary>
public partial class TChrono : IDisposable {

  #region --- Public properties ------------------------------------------------------------------------------
  /// <summary>
  /// The time that the chrono was last started/resetted
  /// </summary>
  public DateTime StartTime { get; private set; }

  /// <summary>
  /// The time that the chrono was last stopped
  /// </summary>
  public DateTime StopTime { get; private set; }

  /// <summary>
  /// The elapsed time since the last start
  /// </summary>
  public TimeSpan ElapsedTime => StopTime - StartTime;
  #endregion --- Public properties ---------------------------------------------------------------------------

  #region --- Events --------------------------------------------
  /// <summary>
  /// Fired when the chrono is started
  /// </summary>
  public event EventHandler? ChronoStarted;
  /// <summary>
  /// Fired when the chrono is stopped
  /// </summary>
  public event EventHandler<TimeSpan>? ChronoStopped;
  #endregion --- Events --------------------------------------------

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// Builds a TChrono and start it
  /// </summary>
  public TChrono() {
    Reset();
  }

  /// <summary>
  /// Dispose of the chrono
  /// </summary>
  public void Dispose() {
    ChronoStarted = null;
    ChronoStopped = null;
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  #region --- Converters -------------------------------------------------------------------------------------
  /// <summary>
  /// Display the string representation of chrono
  /// </summary>
  /// <returns>A string</returns>
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"{nameof(StartTime)} = {StartTime.ToString("HH:mm:ss,FFFF")}");
    RetVal.AppendLine($"{nameof(StopTime)} = {StopTime.ToString("HH:mm:ss,FFFF")}");
    RetVal.AppendLine($"{nameof(ElapsedTime)} = {ElapsedTime.DisplayTime()}");
    RetVal.AppendLine($"{nameof(ChronoStarted)} = {ChronoStarted?.GetType().GetNameEx() ?? "(no event ChronoStarted)"}");
    RetVal.AppendLine($"{nameof(ChronoStopped)} = {ChronoStopped?.GetType().GetNameEx() ?? "(no event ChronoStopped)"}");
    return RetVal.ToString();
  }
  #endregion --- Converters ----------------------------------------------------------------------------------

  /// <summary>
  /// Indicate the chrono is stopped and give the time spend
  /// </summary>
  public void Stop() {
    StopTime = DateTime.Now;
    ChronoStopped?.Invoke(this, ElapsedTime);
  }

  /// <summary>
  /// Reset the chrono and indicate it has started
  /// </summary>
  public void Reset() {
    StartTime = DateTime.Now;
    StopTime = DateTime.MaxValue;
    ChronoStarted?.Invoke(this, EventArgs.Empty);
  }

}

