namespace BLTools.Core.Encryption;

/// <summary>
/// Abstract implementation of a RSA key (public or private)
/// </summary>
public abstract class ARsaKey : IEquatable<ARsaKey>, ILoggable {

  /// <summary>
  /// Thename of the Rsa Key
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Parameters for the key
  /// </summary>
  public byte[]? Key { get; set; }

  public abstract ILogger Logger { get; set; }

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// Create new empty RSA key
  /// </summary>
  protected ARsaKey() { }

  /// <summary>
  /// Create a new named RSA key
  /// </summary>
  /// <param name="name">The name of the key</param>
  protected ARsaKey(string name) {
    Name = name;
  }

  /// <summary>
  /// Create a new named RSA key with parameters
  /// </summary>
  /// <param name="name">The name of the key</param>
  /// <param name="key">The key parameters</param>
  protected ARsaKey(string name, byte[] key) {
    Name = name;
    Key = key;
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  #region --- Converters -------------------------------------------------------------------------------------
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"- {nameof(Name)} = {Name.WithQuotes()}");
    RetVal.AppendLine($"- {nameof(Key)} = {Key?.ToHexString().OrNull()}");
    return RetVal.ToString();
  } 
  #endregion --- Converters ----------------------------------------------------------------------------------


  /// <inheritdoc/>
  public bool Equals(ARsaKey? other) {
    if (other is null) {
      return false;
    }

    if (Object.ReferenceEquals(this, other)) {
      return true;
    }

    if (Name != other.Name) {
      return false;
    }

    if (Key is null && other.Key is null) {
      return true;
    }
    if (Key is null) {
      return false;
    }
    if (other.Key is null) {
      return false;
    }

    if (Key.SequenceEqual(other.Key)) {
      return true;
    }

    return true;
  }

  public override bool Equals(object? obj) {
    return base.Equals(obj as ARsaKey);
  }

  public override int GetHashCode() {
    return (Key ?? []).GetHashCode() + Name.GetHashCode();
  }
}
