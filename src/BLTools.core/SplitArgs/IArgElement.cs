namespace BLTools.Core;

/// <summary>
/// Interface for an element with ID, name, value as string
/// </summary>
public interface IArgElement : IEquatable<IArgElement> {

  #region --- Public properties ------------------------------------------------------------------------------
  /// <summary>
  /// The position within the command line
  /// </summary>
  int Id { get; }
  /// <summary>
  /// The key name
  /// </summary>
  string Name { get; }
  /// <summary>
  /// The value (stored as a string)
  /// </summary>
  string Value { get; }
  #endregion --- Public properties ---------------------------------------------------------------------------

  /// <summary>
  /// Test if the value exists
  /// </summary>
  /// <returns>true if the value is not null, false otherwise</returns>
  bool HasValue();

  /// <summary>
  /// Get a ToString indented
  /// </summary>
  /// <param name="indent">The indent width</param>
  /// <returns></returns>
  string ToString(int indent);
}
