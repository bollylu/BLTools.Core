namespace BLTools.Core;

/// <summary>
/// Extensions for string
/// </summary>
public static partial class StringExtensions {

  #region --- Left --------------------------------------------
  /// <summary>
  /// Gets the left portion of a string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="length">The number of characters to get</param>
  /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
  public static string Left(this string sourceString, int length) {
    #region Validate parameters
    if (length < 0) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString.Length >= length) {
      return sourceString[..length];
    }

    return sourceString;
  }

  /// <summary>
  /// Gets the left portion of a string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="length">The number of characters to get</param>
  /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
  public static ReadOnlySpan<char> Left(this ReadOnlySpan<char> sourceString, int length) {
    #region Validate parameters
    if (length < 0) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString.Length >= length) {
      return sourceString[..length];
    }

    return sourceString;
  }
  #endregion --- Left --------------------------------------------

  #region --- Right --------------------------------------------
  /// <summary>
  /// Gets the right portion of the string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="length">The number of characters to get</param>
  /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
  public static string Right(this string sourceString, int length) {
    #region Validate parameters
    if (length < 0) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString.Length >= length) {
      return sourceString[^length..];
    }

    return sourceString;
  }

  /// <summary>
  /// Gets the right portion of the string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="length">The number of characters to get</param>
  /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
  public static ReadOnlySpan<char> Right(this ReadOnlySpan<char> sourceString, int length) {
    #region Validate parameters
    if (length < 0) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString.Length >= length) {
      return sourceString[^length..];
    }

    return sourceString;
  }
  #endregion --- Right --------------------------------------------

  #region --- After --------------------------------------------
  /// <summary>
  /// Gets the portion of the string after a given string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the delimiter</returns>
  public static string After(this string sourceString, string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    if (string.IsNullOrEmpty(delimiter)) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString == delimiter) {
      return string.Empty;
    }
    int Index = sourceString.IndexOf(delimiter, 0, stringComparison);
    if (Index == -1) {
      return string.Empty;
    }

    return sourceString[(Index + delimiter.Length)..];
  }

  /// <summary>
  /// Gets the portion of the string after a given string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the delimiter</returns>
  public static ReadOnlySpan<char> After(this ReadOnlySpan<char> sourceString, ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    if (delimiter.Length == 0) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString.SequenceEqual(delimiter)) {
      return [];
    }
    int Index = sourceString.IndexOf(delimiter, stringComparison);
    if (Index == -1) {
      return [];
    }

    return sourceString[(Index + delimiter.Length)..];
  }

  /// <summary>
  /// Gets the portion of the string after a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the delimiter</returns>
  public static string After(this string source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.IndexOf(delimiter, stringComparison);
    if (Index == -1) {
      return string.Empty;
    }

    return source[(Index + 1)..];
  }

  /// <summary>
  /// Gets the portion of the string after a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the delimiter</returns>
  public static ReadOnlySpan<char> After(this ReadOnlySpan<char> source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.IndexOf(delimiter.ToString().AsSpan(), stringComparison);
    if (Index == -1) {
      return string.Empty;
    }

    return source[(Index + 1)..];
  }
  #endregion --- After --------------------------------------------

  #region --- AfterLast --------------------------------------------
  /// <summary>
  /// Gets the portion of the string after the last occurence of a given string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
  public static string AfterLast(this string sourceString, string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    //if (sourceString == null) {
    //  return null;
    //}
    if (string.IsNullOrEmpty(delimiter)) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString == delimiter) {
      return string.Empty;
    }
    int Index = sourceString.LastIndexOf(delimiter, stringComparison);
    if (Index == -1) {
      return string.Empty;
    }

    return sourceString[(Index + delimiter.Length)..];
  }

  /// <summary>
  /// Gets the portion of the string after the last occurence of a given string
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
  public static ReadOnlySpan<char> AfterLast(this ReadOnlySpan<char> sourceString, ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    if (delimiter.Length == 0) {
      return sourceString;
    }
    #endregion Validate parameters

    if (sourceString.SequenceEqual(delimiter)) {
      return string.Empty;
    }
    int Index = sourceString.LastIndexOf(delimiter, stringComparison);
    if (Index == -1) {
      return string.Empty;
    }

    return sourceString[(Index + delimiter.Length)..];
  }

  /// <summary>
  /// Gets the portion of the string after the last occurence of a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
  public static string AfterLast(this string source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.LastIndexOf(delimiter.ToString(), stringComparison);
    if (Index == -1) {
      return string.Empty;
    }

    return source[(Index + 1)..];
  }

  /// <summary>
  /// Gets the portion of the string after the last occurence of a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
  public static ReadOnlySpan<char> AfterLast(this ReadOnlySpan<char> source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.LastIndexOf(delimiter.ToString().AsSpan(), stringComparison);
    if (Index == -1) {
      return "";
    }

    return source[(Index + 1)..];
  }
  #endregion --- AfterLast --------------------------------------------

  #region --- Before --------------------------------------------
  /// <summary>
  /// Gets the portion of the string before a given string
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the delimiter</returns>
  public static string Before(this string source, string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    //if (sourceString == null) {
    //  return null;
    //}
    if (string.IsNullOrEmpty(delimiter)) {
      return source;
    }
    #endregion Validate parameters

    if (source == delimiter) {
      return "";
    }
    int Index = source.IndexOf(delimiter, stringComparison);
    //if ( Index == -1 ) {
    //  return sourceString;
    //}
    if (Index < 1) {
      return "";
    }

    return source.Left(Index);
  }

  /// <summary>
  /// Gets the portion of the string before a given string
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the delimiter</returns>
  public static ReadOnlySpan<char> Before(this ReadOnlySpan<char> source, ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    if (delimiter.Length == 0) {
      return source;
    }
    if (source.SequenceEqual(delimiter)) {
      return string.Empty;
    }

    int Index = source.IndexOf(delimiter, stringComparison);

    if (Index < 1) {
      return string.Empty;
    }

    return source[..Index];
  }

  /// <summary>
  /// Gets the portion of the string before a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the delimiter</returns>
  public static string Before(this string source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.IndexOf(delimiter, stringComparison);
    if (Index < 1) {
      return string.Empty;
    }

    return source.Left(Index);
  }

  /// <summary>
  /// Gets the portion of the string before a given char
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the delimiter</returns>
  public static ReadOnlySpan<char> Before(this ReadOnlySpan<char> sourceString, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    if (sourceString.Length == 0) {
      return null;
    }
    #endregion Validate parameters

    int Index = sourceString.IndexOf(delimiter.ToString().AsSpan(), stringComparison);

    if (Index < 1) {
      return string.Empty;
    }

    return sourceString.Left(Index);
  }
  #endregion --- Before --------------------------------------------

  #region --- BeforeLast --------------------------------------------
  /// <summary>
  /// Gets the portion of the string before the last occurence of a given string
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the last occurence of the delimiter</returns>
  public static string BeforeLast(this string source, string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    //if (sourceString == null) {
    //  return null;
    //}
    if (string.IsNullOrEmpty(delimiter)) {
      return source;
    }
    #endregion Validate parameters

    if (source == delimiter) {
      return "";
    }
    int Index = source.LastIndexOf(delimiter, stringComparison);

    if (Index < 1) {
      return "";
    }

    return source.Left(Index);
  }

  /// <summary>
  /// Gets the portion of the string before the last occurence of a given string
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the last occurence of the delimiter</returns>
  public static ReadOnlySpan<char> BeforeLast(this ReadOnlySpan<char> source, ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    if (delimiter.Length == 0) {
      return source;
    }
    #endregion Validate parameters

    if (source.SequenceEqual(delimiter)) {
      return [];
    }

    int Index = source.LastIndexOf(delimiter, stringComparison);
    if (Index < 1) {
      return [];
    }

    return source[..Index];
  }

  /// <summary>
  /// Gets the portion of the string before the last occurence of a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the last occurence of the delimiter</returns>
  public static string BeforeLast(this string source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.LastIndexOf(delimiter.ToString(), stringComparison);

    if (Index < 1) {
      return string.Empty;
    }

    return source[..Index];
  }

  /// <summary>
  /// Gets the portion of the string before the last occurence of a given char
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="delimiter">The char to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string before the last occurence of the delimiter</returns>
  public static ReadOnlySpan<char> BeforeLast(this ReadOnlySpan<char> source, char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
    int Index = source.LastIndexOf(delimiter.ToString().AsSpan(), stringComparison);

    if (Index < 1) {
      return string.Empty;
    }

    return source[..Index];
  }
  #endregion --- BeforeLast --------------------------------------------

  #region --- Except --------------------------------------------
  /// <summary>
  /// Get the whole string but the part to remove
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="dataToRemove">The string to remove</param>
  /// <returns>The cleaned string</returns>
  public static string Except(this string source, string dataToRemove) {
    #region Validate parameters
    if (string.IsNullOrEmpty(dataToRemove)) {
      return source;
    }
    #endregion Validate parameters

    int Index = source.IndexOf(dataToRemove);
    if (Index == 0) {
      return source;
    }

    return source.Replace(dataToRemove, "");
  }

  /// <summary>
  /// Get the whole string but the part to remove
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="dataToRemove">The string to remove</param>
  /// <returns>The cleaned string</returns>
  public static ReadOnlySpan<char> Except(this ReadOnlySpan<char> source, ReadOnlySpan<char> dataToRemove) {
    #region Validate parameters
    if (dataToRemove.Length == 0) {
      return source;
    }
    #endregion Validate parameters

    int Index = source.IndexOf(dataToRemove);
    if (Index == 0) {
      return source;
    }

    return source.Before(dataToRemove)[dataToRemove.Length..];
  }
  #endregion --- Except --------------------------------------------

  #region --- Between --------------------------------------------
  /// <summary>
  /// Gets the portion of the string after a given string
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="firstDelimiter">The first string to search for</param>
  /// <param name="secondDelimiter">The second string to search for</param>
  /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
  /// <returns>The selected portion of the string between the delimiters</returns>
  public static string Between(this string source, string firstDelimiter = "[", string secondDelimiter = "]", StringComparison stringComparison = StringComparison.CurrentCulture) {
    return source.After(firstDelimiter, stringComparison).Before(secondDelimiter, stringComparison);
  }

  /// <summary>
  /// Gets the portion of the string between two given chars
  /// </summary>
  /// <param name="source">The source string</param>
  /// <param name="firstDelimiter">The first char to search for</param>
  /// <param name="secondDelimiter">The second char to search for</param>
  /// <returns>The selected portion of the string between the delimiters</returns>
  public static string Between(this string source, char firstDelimiter = '[', char secondDelimiter = ']') {
    return source.After(firstDelimiter).Before(secondDelimiter);
  }
  #endregion --- Between --------------------------------------------

  /// <summary>
  /// Gets the strings between two given strings
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="firstDelimiter">The first string to search for</param>
  /// <param name="secondDelimiter">The second string to search for</param>
  /// <param name="stringComparison">How to compare</param>
  /// <returns>A list of items found between both delimiters</returns>
  public static IEnumerable<string> ItemsBetween(this string sourceString, string firstDelimiter = "[", string secondDelimiter = "]", StringComparison stringComparison = StringComparison.CurrentCulture) {
    #region Validate parameters
    if (string.IsNullOrEmpty(sourceString)) {
      yield break;
    }
    if (firstDelimiter == "") {
      yield break;
    }
    if (secondDelimiter == "") {
      yield break;
    }
    #endregion Validate parameters

    string ProcessedString = sourceString;

    while (ProcessedString != "" && ProcessedString.Contains(firstDelimiter, stringComparison) && ProcessedString.Contains(secondDelimiter, stringComparison)) {
      yield return ProcessedString.After(firstDelimiter, stringComparison).Before(secondDelimiter, stringComparison);
      ProcessedString = ProcessedString.After(secondDelimiter, stringComparison);
    }

    yield break;

  }

  /// <summary>
  /// Gets the strings between two given chars
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="firstDelimiter">The first char to search for</param>
  /// <param name="secondDelimiter">The second char to search for</param>
  /// <returns>A list of items found between both delimiters</returns>
  public static IEnumerable<string> ItemsBetween(this string sourceString, char firstDelimiter = '[', char secondDelimiter = ']') {
    #region Validate parameters
    if (string.IsNullOrEmpty(sourceString)) {
      yield break;
    }
    #endregion Validate parameters

    string ProcessedString = sourceString;

    while (ProcessedString != "" && ProcessedString.Contains(firstDelimiter) && ProcessedString.Contains(secondDelimiter)) {
      yield return ProcessedString.After(firstDelimiter).Before(secondDelimiter);
      ProcessedString = ProcessedString.After(secondDelimiter);
    }

    yield break;

  }

  /// <summary>
  /// Get a list of strings from one big string splitted
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <param name="delimiter">The delimmiter</param>
  /// <param name="stringSplitOptions">The options to split</param>
  /// <returns>A list of strings</returns>
  public static IEnumerable<string> GetItems(this string sourceString, string delimiter = ";", StringSplitOptions stringSplitOptions = StringSplitOptions.None) {
    #region === Validate parameters ===
    if (string.IsNullOrEmpty(sourceString)) {
      yield break;
    }
    if (delimiter == "") {
      yield return sourceString;
    }
    #endregion === Validate parameters ===
    foreach (string SplitItem in sourceString.Split(delimiter, stringSplitOptions)) {
      yield return SplitItem;
    }
  }

  /// <summary>
  /// Get all the tags between &lt; and &gt;
  /// </summary>
  /// <param name="sourceString">The source string</param>
  /// <returns>An list of strings</returns>
  public static IEnumerable<string> GetXmlTags(this string sourceString) {
    return sourceString.ItemsBetween('<', '>');
  }

  /// <summary>
  /// Capitalize the first letter of each word and uncapitalize other chars
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <param name="delimiter"></param>
  /// <returns>The proper string</returns>
  public static string Proper(this string sourceValue, char delimiter = ' ') {
    if (string.IsNullOrWhiteSpace(sourceValue)) {
      return "";
    }

    StringBuilder RetVal = new StringBuilder();

    string[] Words = sourceValue.Split(delimiter);
    foreach (string WordItem in Words) {
      RetVal.Append($"{WordItem.Left(1).ToUpper()}{WordItem[1..].ToLower()}{delimiter}");
    }
    RetVal.Truncate(1);

    return RetVal.ToString();
  }

  /// <summary>
  /// Removes external quotes from a string (ex. "\"MyString\"" => "MyString")
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>The string without inner quotes</returns>
  public static string RemoveExternalQuotes(this string sourceValue) {
    if (string.IsNullOrWhiteSpace(sourceValue)) {
      return "";
    }

    if (!sourceValue.Contains('"')) {
      return sourceValue;
    }

    StringBuilder RetVal = new StringBuilder(sourceValue);

    if (sourceValue.StartsWith('\"')) {
      RetVal.Remove(0, 1);
    }

    if (sourceValue.EndsWith('\"')) {
      RetVal.Truncate(1);
    }

    return RetVal.ToString();
  }

  /// <summary>
  /// Get a new string surrounded by double quotes
  /// </summary>
  /// <param name="source">The source string</param>
  /// <returns>a new string surrounded by double quotes, or null if the source is null</returns>
  public static string WithQuotes(this string source) {
    return $"\"{source}\"";
  }

  /// <summary>
  /// Get a new string surrounded by double quotes
  /// </summary>
  /// <param name="source">The source string</param>
  /// <returns>a new string surrounded by double quotes, or null if the source is null</returns>
  public static ReadOnlySpan<char> WithQuotes(this ReadOnlySpan<char> source) {
    return $"\"{source}\"";
  }

  /// <summary>
  /// Get a new string with control chars replaced by escape sequence
  /// </summary>
  /// <param name="sourceValue">The source string</param>
  /// <returns>a new string with control chars replaced by escape sequence</returns>
  public static string ReplaceControlChars(this string sourceValue) {
    #region === Validate parameters ===
    if (string.IsNullOrWhiteSpace(sourceValue)) {
      return "";
    }
    #endregion === Validate parameters ===

    StringBuilder RetVal = new StringBuilder(sourceValue.Length);

    int i = 0;
    int SourceLength = sourceValue.Length;
    bool InQuotes = false;
    bool NextCharIsControlChar = false;

    while (i < SourceLength) {

      char CurrentChar = sourceValue[i];

      if (!InQuotes && !NextCharIsControlChar && CurrentChar == '\\') {
        NextCharIsControlChar = true;
        i++;
        continue;
      }

      if (!InQuotes && NextCharIsControlChar && "\"\\\t\b\r\n\f".Contains(CurrentChar)) {
        NextCharIsControlChar = false;
        RetVal.Append(CurrentChar);
        i++;
        continue;
      }

      if (CurrentChar == '"') {
        RetVal.Append(CurrentChar);
        InQuotes = !InQuotes;
        i++;
        continue;
      }

      RetVal.Append(CurrentChar);
      i++;
    }

    return RetVal.ToString();
  }

  #region --- Alignment --------------------------------------------
  /// <summary>
  /// Aligns the source string to the right of a string of width length, filling any remaining places with the filler
  /// </summary>
  /// <param name="source">The source string to align</param>
  /// <param name="width">The width of the result string</param>
  /// <param name="filler">The filler for the missing characters</param>
  /// <returns></returns>
  public static string AlignedRight(this string source, int width, char filler = ' ') {
    #region === Validate parameters ===
    if (width <= 0) {
      width = source.Length;
    }
    #endregion === Validate parameters ===
    return source.PadLeft(width, filler).Left(width);
  }

  /// <summary>
  /// Aligns the source string to the left of a string of width length, filling any remaining places with the filler
  /// </summary>
  /// <param name="source">The source string to align</param>
  /// <param name="width">The width of the result string</param>
  /// <param name="filler">The filler for the missing characters</param>
  /// <returns></returns>
  public static string AlignedLeft(this string source, int width, char filler = ' ') {
    #region === Validate parameters ===
    if (width <= 0) {
      width = source.Length;
    }
    #endregion === Validate parameters ===
    return source.PadRight(width, filler).Left(width);
  }

  /// <summary>
  /// Centers the source string in a string of width length, filling any remaining places with the filler
  /// </summary>
  /// <param name="source">The source string to align</param>
  /// <param name="width">The width of the result string</param>
  /// <param name="filler">The filler for the missing characters</param>
  /// <returns>The new string, limited to <paramref name="width"/> characters</returns>
  public static string AlignedCenter(this string source, int width, char filler = ' ') {
    #region === Validate parameters ===
    if (width <= 0) {
      width = source.Length;
    }
    #endregion === Validate parameters ===
    string LeftPart = new string(filler, (width / 2) - (source.Length / 2));
    return $"{LeftPart}{source}".PadRight(width, filler).Left(width);
  }
  #endregion --- Alignment --------------------------------------------

  /// <summary>
  /// How to do a match when looking for a string in a list
  /// </summary>
  public enum EStringMatch {
    /// <summary>
    /// The comparison take all characters into account
    /// </summary>
    ExactMatch,
    /// <summary>
    /// The comparison can be partial (e.g. "filename.txt.sample" => true for ".txt.", true for "name", false for "nice")
    /// </summary>
    PartialMatch,
    /// <summary>
    /// The searched string must be at the start of one string in the list
    /// </summary>
    StartsWith,
    /// <summary>
    /// The searched string must be at the end of one string in the list
    /// </summary>
    EndsWith
  }

  /// <summary>
  /// Indicates if a string is in a list of string, using CultureInvariantIgnoreCase
  /// </summary>
  /// <param name="source">The string to look for</param>
  /// <param name="listOfMatches">The list of strings to compare with</param>
  /// <param name="match">How the match if done (default to an exact match)</param>
  /// <returns>true if the string is in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
  public static bool IsIn(this string source, IEnumerable<string> listOfMatches, EStringMatch match = EStringMatch.ExactMatch) => IsIn(source, listOfMatches, StringComparison.InvariantCultureIgnoreCase, match);


  /// <summary>
  /// Indicates if a string is in a list of string
  /// </summary>
  /// <param name="source">The string to look for</param>
  /// <param name="listOfMatches">The list of strings to compare with</param>
  /// <param name="comparison">How the comparison is done</param>
  /// <param name="match">How the match if done (default to an exact match)</param>
  /// <returns>true if the string is in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
  public static bool IsIn(this string source, IEnumerable<string> listOfMatches, StringComparison comparison, EStringMatch match = EStringMatch.ExactMatch) {
    #region === Validate parameters ===
    if (listOfMatches is null || listOfMatches.IsEmpty()) {
      return false;
    }
    #endregion === Validate parameters ===

    return match switch {
      EStringMatch.PartialMatch => listOfMatches.Any(x => x.Contains(source, comparison)),
      EStringMatch.StartsWith => listOfMatches.Any(x => x.StartsWith(source, comparison)),
      EStringMatch.EndsWith => listOfMatches.Any(x => x.EndsWith(source, comparison)),
      EStringMatch.ExactMatch => listOfMatches.Any(x => x.Equals(source, comparison)),
      _ => throw new NotImplementedException()
    };

  }


  /// <summary>
  /// Indicates if a string is NOT in a list of string, using CultureInvariantIgnoreCase
  /// </summary>
  /// <param name="source">The string to look for</param>
  /// <param name="listOfMatches">The list of strings to compare with</param>
  /// <param name="match">How the match if done (default to an exact match)</param>
  /// <returns>true if the string is not in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
  public static bool IsNotIn(this string source, IEnumerable<string> listOfMatches, EStringMatch match = EStringMatch.ExactMatch) => IsNotIn(source, listOfMatches, StringComparison.InvariantCultureIgnoreCase, match);

  /// <summary>
  /// Indicates if a string is NOT in a list of string
  /// </summary>
  /// <param name="source">The string to look for</param>
  /// <param name="listOfMatches">The list of strings to compare with</param>
  /// <param name="comparison">How the comparison is done</param>
  /// <param name="match">How the match if done (default to an exact match)</param>
  /// <returns>true if the string is not in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
  public static bool IsNotIn(this string source, IEnumerable<string> listOfMatches, StringComparison comparison, EStringMatch match = EStringMatch.ExactMatch) {
    #region === Validate parameters ===
    if (source is null) {
      return true;
    }
    if (listOfMatches is null || listOfMatches.IsEmpty()) {
      return true;
    }
    #endregion === Validate parameters ===

    return match switch {
      EStringMatch.PartialMatch => listOfMatches.All(x => !x.Contains(source, comparison)),
      EStringMatch.StartsWith => listOfMatches.All(x => !x.StartsWith(source, comparison)),
      EStringMatch.EndsWith => listOfMatches.All(x => !x.EndsWith(source, comparison)),
      EStringMatch.ExactMatch => listOfMatches.All(x => !x.Equals(source, comparison)),
      _ => throw new NotImplementedException()
    };
  }

}
