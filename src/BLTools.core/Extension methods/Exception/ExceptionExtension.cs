namespace BLTools.Core;
public static class ExceptionExtension {

  public static string GetExceptionInfo(this Exception ex, bool withStackTrace = false, bool withInnerExceptionMessage = false) {
    StringBuilder sb = new();
    sb.AppendLine($"Exception type...: {ex.GetType().GetNameEx()}");
    sb.AppendLine($"Exception source.: {ex.Source}");
    sb.AppendLine($"Message..........: {ex.Message}");
    sb.AppendLine($"TargetSite.......: {ex.TargetSite?.ToString() ?? ILogger.VALUE_NULL}");
    if (withStackTrace) {
      sb.AppendLine(ex.StackTrace ?? "".Box("Stack trace"));
    }
    if (withInnerExceptionMessage) {
      if (ex.InnerException is not null) {
        sb.AppendLine($"Inner exception.: {ex.InnerException.Message}");
      }
    }
    return sb.ToString();
  }
}
