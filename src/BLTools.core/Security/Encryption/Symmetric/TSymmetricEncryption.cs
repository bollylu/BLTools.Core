using System.Security.Cryptography;

namespace BLTools.Core.Encryption;

/// <summary>
/// Encrypt data with symmetric algorithms
/// </summary>
public static class TSymmetricEncryption {

  /// <summary>
  /// Encrypt a source string using a password and an algorithm
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="password">The password</param>
  /// <param name="encryptionAlgorithm">The algorithm to use</param>
  /// <param name="keyLength">Key length argument for the algorithm</param>
  /// <returns>The encrypted string</returns>
  public static string EncryptToBase64(this string source, string password, ESymmetricEncryptionAlgorithm encryptionAlgorithm = ESymmetricEncryptionAlgorithm.AES, int keyLength = 256) {
    return source.EncryptToBase64(password, Encoding.UTF8, encryptionAlgorithm, keyLength);
  }

  /// <summary>
  /// Encrypt a source string using a password and an algorithm
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="password">The password</param>
  /// <param name="encoding">The encoding of the source</param>
  /// <param name="encryptionAlgorithm">The algorithm to use</param>
  /// <param name="keyLength">Key length argument for the algorithm</param>
  /// <returns>The encrypted string</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ApplicationException"></exception>
  public static string EncryptToBase64(this string source, string password, Encoding encoding, ESymmetricEncryptionAlgorithm encryptionAlgorithm = ESymmetricEncryptionAlgorithm.AES, int keyLength = 256) {
    return Convert.ToBase64String(source.EncryptToBytes(password, encoding, encryptionAlgorithm, keyLength));
  }

  /// <summary>
  /// Encrypt a source string using a password and an algorithm
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="password">The password</param>
  /// <param name="encoding">The encoding of the source</param>
  /// <param name="encryptionAlgorithm">The algorithm to use</param>
  /// <param name="keyLength">Key length argument for the algorithm</param>
  /// <returns>The encrypted string as a byte array</returns>
  /// <exception cref="ArgumentNullException"></exception>
  /// <exception cref="ApplicationException"></exception>
  public static byte[] EncryptToBytes(this string source, string password, Encoding encoding, ESymmetricEncryptionAlgorithm encryptionAlgorithm = ESymmetricEncryptionAlgorithm.AES, int keyLength = 256) {
    #region Validate parameters
    if (password.Length == 0) {
      return Encoding.UTF8.GetBytes(source);
    }
    TSymmetricUtils.CheckKeyLength(keyLength, encryptionAlgorithm);
    #endregion Validate parameters

    byte[] SourceBytes = encoding.GetBytes(source);

    SymmetricAlgorithm Algo = encryptionAlgorithm switch {
      ESymmetricEncryptionAlgorithm.AES => Aes.Create(),
      ESymmetricEncryptionAlgorithm.TripleDES => TripleDES.Create(),
      _ => throw new ApplicationException("Invalid encryption algorithm")
    };

    Algo.KeySize = keyLength;
    TPIV PIV = TPIV.Generate(password, Algo);

    Algo.Key = PIV.Password;
    Algo.IV = PIV.IV;
    Algo.Mode = CipherMode.CBC;
    Algo.Padding = PaddingMode.PKCS7;

    try {
      using (ICryptoTransform xfrm = Algo.CreateEncryptor()) {
        using (MemoryStream Output = new MemoryStream()) {
          using (CryptoStream CStream = new CryptoStream(Output, xfrm, CryptoStreamMode.Write)) {
            CStream.Write(SourceBytes, 0, SourceBytes.Length);
            CStream.Flush();
            CStream.FlushFinalBlock();
          }
          return Output.ToArray();
        }
      }
    } catch (Exception ex) {
      throw new ApplicationException("Unable to encrypt", ex);
    }

  }




}

