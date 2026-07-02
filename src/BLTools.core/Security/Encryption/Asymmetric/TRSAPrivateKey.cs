namespace BLTools.Core.Encryption;

/// <summary>
/// Implementation of a RSA private key
/// </summary>
public class TRSAPrivateKey : ARsaKey {

  /// <inheritdoc/>
  public override ILogger Logger { get; set; } = new TConsoleLogger();

  #region Constructor(s)
  /// <summary>
  /// Create a new empty private key
  /// </summary>
  public TRSAPrivateKey() : base() {
  }

  /// <summary>
  /// Create a new named private key container
  /// </summary>
  /// <param name="name">The name of the private key</param>
  /// <param name="storagePath">The path to store the key</param>
  public TRSAPrivateKey(string name, string storagePath = "") : base(name) {
    StoragePath = storagePath;
    Name = $"{Name}-pvt.blkey";
  }

  /// <summary>
  /// Create a new named private key with parameters
  /// </summary>
  /// <param name="name">The name of the key</param>
  /// <param name="key">The parameters for the key</param>
  /// <param name="storagePath">The path to store the key</param>
  public TRSAPrivateKey(string name, byte[] key, string storagePath = "") : base(name, key) {
    StoragePath = storagePath;
    Name = $"{Name}-pvt.blkey";
  }
  #endregion Constructor(s)

  #region Converters
  /// <inheritdoc/>
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine("=== Private key ========================================================");
    RetVal.AppendLine($"- {nameof(Name)} = {Name.WithQuotes()}");
    RetVal.AppendLine($"- {nameof(StoragePath)} = {StoragePath.WithQuotes()}");
    RetVal.AppendLine($"- Components");
    RetVal.AppendIndent(base.ToString(), 2);
    RetVal.AppendLine("========================================================================");
    return RetVal.ToString();
  }

  #endregion Converters

  #region Public methods
  /// <inheritdoc/>
  public override bool Save(bool overwrite = true) {
    Root = ToXml();

    try {
      base.Save(overwrite);
      return true;
    } catch (Exception ex) {
      Logger.LogErrorBox("Error while saving private key", ex);
      return false;
    }
  }

  ///// <inheritdoc/>
  //public override bool Save(string filename, bool overwrite = true) {
  //  Root = ToXml();
  //  try {
  //    base.Save(filename);
  //    return true;
  //  } catch (Exception ex) {
  //    Trace.WriteLine($"Error while saving private key : {ex.Message}", Severity.Error);
  //    return false;
  //  }
  //}

  /// <inheritdoc/>
  public override XElement Load() {

    try {
      Root = base.Load(FullFilename);
      FromXml(Root);
      return Root;
    } catch (Exception ex) {
      Logger.LogErrorBox("Error while loading private key", ex);
      return new XElement(XML_ROOT_ELEMENT);
    }
  }

  /// <inheritdoc/>
  public override XElement Load(string fullFilename) {
    Name = Path.GetFileName(fullFilename);
    StoragePath = Path.GetDirectoryName(fullFilename) ?? "";

    try {
      Root = base.Load(FullFilename);
      FromXml(Root);
      return Root;
    } catch (Exception ex) {
      Logger.LogErrorBox($"Error while loading private key {Name.WithQuotes()}", ex);
      return new XElement(XML_ROOT_ELEMENT);
    }
  }
  #endregion Public methods

  #region Public static methods
  /// <summary>
  /// Create a new private key by reading its content from file
  /// </summary>
  /// <param name="fullFilename">The full filename of the key</param>
  /// <returns>The name of the key</returns>
  public static TRSAPrivateKey LoadFrom(string fullFilename) {
    string Name = Path.GetFileName(fullFilename);
    string StoragePath = Path.GetDirectoryName(fullFilename) ?? "";

    TRSAPrivateKey RetVal = new TRSAPrivateKey(Name, StoragePath);
    _ = RetVal.Load();
    return RetVal;
  }
  #endregion Public static methods
}

