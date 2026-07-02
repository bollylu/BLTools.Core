namespace BLTools.Core.Encryption;

internal static class TSymmetricUtils {

  private static readonly Dictionary<ESymmetricEncryptionAlgorithm, int[]> AllowedKeyLengths = new Dictionary<ESymmetricEncryptionAlgorithm, int[]>() {
      {ESymmetricEncryptionAlgorithm.AES, new int[] {128, 192, 256}}
    , {ESymmetricEncryptionAlgorithm.TripleDES, new int[] {128, 192}}
  };

  internal static void CheckKeyLength(int keyLength, ESymmetricEncryptionAlgorithm encryptionMethod) {
    if (!AllowedKeyLengths[encryptionMethod].Contains(keyLength)) {
      string Msg = $"Only {string.Join(",", AllowedKeyLengths[encryptionMethod])} bits key length allowed for {encryptionMethod} : {keyLength}";
      Trace.WriteLine(Msg);
      throw new ArgumentException(Msg);
    }
  }

}
