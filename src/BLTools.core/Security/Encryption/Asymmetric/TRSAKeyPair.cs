using System.Security.Cryptography;

using BLTools.Storage.Xml;

namespace BLTools.Encryption;

/// <summary>
/// A RSA key pair
/// </summary>
public class TRSAKeyPair : IToXml, ILoggable {

  /// <inheritdoc/>
  public ILogger Logger { get; set; } = new TConsoleLogger<TRSAKeyPair>();

  /// <summary>
  /// The name ofthis element in XML
  /// </summary>
  public const string XML_THIS_ELEMENT = "RsaKeyPair";

  #region Public properties
  /// <summary>
  /// A name for the key pair
  /// </summary>
  public string Name { get; set; }

  /// <summary>
  /// The path to store the keys
  /// </summary>
  public string StoragePath { get; init; } = "";

  /// <summary>
  /// The length of the key
  /// </summary>
  public int KeyLength { get; set; }

  /// <summary>
  /// The RSA key
  /// </summary>
  public RSA RSAKey {
    get {
      if (_RSAKey is null) {
        RSA Rsa = RSA.Create();
        Rsa.KeySize = KeyLength;
        _RSAKey = Rsa;
      }
      return _RSAKey;

    }
    set {
      _RSAKey = value;
      _PrivateKey = null;
      _PublicKey = null;
    }
  }
  private RSA? _RSAKey;

  /// <summary>
  /// The private key component
  /// </summary>
  public TRSAPrivateKey PrivateKey {
    get {
      return _PrivateKey ??= new TRSAPrivateKey(Name, StoragePath) { Key = RSAKey.ExportRSAPrivateKey() };
    }
    private set {
      _PrivateKey = value;
    }
  }
  private TRSAPrivateKey? _PrivateKey;

  /// <summary>
  /// The public key component
  /// </summary>
  public TRSAPublicKey PublicKey {
    get {
      return _PublicKey ??= new TRSAPublicKey(Name, StoragePath) { Key = RSAKey.ExportRSAPublicKey() };
    }
    private set {
      _PublicKey = value;
    }
  }
  private TRSAPublicKey? _PublicKey;
  #endregion Public properties

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new unnamed key pair
  /// </summary>
  public TRSAKeyPair() {
    Name = "";
    KeyLength = 1024;
  }

  /// <summary>
  /// A new named key pair
  /// </summary>
  /// <param name="keyname">The name of the key</param>
  /// <param name="keyLength">The length of the key</param>
  public TRSAKeyPair(string keyname, int keyLength = 1024) {
    Name = keyname;
    KeyLength = keyLength;
  }

  /// <summary>
  /// A new named key pair
  /// </summary>
  /// <param name="keyname">The name of the key</param>
  /// <param name="pathname">The path to store the keys</param>
  /// <param name="keyLength">The length of the key</param>
  public TRSAKeyPair(string keyname, string pathname, int keyLength = 1024) {
    Name = keyname;
    StoragePath = pathname;
    KeyLength = keyLength;
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------


  #region --- Converters -------------------------------------------------------------------------------------
  /// <inheritdoc/>
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"- {nameof(Name)} = {Name}");
    RetVal.AppendLine($"- {nameof(StoragePath)} = {StoragePath}");
    RetVal.AppendLine($"- {nameof(KeyLength)} = {KeyLength}");
    RetVal.AppendLine($"- {nameof(RSAKey)} = {RSAKey}");
    RetVal.AppendIndent(PublicKey.ToString(), 2);
    RetVal.AppendIndent(PrivateKey.ToString(), 2);
    return RetVal.ToString();
  }
  #endregion --- Converters -------------------------------------------------------------------------------------
  /// <summary>
  /// Save both private and public keys into separate files
  /// </summary>
  public void Save() {
    try {
      if (!Directory.Exists(StoragePath)) {
        Directory.CreateDirectory(StoragePath);
      }
    } catch (Exception ex) {
      Logger.LogErrorBox($"Unable to create directory {StoragePath.WithQuotes()}", ex);
      return;
    }
    PrivateKey.Save();
    PublicKey.Save();
  }
  /// <summary>
  /// Load both private and public keys from separate files
  /// </summary>
  public void Load() {
    PublicKey.Load();
    PrivateKey.Load();
  }

  /// <summary>
  /// Create a TRsaKeyPair by reading its value in a file
  /// </summary>
  /// <param name="keyname">The name of the key</param>
  /// <param name="pathname">The path of the key</param>
  /// <returns>A new TRSAKeyPair</returns>
  public static TRSAKeyPair LoadFrom(string keyname, string pathname) {
    TRSAKeyPair RetVal = new TRSAKeyPair(keyname, pathname);
    RetVal.Load();
    return RetVal;
  }

  #region --- IToXml --------------------------------------------
  /// <summary>
  /// Convert a RsaKeyPair into an XElement
  /// </summary>
  /// <returns></returns>
  public XElement ToXml() {
    XElement RetVal = new XElement(XML_THIS_ELEMENT);
    RetVal.Add(PrivateKey.ToXml());
    RetVal.Add(PublicKey.ToXml());
    return RetVal;
  }
  /// <summary>
  /// Fill in the properties from an XElement source
  /// </summary>
  /// <param name="source">The XElement source</param>
  public void FromXml(XElement source) {
    if (source is null) {
      return;
    }

    PrivateKey.FromXml(source.SafeReadElement(TRSAPrivateKey.XML_THIS_ELEMENT));
    PublicKey.FromXml(source.SafeReadElement(TRSAPublicKey.XML_THIS_ELEMENT));

  }
  #endregion --- IToXml --------------------------------------------
}
