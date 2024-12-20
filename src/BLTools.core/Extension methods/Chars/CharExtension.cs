﻿namespace BLTools.Core;

/// <summary>
/// Extensions for char class
/// </summary>
public static class CharExtensions {

  /// <summary>
  /// Generate a string of repeated characters
  /// </summary>
  /// <param name="number">The number of characters</param>
  /// <returns>A string of repeated characters</returns>
  public static string Repeat(this char sourceValue, int number) {
    if (number <= 0) {
      return string.Empty;
    }
    return new string(sourceValue, number);
  }

  /// <summary>
  /// Indicates if a char is in a list
  /// </summary>
  /// <param name="sourceValue">The char to test</param>
  /// <param name="values">The list of chars</param>
  /// <returns><see langword="true"/> when the char is in the list, <see langword="false"/> otherwise</returns>
  public static bool IsIn(this char sourceValue, params char[] values) => values.Contains(sourceValue);

  /// <summary>
  /// Indicates if a char is a numeric characters (0-9)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsNumeric(this char sourceValue) {
    return "-0123456789".Contains(sourceValue);
  }

  /// <summary>
  /// Indicates if a char is a numeric characters (0-9)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsNumericOrBlank(this char sourceValue) {
    return sourceValue.IsNumeric() || sourceValue.IsWhiteSpace();
  }

  /// <summary>
  /// Indicates if a char is a numeric characters (0-9) or separator
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsNumericOrSeparator(this char sourceValue) {
    return "0123456789.,-".Contains(sourceValue);
  }

  /// <summary>
  /// Indicates if a char is an alpha character (A-Z or a-z)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlpha(this char sourceValue) {
    return "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Contains(sourceValue);
  }

  /// <summary>
  /// Indicates if a char is an alpha character (A-Z or a-z) or a blank
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaOrBlank(this char sourceValue) {
    return sourceValue.IsAlpha() || sourceValue.IsWhiteSpace();
  }

  /// <summary>
  /// Indicates if a char is an alpha character (A-Z or a-z) or a numeric character
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaOrNumeric(this char sourceValue) {
    return sourceValue.IsAlpha() || sourceValue.IsNumeric();
  }

  /// <summary>
  /// Indicates if a char is an alpha character (A-Z or a-z) or a blank or a numeric character
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsAlphaOrNumericOrBlank(this char sourceValue) {
    return sourceValue.IsAlphaOrBlank() || sourceValue.IsNumeric();
  }

  /// <summary>
  /// Indicates if a char is a space character (0x20)
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsSpace(this char sourceValue) {
    return sourceValue == ' ';
  }

  /// <summary>
  /// Indicates if a char is a white space character ("space", "tab", "backspace", "formfeed", "CR", "LF")
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>True if the assertion succeeds</returns>
  public static bool IsWhiteSpace(this char sourceValue) {
    return char.IsWhiteSpace(sourceValue);
  }

  public static string WithQuotes(this char sourceValue) => $"'{sourceValue}'";
}
