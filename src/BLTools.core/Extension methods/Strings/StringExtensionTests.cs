using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace BLTools.Core;

/// <summary>
/// Extensions for string
/// </summary>
  [SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "", Scope = "class")]
  public static partial class StringExtension {

  #region Tests
  /// <summary>
  /// Indicates if a string contains only alphabetic characters (A-Z and a-z)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlpha(this string sourceValue) {
    return Regex.IsMatch(sourceValue, @"^[A-Za-z]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or numeric characters (0-9)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaNumeric(this string sourceValue) {
    return Regex.IsMatch(sourceValue, "^[A-Za-z0-9]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only numeric characters (0-9) or separators (-.,)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsNumeric(this string sourceValue) {
    return Regex.IsMatch(sourceValue, @"^[-\d][\d\.,]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only numeric characters (0-9) or separators (-.,)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsNumericOnly(this string sourceValue) {
    return Regex.IsMatch(sourceValue, @"^[-\d][\d]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or blank
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaOrBlank(this string sourceValue) {
    return Regex.IsMatch(sourceValue.Trim(), @"^[A-Za-z ]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or numeric characters (0-9) or blank
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaNumericOrBlank(this string sourceValue) {
    return Regex.IsMatch(sourceValue.Trim(), @"^[A-Za-z\d ]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only numeric characters (0-9) or blank
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsNumericOrBlank(this string sourceValue) {
    return Regex.IsMatch(sourceValue.Trim(), @"^[-\d][\d\., ]*$", RegexOptions.Compiled);
  }

  /// <summary>
  /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or numeric characters (0-9) or blank or dashes
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaNumericOrBlankOrDashes(this string sourceValue) {
    return Regex.IsMatch(sourceValue.Trim(), @"^[A-Za-z\d -]*$", RegexOptions.Compiled);
  }
  /// <summary>
  /// Indicates if a string contains only specified characters
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <param name="charList">The list of characters to test for</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsMadeOfTheseChars(this string sourceValue, params char[] charList) {
    return Regex.IsMatch(sourceValue.Trim(), $@"^[{string.Join("", charList)}]*$", RegexOptions.Compiled);
  }
  #endregion Tests

}
