namespace BLTools.Core.Logging;

[Flags]
public enum ELogErrorOptions {
  None = 0,
  WithStackTrace = 1,
  WithInnerException = 2
}
