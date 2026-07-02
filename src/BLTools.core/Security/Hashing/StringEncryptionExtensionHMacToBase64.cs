using System.Security.Cryptography;

namespace BLTools.Core.Encryption;

public static partial class StringEncryptionExtension {

  /// <summary>
  /// Calculate the HMAC for the source string and convert the result to base64
  /// </summary>
  /// <param name="source">The source string (UTF8)</param>
  /// <param name="key">The key used to calculate HMAC (UTF8)</param>
  /// <param name="hashMethod">The hash method to use</param>
  /// <returns>The base64 version of the HMAC</returns>
  public static string HMacToBase64(this string source, string key, EHashingMethods hashMethod = EHashingMethods.SHA256) {
    return source.HMacToBase64(Encoding.UTF8.GetBytes(key), hashMethod);
  }

  /// <summary>
  /// Calculate the HMACX for the source string and convert the result to base64
  /// </summary>
  /// <param name="source">The source string (UTF8)</param>
  /// <param name="key">The key used to calculate HMAC expressed in bytes</param>
  /// <param name="hashMethod">The hash method to use</param>
  /// <returns>The base64 version of the HMAC</returns>
  public static string HMacToBase64(this string source, byte[] key, EHashingMethods hashMethod = EHashingMethods.SHA256) {

    byte[] SourceData = Encoding.UTF8.GetBytes(source);
    byte[] HashedData;

    switch (hashMethod) {

      case EHashingMethods.MD5:
        using (HMACMD5 HMACMD5Hasher = new HMACMD5(key)) {
          HashedData = HMACMD5Hasher.ComputeHash(SourceData);
        }
        break;

      case EHashingMethods.SHA1:
        using (HMACSHA1 HMACSHA1Hasher = new HMACSHA1(key)) {
          HashedData = HMACSHA1Hasher.ComputeHash(SourceData);
        }
        break;

      case EHashingMethods.SHA256:
        using (HMACSHA256 HMACSHA256Hasher = new HMACSHA256(key)) {
          HashedData = HMACSHA256Hasher.ComputeHash(SourceData);
        }
        break;

      case EHashingMethods.SHA384:
        using (HMACSHA384 HMACSHA384Hasher = new HMACSHA384(key)) {
          HashedData = HMACSHA384Hasher.ComputeHash(SourceData);
        }
        break;

      case EHashingMethods.SHA512:
        using (HMACSHA512 HMACSHA512Hasher = new HMACSHA512(key)) {
          HashedData = HMACSHA512Hasher.ComputeHash(SourceData);
        }
        break;

      default:
        return "";
    }

    return Convert.ToBase64String(HashedData, Base64FormattingOptions.InsertLineBreaks);

  }

  /// <summary>
  /// Verify if a HMAC matches given a source string and a key
  /// </summary>
  /// <param name="source">The source string (UTF8)</param>
  /// <param name="key">The key used to calculate HMAC (UTF8)</param>
  /// <param name="base64Hash">The base64 hash to verify</param>
  /// <param name="hashMethod">The hash method to use</param>
  /// <returns></returns>
  public static bool VerifyHMACFromBase64(this string source, string key, string base64Hash, EHashingMethods hashMethod = EHashingMethods.SHA256) {
    string HashToTest = source.HMacToBase64(key, hashMethod);
    return HashToTest == base64Hash;
  }

  /// <summary>
  /// Verify if a HMAC matches given a source string and a key
  /// </summary>
  /// <param name="source">The source string (UTF8)</param>
  /// <param name="key">The key used to calculate HMAC expressed in bytes</param>
  /// <param name="base64Hash">The base64 hash to verify</param>
  /// <param name="hashMethod">The hash method to use</param>
  /// <returns></returns>
  public static bool VerifyHMACFromBase64(this string source, byte[] key, string base64Hash, EHashingMethods hashMethod = EHashingMethods.SHA256) {
    string HashToTest = source.HMacToBase64(key, hashMethod);
    return HashToTest == base64Hash;
  }


}



