namespace BLTools.Core.Logging;

/// <summary>
/// Avoid the dump of the item
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Field | AttributeTargets.Property)]
public class DoNotDumpAttribute : Attribute {

}
