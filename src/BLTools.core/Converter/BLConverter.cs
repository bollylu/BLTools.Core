namespace BLTools.Core;

/// <summary>
/// Convert data
/// </summary>
public static class BLConverter : ILoggable {

  /// <summary>
  /// Set to true to obtain additional debug info
  /// </summary>
  public static bool TraceError = false;

  /// <summary>
  /// The logger (for error messages, see TraceError)
  /// </summary>
  public static ILogger Logger { get; set; } = new TTraceLogger();

  /// <summary>
  /// Convert between types
  /// </summary>
  /// <typeparam name="T">The type of data to convert to</typeparam>
  /// <param name="source">The source for the conversion</param>
  /// <returns>A converted value of type T or the default T value</returns>
  public static T? BLConvert<T>(object source) {
    return BLConvert(source, CultureInfo.CurrentCulture, default(T));
  }

  /// <summary>
  /// Convert between types
  /// </summary>
  /// <typeparam name="T">The type of data to convert to</typeparam>
  /// <param name="source">The source for the conversion</param>
  /// <param name="defaultValue">The default value of type T to return if any error</param>
  /// <returns>A converted value of type T or the default value</returns>
  public static T BLConvert<T>(object source, T defaultValue) {
    return BLConvert(source, CultureInfo.CurrentCulture, defaultValue);
  }

  /// <summary>
  /// Convert a value from one type to another (possibly through an evaluation of the value : e.g. "0", "True", "T" all becomes True)
  /// </summary>
  /// <typeparam name="T">Requested output type</typeparam>
  /// <param name="source">Data source</param>
  /// <param name="culture">Culture used to perform certain conversions</param>
  /// <param name="defaultValue">What to return when unable to convert</param>
  /// <param name="separatorForMultipleItems">Separator character for array conversions</param>
  /// <returns>A converted value of type T or the default value</returns>
  public static T BLConvert<T>(object source, CultureInfo culture, T defaultValue, char separatorForMultipleItems = ';') {
    try {
      // Handle null source
      if (source == null) {
        return defaultValue;
      }

      // Handle direct cast
      if (source.GetType() == typeof(T)) {
        return (T)source;
      }

      // Handle Enums
      if (typeof(T).IsEnum) {
        if (source is string enumSource) {
          try {
            return (T)Enum.Parse(typeof(T), enumSource, ignoreCase: true);
          } catch {
            _LogError($"Failed to parse enum value: {enumSource}");
            return defaultValue;
          }
        }
        return defaultValue;
      }

      // Handle Version
      if (typeof(T).Name == nameof(Version)) {
        if (source is string versionSource && Version.TryParse(versionSource, out Version? versionParsed)) {
          return (T)Convert.ChangeType(versionParsed, typeof(Version));
        }
        return defaultValue;
      }

      // Use culture info
      culture ??= CultureInfo.CurrentCulture;

      // Handle specific types
      string typeName = typeof(T).Name.ToLowerInvariant();

      return typeName switch {
        "double" or "single" => ConvertFloatingPoint<T>(source, culture, defaultValue),
        "datetime" => (T)Convert.ChangeType(source, typeof(T), culture.DateTimeFormat),
        "string[]" => ConvertStringArray<T>(source, separatorForMultipleItems, defaultValue),
        "int32[]" => ConvertNumericArray<T, int>(source, separatorForMultipleItems, int.TryParse, defaultValue),
        "int64[]" => ConvertNumericArray<T, long>(source, separatorForMultipleItems, long.TryParse, defaultValue),
        "double[]" => ConvertCultureNumericArray<T, double>(source, culture, separatorForMultipleItems, double.TryParse, defaultValue),
        "single[]" => ConvertCultureNumericArray<T, float>(source, culture, separatorForMultipleItems, float.TryParse, defaultValue),
        "boolean" => ConvertBoolean<T>(source, defaultValue),
        "guid" => ConvertGuid<T>(source, defaultValue),
        _ => (T)Convert.ChangeType(source, typeof(T))
      };
    } catch (Exception ex) {
      Logger.LogError($"Error during conversion of {source?.ToString()?.WithQuotes()} to {typeof(T).Name}", ex);
      return defaultValue;
    }
  }

  private static T ConvertFloatingPoint<T>(object source, CultureInfo culture, T defaultValue) {
    if (source is not string floatSource) {
      return (T)Convert.ChangeType(source, typeof(T), culture.NumberFormat);
    }

    char decimalSeparator = culture.NumberFormat.NumberDecimalSeparator[0];
    int nonNumericCount = 0;
    int decimalSeparatorCount = 0;

    foreach (char c in floatSource) {
      if (!c.IsNumeric() && c != decimalSeparator) {
        nonNumericCount++;
      } else if (c == decimalSeparator) {
        decimalSeparatorCount++;
      }
    }

    if (nonNumericCount > 0) {
      _LogError($"Bad format for conversion: {floatSource}: unknown non-numeric characters");
      return defaultValue;
    }

    if (decimalSeparatorCount > 1) {
      _LogError($"Bad format for conversion: {floatSource}: too many decimal separators ({decimalSeparator})");
      return defaultValue;
    }

    return (T)Convert.ChangeType(floatSource, typeof(T), culture.NumberFormat);
  }

  private static T ConvertStringArray<T>(object source, char separator, T defaultValue) {
    if (source is not string stringArraySource) {
      _LogError($"Bad format for conversion to string[]: source is not a string: {source.GetType().GetNameEx()}");
      return defaultValue;
    }
    return (T)Convert.ChangeType(stringArraySource.Split(separator), typeof(T));
  }

  private static T ConvertNumericArray<T, TNumeric>(object source, char separator, TryParseDelegate<TNumeric> tryParse, T defaultValue)
    where TNumeric : struct, IComparable, IFormattable, IConvertible {
    if (source is not string numericArraySource) {
      _LogError($"Bad format for conversion to {typeof(TNumeric).Name}[]: source is not a string: {source.GetType().GetNameEx()}");
      return defaultValue;
    }

    string[] parts = numericArraySource.Split(separator);
    TNumeric[] result = new TNumeric[parts.Length];

    for (int i = 0; i < parts.Length; i++) {
      if (!tryParse(parts[i], out TNumeric parsedValue)) {
        _LogError($"Failed to parse {typeof(TNumeric).Name} value: {parts[i]}");
        return defaultValue;
      }
      result[i] = parsedValue;
    }

    return (T)Convert.ChangeType(result, typeof(T));
  }

  private static T ConvertCultureNumericArray<T, TNumeric>(object source, CultureInfo culture, char separator, CultureTryParseDelegate<TNumeric> tryParse, T defaultValue)
    where TNumeric : struct, IComparable, IFormattable, IConvertible {
    if (source is not string numericArraySource) {
      _LogError($"Bad format for conversion to {typeof(TNumeric).Name}[]: source is not a string: {source.GetType().GetNameEx()}");
      return defaultValue;
    }

    string[] parts = numericArraySource.Split(separator);
    TNumeric[] result = new TNumeric[parts.Length];

    for (int i = 0; i < parts.Length; i++) {
      if (!tryParse(parts[i], NumberStyles.Any, culture.NumberFormat, out TNumeric parsedValue)) {
        _LogError($"Failed to parse {typeof(TNumeric).Name} value: {parts[i]}");
        return defaultValue;
      }
      result[i] = parsedValue;
    }

    return (T)Convert.ChangeType(result, typeof(T));
  }

  private static T ConvertBoolean<T>(object source, T defaultValue) {
    if (source is string stringBoolSource) {
      return (T)Convert.ChangeType(stringBoolSource.ToBool(), typeof(T));
    }

    long signedValue;
    ulong unsignedValue;

    switch (source.GetType().Name.ToLowerInvariant()) {
      case "int16":
      case "int32":
      case "int64":
      case "sbyte":
        signedValue = (long)Convert.ChangeType(source, typeof(long));
        return (T)Convert.ChangeType(signedValue == 1, typeof(T));

      case "uint16":
      case "uint32":
      case "uint64":
      case "byte":
        unsignedValue = (ulong)Convert.ChangeType(source, typeof(ulong));
        return (T)Convert.ChangeType(unsignedValue == 1, typeof(T));
    }

    _LogError($"Error during conversion of {source?.ToString()?.WithQuotes()} to {typeof(T).GetNameEx()}: unhandled source type");
    return defaultValue;
  }

  private static T ConvertGuid<T>(object source, T defaultValue) {
    switch (source) {
      case string guidStringSource:
        if (guidStringSource.Trim().IsEmpty()) {
          return (T)Convert.ChangeType(Guid.Empty, typeof(T));
        }
        if (Guid.TryParse(guidStringSource, out Guid parsedGuid)) {
          return (T)Convert.ChangeType(parsedGuid, typeof(T));
        }
        _LogError($"Failed to parse GUID value: {guidStringSource}");
        return defaultValue;

      case Guid guidSource:
        return (T)Convert.ChangeType(guidSource, typeof(T));

      default:
        return defaultValue;
    }
  }

  private static void _LogError(string message) {
    if (TraceError && Logger is not null) {
      Logger.LogError(message);
    }
  }

  /// <summary>
  /// Delegate for TryParse methods without culture parameter
  /// </summary>
  private delegate bool TryParseDelegate<T>(string s, out T result);

  /// <summary>
  /// Delegate for TryParse methods with culture parameter
  /// </summary>
  private delegate bool CultureTryParseDelegate<T>(string s, NumberStyles style, IFormatProvider provider, out T result);
}
