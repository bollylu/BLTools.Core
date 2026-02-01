using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace BLTools.Core;

public static class StringParser {

  /// <summary>
  /// The default character used to separate values when converting to an enumerable collection.
  /// </summary>
  public static char DEFAULT_SEPARATOR { get; set; } = ';';
  public static CultureInfo DEFAULT_CULTURE { get; set; } = CultureInfo.InvariantCulture;

  #region --- Version --------------------------------------------
  /// <summary>
  /// Converts a string to a Version.
  /// </summary>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted Version, or the default value if the conversion fails.</returns>
  public static Version ToVersion(this string source, Version defaultValue) => ToVersion(source.AsSpan(), defaultValue);

  /// <summary>
  /// Converts a ReadOnlySpan<char> to a Version.
  /// </summary>
  /// <param name="source">The ReadOnlySpan<char> to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted Version, or the default value if the conversion fails.</returns>
  public static Version ToVersion(this ReadOnlySpan<char> source, Version defaultValue) {
    if (Version.TryParse(source, out Version? result)) {
      return result;
    } else {
      return defaultValue;
    }
  }
  #endregion --- Version -----------------------------------------

  #region --- Guid --------------------------------------------
  /// <summary>
  /// Converts a string to a Guid.
  /// </summary>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted Guid, or the default value if the conversion fails.</returns>
  public static Guid ToGuid(this string source, Guid defaultValue) => ToGuid(source.AsSpan(), defaultValue);

  /// <summary>
  /// Converts a ReadOnlySpan<char> to a Guid.
  /// </summary>
  /// <param name="source">The ReadOnlySpan<char> to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted Guid, or the default value if the conversion fails.</returns>
  public static Guid ToGuid(this ReadOnlySpan<char> source, Guid defaultValue) {
    if (Guid.TryParse(source, out Guid result)) {
      return result;
    } else {
      return defaultValue;
    }
  }
  #endregion --- Guid -----------------------------------------

  #region --- DateTime --------------------------------------------
  /// <summary>
  /// Converts a string to a DateTime using the current culture info.
  /// </summary>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted DateTime, or the default value if the conversion fails.</returns>
  public static DateTime ToDateTime(this string source, DateTime defaultValue) => ToDateTime(source.AsSpan(), CultureInfo.CurrentCulture, defaultValue);

  /// <summary>
  /// Converts a ReadOnlySpan<char> to a DateTime using the current culture info.
  /// </summary>
  /// <param name="source">The ReadOnlySpan<char> to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted DateTime, or the default value if the conversion fails.</returns>
  public static DateTime ToDateTime(this ReadOnlySpan<char> source, DateTime defaultValue) => ToDateTime(source, CultureInfo.CurrentCulture, defaultValue);

  /// <summary>
  /// Converts a string to a DateTime using the specified culture information.
  /// </summary>
  /// <param name="source">The string to convert.</param>
  /// <param name="cultureInfo">The culture information to use for the conversion.</param>
  /// <param name="defaultValue">The default value to return if the conversion fails.</param>
  /// <returns>The converted DateTime value, or the default value if the conversion fails.</returns>
  public static DateTime ToDateTime(this string source, CultureInfo cultureInfo, DateTime defaultValue) => ToDateTime(source.AsSpan(), cultureInfo, defaultValue);

  /// <summary>
  /// Converts a ReadOnlySpan<char> to a DateTime using the specified culture info.
  /// </summary>
  /// <param name="source">The ReadOnlySpan<char> to convert.</param>
  /// <param name="cultureInfo">The culture info to use for the conversion.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <returns>The converted DateTime, or the default value if the conversion fails.</returns>
  public static DateTime ToDateTime(this ReadOnlySpan<char> source, CultureInfo cultureInfo, DateTime defaultValue) {
    if (DateTime.TryParse(source, cultureInfo, out DateTime result)) {
      return result;
    } else {
      return defaultValue;
    }
  }
  #endregion --- DateTime -----------------------------------------

  #region --- Enum --------------------------------------------
  /// <summary>
  /// Converts a string to an enum of type T.
  /// </summary>
  /// <typeparam name="T">The type of the enum to convert to.</typeparam>
  /// <param name="source">The ReadOnlySpan<char> to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <param name="ignoreCase">Whether to ignore case when converting.</param>
  /// <returns>The converted enum value, or the default value if the conversion fails.</returns>
  public static T ToEnum<T>(this string source, T defaultValue, bool ignoreCase = true) where T : struct, Enum => ToEnum(source.AsSpan(), ignoreCase, defaultValue);
  /// <summary>
  /// Converts a ReadOnlySpan<char> to an enum of type T.
  /// </summary>
  /// <typeparam name="T">The type of the enum to convert to.</typeparam>
  /// <param name="source">The ReadOnlySpan<char> to convert.</param>
  /// <param name="defaultValue">The default value to return in case the conversion fails.</param>
  /// <param name="ignoreCase">Whether to ignore case when converting.</param>
  /// <returns>The converted enum value, or the default value if the conversion fails.</returns>
  public static T ToEnum<T>(this ReadOnlySpan<char> source, bool ignoreCase, T defaultValue) where T : struct, Enum {
    if (Enum.TryParse(source, ignoreCase, out T result)) {
      return result;
    } else {
      return defaultValue;
    }
  }
  #endregion --- Enum -----------------------------------------

  #region --- Boolean --------------------------------------------
  /// <summary>
  /// Converts a string to a bool
  /// </summary>
  /// <param name="booleanString">A string representing a bool (0,1; false,true; no,yes; n,y)</param>
  /// <returns>A bool as represented by the string (default=false)</returns>
  public static bool ToBool(this string booleanString, bool defaultValue = false, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase) {

    #region Validate parameters
    if (booleanString.IsEmpty()) {
      return defaultValue;
    }
    #endregion Validate parameters

    return booleanString switch {
      _ when booleanString.IsIn(["0", "false", "no", "n"], stringComparison, StringExtensions.EStringMatch.ExactMatch) => false,
      _ when booleanString.IsIn(["1", "true", "yes", "y"], stringComparison, StringExtensions.EStringMatch.ExactMatch) => true,
      _ => defaultValue
    };

  }

  /// <summary>
  /// Converts a string to a bool
  /// </summary>
  /// <param name="booleanString">The string to convert</param>
  /// <param name="trueValue">The string value representing true</param>
  /// <param name="falseValue">The string value representing false</param>
  /// <param name="isCaseSensitive">Do we test the values with case sensitivity (default=false)</param>
  /// <returns>A bool as represented by the string (default=false)</returns>
  public static bool ToBool(this string booleanString, string trueValue, string falseValue, StringComparison comparison = StringComparison.CurrentCultureIgnoreCase) =>
    booleanString.AsSpan().ToBool(trueValue.AsSpan(), falseValue.AsSpan(), comparison);

  /// <summary>
  /// Converts a ReadOnlySpan<char> to a bool
  /// </summary>
  /// <param name="booleanString">The ReadOnlySpan<char> to convert</param>
  /// <param name="trueValue">The ReadOnlySpan<char> value representing true</param>
  /// <param name="falseValue">The ReadOnlySpan<char> value representing false</param>
  /// <param name="isCaseSensitive">Do we test the values with case sensitivity (default=false)</param>
  /// <returns>A bool as represented by the string (default=false)</returns>
  public static bool ToBool(this ReadOnlySpan<char> booleanString, ReadOnlySpan<char> trueValue, ReadOnlySpan<char> falseValue, StringComparison comparison = StringComparison.CurrentCultureIgnoreCase) {
    #region Validate parameters
    if (booleanString.Length == 0) {
      return false;
    }
    #endregion Validate parameters

    if (booleanString.Equals(trueValue, comparison)) {
      return true;
    }

    if (booleanString.Equals(falseValue, comparison)) {
      return false;
    }

    Trace.WriteLine($"Error: value to convert doesn't match any possible value : true={trueValue.WithQuotes()}, false={falseValue.WithQuotes()}, actual={booleanString.WithQuotes()}");
    return false;

  }
  #endregion --- Boolean -----------------------------------------

  #region --- BinaryInteger --------------------------------------------
  /// <summary>
  /// Converts a string to a binary integer type T.
  /// </summary>
  /// <typeparam name="T">The destination binary integer type to convert to.</typeparam>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <returns>The converted binary integer value or the default value in case of error.</returns>
  public static T ToBinaryInteger<T>(this string source, T defaultValue) where T : IBinaryInteger<T> =>
    ToBinaryInteger(source.AsSpan(), defaultValue, DEFAULT_CULTURE);

  /// <summary>
  /// Converts a string to a binary integer type T.
  /// </summary>
  /// <typeparam name="T">The destination binary integer type to convert to.</typeparam>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <returns>The converted binary integer value or the default value in case of error.</returns>
  public static T ToBinaryInteger<T>(this ReadOnlySpan<char> source, T defaultValue) where T : IBinaryInteger<T> =>
    ToBinaryInteger(source, defaultValue, DEFAULT_CULTURE);

  public static T ToBinaryInteger<T>(this string source, T defaultValue, CultureInfo culture) where T : IBinaryInteger<T> =>
    ToBinaryInteger(source.AsSpan(), defaultValue, culture);

  public static T ToBinaryInteger<T>(this ReadOnlySpan<char> source, T defaultValue, CultureInfo culture) where T : IBinaryInteger<T> {
    try {
      if (T.TryParse(source, NumberStyles.Integer | NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands, culture, out T? result)) {
        return result;
      } else {
        return defaultValue;
      }
    } catch {
      return defaultValue;
    }
  }
  #endregion --- BinaryInteger -----------------------------------------

  #region --- FloatingPoint --------------------------------------------
  /// <summary>
  /// Converts a string to a floating point number using InvariantCulture
  /// </summary>
  /// <typeparam name="T">The type of the floating point number to convert to.</typeparam>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return if the conversion fails.</param>
  /// <returns>The converted floating point number, or the default value if the conversion fails.</returns>
  public static T ToFloatingPoint<T>(this string source, T defaultValue) where T : IFloatingPoint<T> =>
    ToFloatingPoint(source, defaultValue, DEFAULT_CULTURE);

  /// <summary>
  /// Converts a string to a floating point type T, using the specified culture.
  /// </summary>
  /// <typeparam name="T">The destination floating point type to convert to.</typeparam>
  /// <param name="source">The string to convert.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <param name="culture">The culture to use for the conversion.</param>
  /// <returns>The converted floating point value or the default value in case of error.</returns>
  public static T ToFloatingPoint<T>(this string source, T defaultValue, CultureInfo culture) where T : IFloatingPoint<T> =>
    ToFloatingPoint(source.AsSpan(), defaultValue, culture);


  public static T ToFloatingPoint<T>(this ReadOnlySpan<char> source, T defaultValue) where T : IFloatingPoint<T> =>
    ToFloatingPoint(source, defaultValue, DEFAULT_CULTURE);

  public static T ToFloatingPoint<T>(this ReadOnlySpan<char> source, T defaultValue, CultureInfo culture) where T : IFloatingPoint<T> {
    char DecimalSeparator = culture.NumberFormat.NumberDecimalSeparator[0];
    int decimalCount = 0;

    foreach (var c in source) {
      if (!c.IsNumericOrSeparator()) {
        return defaultValue;
      }

      if (c == DecimalSeparator) {
        decimalCount++;
        if (decimalCount > 1) {
          return defaultValue;
        }
      }
    }

    try {
      return T.Parse(source, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, culture);
    } catch {
      return defaultValue;
    }
  }
  #endregion --- FloatingPoint -----------------------------------------

  #region --- List of BinaryInteger --------------------------------------------
  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this string source, T defaultValue) where T : IBinaryInteger<T> =>
    ToEnumerableBinaryInteger(source.AsSpan(), defaultValue, DEFAULT_SEPARATOR, DEFAULT_CULTURE);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this string source, T defaultValue, char separator) where T : IBinaryInteger<T> =>
    ToEnumerableBinaryInteger(source.AsSpan(), defaultValue, separator, DEFAULT_CULTURE);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this string source, T defaultValue, char separator, CultureInfo culture) where T : IBinaryInteger<T> =>
    ToEnumerableBinaryInteger(source.AsSpan(), defaultValue, separator, culture);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this string source, T defaultValue, CultureInfo culture) where T : IBinaryInteger<T> =>
    ToEnumerableBinaryInteger(source.AsSpan(), defaultValue, DEFAULT_SEPARATOR, culture);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this ReadOnlySpan<char> source, T defaultValue) where T : IBinaryInteger<T> =>
    ToEnumerableBinaryInteger(source, defaultValue, DEFAULT_SEPARATOR, DEFAULT_CULTURE);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this ReadOnlySpan<char> source, T defaultValue, char separator) where T : IBinaryInteger<T> =>
   ToEnumerableBinaryInteger(source, defaultValue, separator, DEFAULT_CULTURE);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this ReadOnlySpan<char> source, T defaultValue, CultureInfo culture) where T : IBinaryInteger<T> =>
    ToEnumerableBinaryInteger(source, defaultValue, DEFAULT_SEPARATOR, culture);

  public static IEnumerable<T> ToEnumerableBinaryInteger<T>(this ReadOnlySpan<char> source, T defaultValue, char separator, CultureInfo culture) where T : IBinaryInteger<T> {
    List<T> RetVal = [];
    foreach (var RangeItem in source.Split(separator)) {
      if (RangeItem.End.Value - RangeItem.Start.Value <= 0) {
        continue;
      }
      RetVal.Add(source[RangeItem].ToBinaryInteger<T>(defaultValue, culture));
    }
    return RetVal;
  }
  #endregion --- List of BinaryInteger -----------------------------------------



  /// <summary>
  /// Converts a string to an enumerable collection of floating point type T using InvariantCulture.
  /// </summary>
  /// <typeparam name="T">The destination floating point type to convert to.</typeparam>
  /// <param name="source">The string to convert, where each value is separated by the default separator.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <returns>An enumerable collection of floating point with either converted value or the default value in case of error.</returns>
  /// <remarks>Collection elements in the source string must be separated by a different character than comma separator from the culture.</remarks>
  public static IEnumerable<T> ToEnumerableFloatingPoint<T>(this string source, T defaultValue) where T : IFloatingPoint<T> =>
    ToEnumerableFloatingPoint(source, defaultValue, DEFAULT_CULTURE, DEFAULT_SEPARATOR);

  /// <summary>
  /// Converts a string to an enumerable collection of floating point type T using InvariantCulture.
  /// </summary>
  /// <typeparam name="T">The destination floating point type to convert to.</typeparam>
  /// <param name="source">The string to convert, where each value is separated by the specified separator.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <param name="separator">The character that separates the values in the source string (default=DEFAULT_SEPARATOR).</param>
  /// <returns>An enumerable collection of floating point with either converted value or the default value in case of error.</returns>
  /// <remarks>Collection elements in the source string must be separated by a different character than comma separator from the culture.</remarks>
  public static IEnumerable<T> ToEnumerableFloatingPoint<T>(this string source, T defaultValue, char separator) where T : IFloatingPoint<T> =>
    ToEnumerableFloatingPoint(source, defaultValue, DEFAULT_CULTURE, separator);

  /// <summary>
  /// Converts a string to an enumerable collection of floating point type T, using the specified culture.
  /// </summary>
  /// <typeparam name="T">The destination floating point type to convert to.</typeparam>
  /// <param name="source">The string to convert, where each value is separated by the default separator.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <param name="culture">The culture to use for the conversion.</param>
  /// <returns>An enumerable collection of floating point with either converted value or the default value in case of error.</returns>
  /// /// <remarks>Collection elements in the source string must be separated by a different character than comma separator from the culture.</remarks>
  public static IEnumerable<T> ToEnumerableFloatingPoint<T>(this string source, T defaultValue, CultureInfo culture) where T : IFloatingPoint<T> =>
    ToEnumerableFloatingPoint(source, defaultValue, culture, DEFAULT_SEPARATOR);

  /// <summary>
  /// Converts a string to an enumerable collection of floating point type T, using the specified culture.
  /// </summary>
  /// <typeparam name="T">The destination floating point type to convert to.</typeparam>
  /// <param name="source">The string to convert, where each value is separated by the specified separator.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <param name="culture">The culture to use for the conversion.</param>
  /// <param name="separator">The character that separates the values in the source string (default is DEFAULT_SEPARATOR).</param>
  /// <returns>An enumerable collection of floating point with either converted value or the default value in case of error.</returns>
  /// /// <remarks>Collection elements in the source string must be separated by a different character than comma separator from the culture.</remarks>
  public static IEnumerable<T> ToEnumerableFloatingPoint<T>(this string source, T defaultValue, CultureInfo culture, char separator) where T : IFloatingPoint<T> =>
    ToEnumerableFloatingPoint(source.AsSpan(), defaultValue, separator, culture);

  /// <summary>
  /// Converts a string to an enumerable collection of binary integer type T.
  /// </summary>
  /// <typeparam name="T">The destination binary integer type to convert to.</typeparam>
  /// <param name="source">The string to convert, where each value is separated by the default separator.</param>
  /// <param name="defaultValue">The default value to return in case of conversion error.</param>
  /// <returns>An enumerable collection of binary integer with either converted value or the default value in case of error.</returns>
  public static IEnumerable<T> ToEnumerableFloatingPoint<T>(this ReadOnlySpan<char> source, T defaultValue, char separator, CultureInfo culture) where T : IFloatingPoint<T> {
    List<T> RetVal = [];
    foreach (var RangeItem in source.Split(separator)) {
      if (RangeItem.End.Value - RangeItem.Start.Value <= 0) {
        continue;
      }
      RetVal.Add(source[RangeItem].ToFloatingPoint<T>(defaultValue, culture));
    }
    return RetVal;
  }


  /// <summary>
  /// Converts a string to a T, using InvariantCulture
  /// </summary>
  /// <typeparam name="T">The destination type to convert to</typeparam>
  /// <param name="source">The source value</param>
  /// <param name="defaultValue">A default value to return in case of error</param>
  /// <returns>The converted value or the default value in case of error</returns>
  public static T Parse<T>(this string source, T defaultValue) => Parse(source, defaultValue, CultureInfo.InvariantCulture);

  /// <summary>
  /// Converts a string to a T
  /// </summary>
  /// <typeparam name="T">The destination type to convert to</typeparam>
  /// <param name="source"The source value></param>
  /// <param name="defaultValue">A default value to return in case of error</param>
  /// <param name="culture">The culture to use for conversion (default=InvariantCulture)</param>
  /// <returns>The converted value or the default value in case of error</returns>
  public static T Parse<T>(this string source, T defaultValue, CultureInfo culture) {
    try {
      switch (defaultValue) {
        case string:
          return (T)Convert.ChangeType(source, typeof(string));
        case bool BoolDefaultValue:
          return (T)Convert.ChangeType(source.ToBool(BoolDefaultValue), typeof(T));
        case int IntDefaultValue:
          return (T)Convert.ChangeType(source.ToBinaryInteger(IntDefaultValue), typeof(T));
        case uint UIntDefaultValue:
          return (T)Convert.ChangeType(source.ToBinaryInteger(UIntDefaultValue), typeof(T));
        case long LongDefaultValue:
          return (T)Convert.ChangeType(source.ToBinaryInteger(LongDefaultValue), typeof(T));
        case ulong ULongDefaultValue:
          return (T)Convert.ChangeType(source.ToBinaryInteger(ULongDefaultValue), typeof(T));
        case Int128 Int128DefaultValue:
          return (T)Convert.ChangeType(source.ToBinaryInteger(Int128DefaultValue), typeof(T));
        case UInt128 UInt128DefaultValue:
          return (T)Convert.ChangeType(source.ToBinaryInteger(UInt128DefaultValue), typeof(T));
        case float FloatDefaultValue:
          return (T)Convert.ChangeType(source.ToFloatingPoint(FloatDefaultValue, culture), typeof(T));
        case double DoubleDefaultValue:
          return (T)Convert.ChangeType(source.ToFloatingPoint(DoubleDefaultValue, culture), typeof(T));
        case decimal DecimalDefaultValue:
          return (T)Convert.ChangeType(source.ToFloatingPoint(DecimalDefaultValue, culture), typeof(T));
        case DateTime DateTimeDefaultValue:
          return (T)Convert.ChangeType(source.AsSpan().ToDateTime(culture, DateTimeDefaultValue), typeof(T));
        case Guid GuidDefaultValue:
          return (T)Convert.ChangeType(source.AsSpan().ToGuid(GuidDefaultValue), typeof(T));
        case Version VersionDefaultValue:
          return (T)Convert.ChangeType(source.AsSpan().ToVersion(VersionDefaultValue), typeof(T));
        case Enum EnumDefaultValue:
          try {
            Enum Result = (Enum)Enum.Parse(EnumDefaultValue.GetType(), source, true);
            return (T)Convert.ChangeType(Result, typeof(T));
          } catch {
            return defaultValue;
          }
        default:
          return (T)Convert.ChangeType(source, typeof(T));
      }
    } catch {
      return defaultValue;
    }
  }

}
