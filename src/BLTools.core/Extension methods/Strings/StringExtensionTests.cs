using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace BLTools.Core;

/// <summary>
/// Extensions for string
/// </summary>
[SuppressMessage("Performance", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "", Scope = "class")]
public static partial class StringExtension {

  #region Tests
  extension(string sourceValue) {
    /// <summary>
    /// Indicates if a string contains only alphabetic characters (A-Z and a-z)
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsAlpha() {
      return Regex.IsMatch(sourceValue, @"^[A-Za-z]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or numeric characters (0-9)
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsAlphaNumeric() {
      return Regex.IsMatch(sourceValue, "^[A-Za-z0-9]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only numeric characters (0-9) or separators (-.,)
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsNumeric() {
      return Regex.IsMatch(sourceValue, @"^[-\d][\d\.,]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only numeric characters (0-9) or separators (-.,)
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsNumericOnly() {
      return Regex.IsMatch(sourceValue, @"^[-\d][\d]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or blank
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsAlphaOrBlank() {
      return Regex.IsMatch(sourceValue.Trim(), @"^[A-Za-z ]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or numeric characters (0-9) or blank
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsAlphaNumericOrBlank() {
      return Regex.IsMatch(sourceValue.Trim(), @"^[A-Za-z\d ]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only numeric characters (0-9) or blank
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsNumericOrBlank() {
      return Regex.IsMatch(sourceValue.Trim(), @"^[-\d][\d\., ]*$", RegexOptions.Compiled);
    }



    /// <summary>
    /// Indicates if a string contains only alphabetic characters (A-Z and a-z) or numeric characters (0-9) or blank or dashes
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsAlphaNumericOrBlankOrDashes() {
      return Regex.IsMatch(sourceValue.Trim(), @"^[A-Za-z\d -]*$", RegexOptions.Compiled);
    }


    /// <summary>
    /// Indicates if a string contains only specified characters
    /// </summary>
    /// <param name="sourceValue">The source string</param>
    /// <param name="charList">The list of characters to test for</param>
    /// <returns>True if the assertion succeeds</returns>
    public bool IsMadeOfTheseChars(params char[] charList) {
      return Regex.IsMatch(sourceValue.Trim(), $@"^[{string.Join("", charList)}]*$", RegexOptions.Compiled);
    }
  }
  #endregion Tests

}
