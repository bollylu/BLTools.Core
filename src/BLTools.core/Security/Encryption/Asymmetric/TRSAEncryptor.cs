using System.Security.Cryptography;

namespace BLTools.Core.Encryption;

public class TRSAEncryptor {

  public ILogger Logger { get; set; } = new TTraceLogger<TRSAEncryptor>();

  #region Encrypt
  internal static string? EncryptToBase64(string source, TRSAPublicKey publicKey, Encoding encoding) {
    #region Validate parameters
    if (source is null) {
      Logger.LogError("Error: Unable to encrypt a null string");
      throw new ArgumentException("Unable to encrypt a null string", nameof(source));
    }
    #endregion Validate parameters

    try {
      using (RSACryptoServiceProvider RSACSP = new RSACryptoServiceProvider()) {
        RSACSP.ImportRSAPublicKey(publicKey.Key, out int Bytes);
        byte[] EncryptedData = RSACSP.Encrypt(encoding.GetBytes(source), true);
        return Convert.ToBase64String(EncryptedData);
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Error: unable to encrypt data", ex);
      return null;
    }
  }
  #endregion Encrypt

  #region Decrypt
  internal static string? DecryptFromBase64(string base64Source, TRSAPrivateKey privateKey, Encoding encoding) {
    #region Validate parameters
    if (base64Source == null) {
      Logger.LogError("Error: Unable to decrypt a null string");
      throw new ArgumentException("Unable to encrypt a null string", nameof(base64Source));
    }
    #endregion Validate parameters

    try {
      using (RSACryptoServiceProvider RSACSP = new RSACryptoServiceProvider()) {
        RSACSP.ImportRSAPrivateKey(privateKey.Key, out int Bytes);
        byte[] DecryptedData = RSACSP.Decrypt(Convert.FromBase64String(base64Source), true);
        return encoding.GetString(DecryptedData);
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Error: unable to decrypt data", ex);
      return null;
    }
  }
  #endregion Decrypt

  #region Sign
  internal static string? SignToBase64(string source, TRSAPrivateKey privateKey, Encoding encoding) {
    #region Validate parameters
    if (source is null) {
      Logger.LogError("Error: Unable to sign a null string");
      throw new ArgumentException("Unable to sign a null string", nameof(source));
    }
    #endregion Validate parameters

    try {
      using (RSACryptoServiceProvider RSACSP = new RSACryptoServiceProvider()) {
        RSACSP.ImportRSAPrivateKey(privateKey.Key, out int Bytes);
        byte[] Signature = RSACSP.SignData(encoding.GetBytes(source), SHA256.Create());
        return Convert.ToBase64String(Signature);
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Error: unable to sign data", ex);
      return null;
    }
  }
  #endregion Sign

  #region Validate signature

  internal static bool IsSignatureBase64Valid(string source, string base64Signature, TRSAPublicKey publicKey, Encoding encoding) {

    #region Validate parameters
    if (source == null) {
      string Msg = "Error: Unable to validate the signature of a null string";
      Trace.WriteLine(Msg);
      throw new ArgumentException(Msg, "source");
    }

    if (string.IsNullOrEmpty(base64Signature)) {
      string Msg = "Unable to validate an empty or null signature";
      Trace.WriteLine(Msg);
      throw new ArgumentException(Msg, "base64Signature");
    }
    #endregion Validate parameters

    try {
      using (RSACryptoServiceProvider RSACSP = new RSACryptoServiceProvider()) {
        RSACSP.ImportRSAPublicKey(publicKey.Key, out int Bytes);
        return RSACSP.VerifyData(encoding.GetBytes(source), SHA256.Create(), Convert.FromBase64String(base64Signature));
      }
    } catch (Exception ex) {
      Logger.LogErrorBox("Error: unable to validate signature", ex);
      return false;
    }
  }
  #endregion Validate signature
}
