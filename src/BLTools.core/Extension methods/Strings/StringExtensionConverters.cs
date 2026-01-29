using System.Runtime.InteropServices;
using System.Security;

namespace BLTools.Core;

/// <summary>
/// Extensions for string
/// </summary>
public static partial class StringExtensions {

  #region Converters


  /// <summary>
  /// Convert a string to an array of bytes
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <returns>The array of bytes</returns>
  public static byte[] ToByteArray(this string sourceString) {
    return [.. sourceString.Select<char, byte>(c => (byte)c)];
  }

  /// <summary>
  /// Convert a string to an array of bytes
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="cleanupSource">When true, the source string will be preprocessed by removing any space, colon, dash, semi-colon and comma</param>
  /// <returns>The array of bytes</returns>
  public static byte[] ToByteArrayFromHex(this string sourceString, bool cleanupSource = true) {
    #region Validate parameters
    if (sourceString.IsEmpty()) {
      return [];

    }

    string ProcessedSource;

    if (cleanupSource) {
      ProcessedSource = sourceString.Replace(" ", "")
                                    .Replace(":", "")
                                    .Replace("-", "")
                                    .Replace(";", "")
                                    .Replace(",", "");
    } else {
      ProcessedSource = sourceString;
    }

    if ((ProcessedSource.Length % 2) != 0) {
      throw new FormatException($"Length of source string is not valid for conversion : {ProcessedSource.Length}");
    }
    #endregion Validate parameters

    try {
      using (MemoryStream RetVal = new MemoryStream()) {
        for (int i = 0; i < ProcessedSource.Length; i += 2) {
          string HexByte = ProcessedSource.Substring(i, 2);
          byte ConvertedValue = byte.Parse(HexByte, NumberStyles.HexNumber);
          RetVal.WriteByte(ConvertedValue);
        }
        RetVal.Flush();
        return RetVal.ToArray();
      }
    } catch (Exception ex) {
      throw new FormatException($"Invalid data for conversion : {ProcessedSource}", ex);
    }

  }
  #endregion Converters

}
