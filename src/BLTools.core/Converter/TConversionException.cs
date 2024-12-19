namespace BLTools.Core;

public class TConversionException : Exception {

  public Type? SourceType { get; set; }
  public Type? TargetType { get; set; }

  public TConversionException(string message) : base(message) { }
  public TConversionException(string message, Exception innerException) : base(message, innerException) { }
}
