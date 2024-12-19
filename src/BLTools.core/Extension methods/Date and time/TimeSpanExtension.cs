namespace BLTools.Core;

/// <summary>
/// DateTime extensions
/// </summary>
public static class TimeSpanExtension {

  /// <summary>
  /// Format the display of timespan
  /// </summary>
  /// <param name="source">The source timespan</param>
  /// <returns>The formatted timespan (d,h,m,s,ms)</returns>
  public static string DisplayTime(this TimeSpan source) {
    if (source.TotalMilliseconds < 1_000) { // 1 second
      return $"{source.TotalMilliseconds} ms";
    }
    if (source.TotalMilliseconds < 60_000) { // 1 minute
      return $"{source.Seconds} s {source.Milliseconds} ms";
    }
    if (source.TotalMilliseconds < 3_600_000) { // 1 hour
      return $"{source.Minutes} m {source.Seconds} s {source.Milliseconds} ms";
    }

    if (source.TotalMilliseconds < 86_400_000) { // 1 day
      return $"{source.Hours} h {source.Minutes} m {source.Seconds} s {source.Milliseconds} ms";
    }

    return $"{source.Days} d {source.Hours} h {source.Minutes} m {source.Seconds} s {source.Milliseconds} ms";
  }
}