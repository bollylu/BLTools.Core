using System.Security.Cryptography;

namespace BLTools.Core.Encryption;

/// <summary>
/// Decrypt data with symmetric algorithms
/// </summary>
public static class TSymmetricDecryption {

  /// <summary>
  /// Decrypt a base64 string using a password and an algorithm
  /// </summary>
  /// <param name="source">The base64 source</param>
  /// <param name="password">The password to use</param>
  /// <param name="encryptionAlgorithm">The algorithm to use</param>
  /// <param name="keyLength">Key length argument for the algorithm</param>
  /// <returns>The decrypted string</returns>
  public static string DecryptFromBase64(this string source, string password, ESymmetricEncryptionAlgorithm encryptionAlgorithm = ESymmetricEncryptionAlgorithm.AES, int keyLength = 256) {
    return source.DecryptFromBase64(password, Encoding.UTF8, encryptionAlgorithm, keyLength);
  }

  /// <summary>
  /// Decrypt a base64 string using a password and an algorithm
  /// </summary>
  /// <param name="source">The base64 source</param>
  /// <param name="password">The password to use</param>
  /// <param name="encoding">The encoding of the decrypted string</param>
  /// <param name="encryptionAlgorithm">The algorithm to use</param>
  /// <param name="keyLength">Key length argument for the algorithm</param>
  /// <returns>The decrypted string</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static string DecryptFromBase64(this string source, string password, Encoding encoding, ESymmetricEncryptionAlgorithm encryptionAlgorithm = ESymmetricEncryptionAlgorithm.AES, int keyLength = 256) {
    return encoding.GetString(source.DecryptToBytesFromBase64(password, encoding, encryptionAlgorithm, keyLength));
  }

  /// <summary>
  /// Decrypt a base64 string using a password and an algorithm
  /// </summary>
  /// <param name="source">The base64 source</param>
  /// <param name="password">The password to use</param>
  /// <param name="encoding">The encoding of the decrypted string</param>
  /// <param name="encryptionAlgorithm">The algorithm to use</param>
  /// <param name="keyLength">Key length argument for the algorithm</param>
  /// <returns>The decrypted string as a byte array</returns>
  /// <exception cref="ArgumentNullException"></exception>
  public static byte[] DecryptToBytesFromBase64(this string source, string password, Encoding encoding, ESymmetricEncryptionAlgorithm encryptionAlgorithm = ESymmetricEncryptionAlgorithm.AES, int keyLength = 256) {
    #region Validate parameters
    if (password.Length == 0) {
      return Convert.FromBase64String(source);
    }
    TSymmetricUtils.CheckKeyLength(keyLength, encryptionAlgorithm);
    #endregion Validate parameters

    byte[] SourceBytes = Convert.FromBase64String(source);

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
      using (ICryptoTransform xfrm = Algo.CreateDecryptor()) {
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
      throw new ApplicationException("Unable to decrypt", ex);
    }
  }

}
