namespace BLTools.Core;

public static class ExceptionExtension {

  /// <summary>
  /// Provides extension methods for the Exception class
  /// </summary>
  extension(Exception ex) {
    /// <summary>
    /// Returns a string with the information of the exception
    /// </summary>
    /// <param name="withStackTrace">If true, the stack trace is included in the result</param>
    /// <param name="withInnerExceptionMessage">If true, the inner exception message is included in the result</param>
    /// <returns>A string with the information of the exception</returns>
    public string GetExceptionInfo(bool withStackTrace = false, bool withInnerExceptionMessage = false) {
      StringBuilder sb = new();
      sb.AppendLine($"Exception type...: {ex.GetType().GetNameEx()}");
      sb.AppendLine($"Exception source.: {ex.Source}");
      sb.AppendLine($"Message..........: {ex.Message}");
      sb.AppendLine($"TargetSite.......: {ex.TargetSite?.ToString().OrNull()}");
      if (withStackTrace) {
        sb.AppendLine(ex.StackTrace ?? "".Box("Stack trace"));
      }
      if (withInnerExceptionMessage) {
        if (ex.InnerException is not null) {
          sb.AppendLine($"Inner exception");
          sb.AppendLine(ex.InnerException.GetExceptionInfo(withStackTrace, withInnerExceptionMessage));
        }
      }
      return sb.ToString();
    }
  }
}
