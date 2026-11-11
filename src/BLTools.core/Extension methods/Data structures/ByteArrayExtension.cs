namespace BLTools.Core;

/// <summary>
/// Extension methods for arrays
/// </summary>
public static class ByteArrayExtensions {
  extension(IEnumerable<byte> rawData) {

    /// <summary>
    /// Convert an array of byte into a string of hex values, each separated by a space (ex. "A6 34 F2")
    /// </summary>
    /// <param name="rawData">The byte array</param>
    /// <param name="separator">An optional separator</param>
    /// <returns>The string of hex values</returns>
    public string ToHexString(string separator = " ") {

      if (rawData is null || rawData.IsEmpty()) {
        return "";
      }

      return string.Join(separator, rawData.Select(d => d.ToString("X2")));
    }
  }
  extension(byte[] rawData) {

    /// <summary>
    /// Convert an array of bytes to a string of chars
    /// </summary>
    /// <param name="rawData">The array to convert</param>
    /// <returns></returns>
    public string ToCharString() {
      if (rawData.IsEmpty()) {
        return "";
      }

      return new string(rawData.Select(d => (char)d).ToArray());
    }
  }
}
