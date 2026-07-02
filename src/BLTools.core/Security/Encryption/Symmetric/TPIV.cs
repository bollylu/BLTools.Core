using System.Security.Cryptography;

namespace BLTools.Core.Encryption;

/// <summary>
/// Initialization parameter for encryption algorithms
/// </summary>
public class TPIV {
  
  /// <summary>
  /// The password
  /// </summary>
  public byte[] Password { get; set; }

  /// <summary>
  /// The initialization vector
  /// </summary>
  public byte[] IV { get; set; }

  /// <summary>
  /// The size of the salt for generation of the IV
  /// </summary>
  public static byte SaltSize { get; set; } = 8;

  /// <summary>
  /// The size of the salt for generation of the IV
  /// </summary>
  public static long Salt { get; set; } = 65483197;

  #region Constructor(s)
  /// <summary>
  /// Generate a TPIV
  /// </summary>
  /// <param name="password">The password</param>
  /// <param name="iv">The IV</param>
  public TPIV(byte[] password, byte[] iv) {
    Password = password;
    IV = iv;
  }

  /// <summary>
  /// Generate a TPIV using password
  /// </summary>
  /// <param name="password">The password</param>
  /// <param name="iv">The IV</param>
  /// <exception cref="ArgumentNullException"></exception>
  public TPIV(string password, byte[] iv) {
    Password = Encoding.UTF8.GetBytes(password);
    IV = iv;
  }

  /// <summary>
  /// Generate a TPIV
  /// </summary>
  /// <param name="password">The password to use</param>
  /// <param name="iv">The IV</param>
  /// <param name="encoding">The encoding of the password</param>
  /// <exception cref="ArgumentNullException"></exception>
  public TPIV(string password, byte[] iv, Encoding encoding) {
    Password = encoding.GetBytes(password);
    IV = iv;
  }

  /// <summary>
  /// Generate a TPIV using UTF8
  /// </summary>
  /// <param name="password">The password to use</param>
  /// <param name="algo"></param>
  /// <returns></returns>
  public static TPIV Generate(string password, SymmetricAlgorithm algo) {
    if (algo.BlockSize.IsOutsideRange(algo.LegalBlockSizes.First().MinSize, algo.LegalBlockSizes.Last().MaxSize)) {
      algo.BlockSize = algo.LegalBlockSizes.First().MinSize;
    }

    if (algo.KeySize.IsOutsideRange(algo.LegalKeySizes.First().MinSize, algo.LegalKeySizes.Last().MaxSize)) {
      algo.KeySize = algo.LegalKeySizes.First().MinSize;
    }

    int IVSize = algo.BlockSize / 8;
    int KeySize = algo.KeySize / 8;
    
    byte[] saltBytes = BitConverter.GetBytes(Salt);
    byte[] derived = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 100_000, HashAlgorithmName.SHA256, KeySize + IVSize);

    return new TPIV(derived[..KeySize], derived[KeySize..]);
  }
  #endregion Constructor(s)
}
