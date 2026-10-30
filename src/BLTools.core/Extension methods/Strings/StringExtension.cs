namespace BLTools.Core;

/// <summary>
/// Extensions for string
/// </summary>
public static partial class StringExtensions {

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

  extension(ReadOnlySpan<char> source) {

    /// <summary>
    /// Gets the left portion of a string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="length">The number of characters to get</param>
    /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
    public ReadOnlySpan<char> Left(int length) {
      #region Validate parameters
      if (length < 0) {
        return source;
      }
      #endregion Validate parameters

      if (source.Length >= length) {
        return source[..length];
      }

      return source;
    }

    /// <summary>
    /// Gets the right portion of the string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="length">The number of characters to get</param>
    /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
    public ReadOnlySpan<char> Right(int length) {
      #region Validate parameters
      if (length < 0) {
        return source;
      }
      #endregion Validate parameters

      if (source.Length >= length) {
        return source[^length..];
      }

      return source;
    }

    /// <summary>
    /// Gets the portion of the string after a given string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the delimiter</returns>
    public ReadOnlySpan<char> After(ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      #region Validate parameters
      if (delimiter.Length == 0) {
        return source;
      }
      #endregion Validate parameters

      if (source.SequenceEqual(delimiter)) {
        return [];
      }
      int Index = source.IndexOf(delimiter, stringComparison);
      if (Index == -1) {
        return [];
      }

      return source[(Index + delimiter.Length)..];
    }

    /// <summary>
    /// Gets the portion of the string after a given char
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The char to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the delimiter</returns>
    public ReadOnlySpan<char> After(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      int Index = source.IndexOf(delimiter.ToString().AsSpan(), stringComparison);
      if (Index == -1) {
        return string.Empty;
      }

      return source[(Index + 1)..];
    }

    /// <summary>
    /// Gets the portion of the string after the last occurence of a given string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
    public ReadOnlySpan<char> AfterLast(ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      #region Validate parameters
      if (delimiter.Length == 0) {
        return source;
      }
      #endregion Validate parameters

      if (source.SequenceEqual(delimiter)) {
        return string.Empty;
      }
      int Index = source.LastIndexOf(delimiter, stringComparison);
      if (Index == -1) {
        return string.Empty;
      }

      return source[(Index + delimiter.Length)..];
    }

    /// <summary>
    /// Gets the portion of the string after the last occurence of a given char
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The char to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
    public ReadOnlySpan<char> AfterLast(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      int Index = source.LastIndexOf(delimiter.ToString().AsSpan(), stringComparison);
      if (Index == -1) {
        return "";
      }

      return source[(Index + 1)..];
    }

    /// <summary>
    /// Gets the portion of the string before a given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string before the delimiter</returns>
    public ReadOnlySpan<char> Before(ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
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
    public ReadOnlySpan<char> Before(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      #region Validate parameters
      if (source.Length == 0) {
        return null;
      }
      #endregion Validate parameters

      int Index = source.IndexOf(delimiter.ToString().AsSpan(), stringComparison);

      if (Index < 1) {
        return string.Empty;
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
    public ReadOnlySpan<char> BeforeLast(ReadOnlySpan<char> delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
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
    public ReadOnlySpan<char> BeforeLast(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      int Index = source.LastIndexOf(delimiter.ToString().AsSpan(), stringComparison);

      if (Index < 1) {
        return string.Empty;
      }

      return source[..Index];
    }

    /// <summary>
    /// Get the whole string but the part to remove
    /// </summary>
    /// <param name="dataToRemove">The string to remove</param>
    /// <returns>The cleaned string</returns>
    public ReadOnlySpan<char> Except(ReadOnlySpan<char> dataToRemove) {
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

    /// <summary>
    /// Get a new string surrounded by double quotes
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>a new string surrounded by double quotes, or null if the source is null</returns>
    public ReadOnlySpan<char> WithQuotes() {
      return $"\"{source}\"";
    }

    /// <summary>
    /// Get a new string surrounded by single quotes (eg. for SQL)
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>a new string surrounded by single quotes, or null if the source is null</returns>
    public ReadOnlySpan<char> WithSingleQuotes() {
      return $"'{source}'";
    }

    /// <summary>
    /// Removes external quotes from a string (ex. "\"MyString\"" => "MyString")
    /// </summary>
    /// <returns>The string without inner quotes</returns>
    public ReadOnlySpan<char> RemoveExternalQuotes() {
      if (source.Length == 0) {
        return "";
      }

      if (!source.Contains('\"')) {
        return source;
      }

      const char QUOTE = '\"';

      if (source.StartsWith(QUOTE)) {
        source = source[1..];
      }

      if (source.EndsWith(QUOTE)) {
        source = source[..^1];
      }

      return source;
    }
  }

  extension(string source) {

    /// <summary>
    /// Gets the left portion of a string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="length">The number of characters to get</param>
    /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
    public string Left(int length) {
      return source.AsSpan().Left(length).ToString();
    }

    /// <summary>
    /// Gets the right portion of the string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="length">The number of characters to get</param>
    /// <returns>The selected portion of the string. If Length > Length of the string, returns the string.</returns>
    public string Right(int length) {
      return source.AsSpan().Right(length).ToString();
    }

    /// <summary>
    /// Gets the portion of the string after a given string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the delimiter</returns>
    public string After(string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().After(delimiter.AsSpan(), stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string after a given char
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The char to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the delimiter</returns>
    public string After(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().After(delimiter, stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string after the last occurence of a given string
    /// </summary>
    /// <param name="sourceString">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
    public string AfterLast(string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().AfterLast(delimiter.AsSpan(), stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string after the last occurence of a given char
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The char to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string after the last occurence of a delimiter</returns>
    public string AfterLast(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().AfterLast(delimiter, stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string before a given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string before the delimiter</returns>
    public string Before(string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().Before(delimiter.AsSpan(), stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string before a given char
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The char to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string before the delimiter</returns>
    public string Before(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().Before(delimiter, stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string before the last occurence of a given string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string before the last occurence of the delimiter</returns>
    public string BeforeLast(string delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().BeforeLast(delimiter.AsSpan(), stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string before the last occurence of a given char
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="delimiter">The char to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string before the last occurence of the delimiter</returns>
    public string BeforeLast(char delimiter, StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().BeforeLast(delimiter, stringComparison).ToString();
    }

    /// <summary>
    /// Get the whole string but the part to remove
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="dataToRemove">The string to remove</param>
    /// <returns>The cleaned string</returns>
    public string Except(string dataToRemove) {
      return source.AsSpan().Except(dataToRemove.AsSpan()).ToString();
    }

    /// <summary>
    /// Gets the portion of the string after a given string
    /// </summary>
    /// <param name="firstDelimiter">The first string to search for</param>
    /// <param name="secondDelimiter">The second string to search for</param>
    /// <param name="stringComparison">The culture to find delimiter (useful for ignoring case)</param>
    /// <returns>The selected portion of the string between the delimiters</returns>
    public string Between(string firstDelimiter = "[", string secondDelimiter = "]", StringComparison stringComparison = StringComparison.CurrentCulture) {
      return source.AsSpan().After(firstDelimiter.AsSpan(), stringComparison).Before(secondDelimiter.AsSpan(), stringComparison).ToString();
    }

    /// <summary>
    /// Gets the portion of the string between two given chars
    /// </summary>
    /// <param name="firstDelimiter">The first char to search for</param>
    /// <param name="secondDelimiter">The second char to search for</param>
    /// <returns>The selected portion of the string between the delimiters</returns>
    public string Between(char firstDelimiter = '[', char secondDelimiter = ']') {
      return source.AsSpan().After(firstDelimiter).Before(secondDelimiter).ToString();
    }

    /// <summary>
    /// Gets the strings between two given strings
    /// </summary>
    /// <param name="firstDelimiter">The first string to search for</param>
    /// <param name="secondDelimiter">The second string to search for</param>
    /// <param name="stringComparison">How to compare</param>
    /// <returns>A list of items found between both delimiters</returns>
    public IEnumerable<string> ItemsBetween(string firstDelimiter = "[", string secondDelimiter = "]", StringComparison stringComparison = StringComparison.CurrentCulture) {
      #region Validate parameters
      if (string.IsNullOrEmpty(source)) {
        yield break;
      }
      if (firstDelimiter == "") {
        yield break;
      }
      if (secondDelimiter == "") {
        yield break;
      }
      #endregion Validate parameters

      string ProcessedString = source;

      while (ProcessedString.Length != 0 && ProcessedString.Contains(firstDelimiter, stringComparison) && ProcessedString.Contains(secondDelimiter, stringComparison)) {
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
    public IEnumerable<string> ItemsBetween(char firstDelimiter = '[', char secondDelimiter = ']') {
      #region Validate parameters
      if (string.IsNullOrEmpty(source)) {
        yield break;
      }
      #endregion Validate parameters

      string ProcessedString = source;

      while (ProcessedString != "" && ProcessedString.Contains(firstDelimiter) && ProcessedString.Contains(secondDelimiter)) {
        yield return ProcessedString.After(firstDelimiter).Before(secondDelimiter);
        ProcessedString = ProcessedString.After(secondDelimiter);
      }

      yield break;

    }

    /// <summary>
    /// Get a list of strings from one big string splitted
    /// </summary>
    /// <param name="delimiter">The delimmiter</param>
    /// <param name="stringSplitOptions">The options to split</param>
    /// <returns>A list of strings</returns>
    public IEnumerable<string> GetItems(string delimiter = ";", StringSplitOptions stringSplitOptions = StringSplitOptions.None) {
      #region === Validate parameters ===
      if (string.IsNullOrEmpty(source)) {
        yield break;
      }
      if (delimiter == "") {
        yield return source;
      }
      #endregion === Validate parameters ===
      foreach (string SplitItem in source.Split(delimiter, stringSplitOptions)) {
        yield return SplitItem;
      }
    }

    /// <summary>
    /// Get all the tags between &lt; and &gt;
    /// </summary>
    /// <returns>An list of strings</returns>
    public IEnumerable<string> GetXmlTags() {
      return source.ItemsBetween('<', '>');
    }

    /// <summary>
    /// Capitalize the first letter of each word and uncapitalize other chars
    /// </summary>
    /// <param name="delimiter"></param>
    /// <returns>The proper string</returns>
    public string Proper(char delimiter = ' ') {
      if (string.IsNullOrWhiteSpace(source)) {
        return "";
      }

      StringBuilder RetVal = new StringBuilder();

      string[] Words = source.Split(delimiter);
      foreach (string WordItem in Words) {
        RetVal.Append($"{WordItem.Left(1).ToUpper()}{WordItem[1..].ToLower()}{delimiter}");
      }
      RetVal.Truncate(1);

      return RetVal.ToString();
    }

    /// <summary>
    /// Removes external quotes from a string (ex. "\"MyString\"" => "MyString")
    /// </summary>
    /// <returns>The string without inner quotes</returns>
    public string RemoveExternalQuotes() {
      return source.AsSpan().RemoveExternalQuotes().ToString();
    }

    /// <summary>
    /// Get a new string surrounded by double quotes
    /// </summary>
    /// <returns>a new string surrounded by double quotes, or null if the source is null</returns>
    public string WithQuotes() {
      return $"\"{source}\"";
    }

    /// <summary>
    /// Get a new string surrounded by single quotes (eg. for SQL)
    /// </summary>
    /// <returns>a new string surrounded by single quotes, or null if the source is null</returns>
    public string WithSingleQuotes() {
      return $"'{source}'";
    }

    /// <summary>
    /// Get a new string with control chars replaced by escape sequence
    /// </summary>
    /// <returns>a new string with control chars replaced by escape sequence</returns>
    public string ReplaceControlChars() {
      #region === Validate parameters ===
      if (string.IsNullOrWhiteSpace(source)) {
        return "";
      }
      #endregion === Validate parameters ===

      StringBuilder RetVal = new StringBuilder(source.Length);

      int i = 0;
      int SourceLength = source.Length;
      bool InQuotes = false;
      bool NextCharIsControlChar = false;

      while (i < SourceLength) {

        char CurrentChar = source[i];

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
    /// <param name="width">The width of the result string</param>
    /// <param name="filler">The filler for the missing characters</param>
    /// <returns></returns>
    public string AlignedRight(int width, char filler = ' ') {
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
    public string AlignedLeft(int width, char filler = ' ') {
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
    public string AlignedCenter(int width, char filler = ' ') {
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
    /// Indicates if a string is in a list of string, using CultureInvariantIgnoreCase
    /// </summary>
    /// <param name="listOfMatches">The list of strings to compare with</param>
    /// <param name="match">How the match if done (default to an exact match)</param>
    /// <returns>true if the string is in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
    public bool IsIn(IEnumerable<string> listOfMatches, EStringMatch match = EStringMatch.ExactMatch) => source.IsIn(listOfMatches, StringComparison.InvariantCultureIgnoreCase, match);


    /// <summary>
    /// Indicates if a string is in a list of string
    /// </summary>
    /// <param name="source">The string to look for</param>
    /// <param name="listOfMatches">The list of strings to compare with</param>
    /// <param name="comparison">How the comparison is done</param>
    /// <param name="match">How the match if done (default to an exact match)</param>
    /// <returns>true if the string is in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
    public bool IsIn(IEnumerable<string> listOfMatches, StringComparison comparison, EStringMatch match = EStringMatch.ExactMatch) {
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
    public bool IsNotIn(IEnumerable<string> listOfMatches, EStringMatch match = EStringMatch.ExactMatch) => source.IsNotIn(listOfMatches, StringComparison.InvariantCultureIgnoreCase, match);

    /// <summary>
    /// Indicates if a string is NOT in a list of string
    /// </summary>
    /// <param name="source">The string to look for</param>
    /// <param name="listOfMatches">The list of strings to compare with</param>
    /// <param name="comparison">How the comparison is done</param>
    /// <param name="match">How the match if done (default to an exact match)</param>
    /// <returns>true if the string is not in the list, false otherwise. If any of the source or the list is null/empty, false</returns>
    public bool IsNotIn(IEnumerable<string> listOfMatches, StringComparison comparison, EStringMatch match = EStringMatch.ExactMatch) {
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
}
