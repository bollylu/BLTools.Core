namespace BLTools.Core;

/// <summary>
/// Single element of arguments : Id(position), Name, Value
/// </summary>
public class ArgElement : IArgElement, IEquatable<ArgElement> {

  #region --- Public properties ------------------------------------------------------------------------------
  /// <summary>
  /// The position within the command line
  /// </summary>
  public int Id { get; private set; }
  /// <summary>
  /// The key name
  /// </summary>
  public string Name { get; private set; }
  /// <summary>
  /// The value (stored as a string)
  /// </summary>
  public string Value { get; private set; }
  #endregion --- Public properties ---------------------------------------------------------------------------

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// Builds a key/pair value with an ID
  /// </summary>
  /// <param name="id"></param>
  /// <param name="name"></param>
  /// <param name="value"></param>
  public ArgElement(int id, string name, string value) {
    Id = id;
    Name = name;
    Value = value;
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public string ToString(int indent) {
    StringBuilder RetVal = new StringBuilder();
    RetVal.Append($"{' '.Repeat(indent)}[{Id}] {Name.WithQuotes()}");
    if (!string.IsNullOrEmpty(Value)) {
      RetVal.Append($" = {Value.WithQuotes()}");
    }
    return RetVal.ToString();
  }

  /// <inheritdoc/>
  public override string ToString() {
    return ToString(0);
  }

  /// <inheritdoc/>
  public bool HasValue() {
    return Value != null;
  }

  /// <inheritdoc/>
  public override bool Equals(object? obj) {
    if (obj is not ArgElement other) {
      return false;
    }

    return Name == other.Name && Value == other.Value;
  }

  /// <inheritdoc/>
  public override int GetHashCode() {
    return Id.GetHashCode() | Name.GetHashCode() | Value.GetHashCode();
  }

  /// <summary>
  /// Compare two IArgElements using StringComparison.CurrentCulture
  /// </summary>
  /// <param name="other">The other IArgElement to compare with</param>
  /// <returns>true if same, false otherwise</returns>
  public bool Equals(IArgElement? other) {
    return Equals(other, StringComparison.CurrentCulture);
  }

  /// <summary>
  /// Compare two IArgElements
  /// </summary>
  /// <param name="other">The other IArgElement to compare with</param>
  /// <param name="comparison">How to compare the strings</param>
  /// <returns>true if same, false otherwise</returns>
  public bool Equals(IArgElement? other, StringComparison comparison = StringComparison.CurrentCultureIgnoreCase) {
    return other switch {
      null => false,
      _ => Name.Equals(other.Name, comparison) && Value.Equals(other.Value, comparison)
    };
  }

  public bool Equals(ArgElement? other) {
    return other switch {
      null => false,
      _ => Name == other.Name && Value == other.Value
    };
  }
}
