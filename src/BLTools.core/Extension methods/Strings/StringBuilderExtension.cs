namespace BLTools.Core;

/// <summary>
/// Extensions for StringBuilder
/// </summary>
public static class StringBuilderExtensions {

  private const string CRLF = "\r\n";
  private const char TAB = '\t';
  private const char SPACE = ' ';
  extension(StringBuilder source) {

    /// <summary>
    /// Removes n characters from the end of the StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <param name="length">The amount of character(s) to remove</param>
    /// <returns></returns>
    public StringBuilder Truncate(int length) {
      if (length <= 0) {
        return source;
      }
      if (length >= source.Length) {
        return source.Clear();
      }

      return source.Remove(source.Length - length, length);
    }



    /// <summary>
    /// Remove trailing spaces or tabs from StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <returns></returns>
    public StringBuilder Trim() {
      return source.Trim(SPACE, TAB);
    }



    /// <summary>
    /// Remove leading spaces or tabs from StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <returns></returns>
    public StringBuilder TrimLeft() {
      return source.TrimLeft(SPACE, TAB);
    }



    /// <summary>
    /// Remove leading spaces or tabs from StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <returns></returns>
    public StringBuilder TrimAll() {
      return source.TrimAll(SPACE, TAB);
    }



    /// <summary>
    /// Remove trailing characters from StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <param name="chars">The characters to remove</param>
    /// <returns></returns>
    public StringBuilder Trim(params char[] chars) {
      if (source.Length == 0) {
        return source;
      }
      if (chars is null || chars.Length == 0) {
        return source;
      }

      while (chars.Contains(source[^1])) {
        source = source.Remove(source.Length - 1, 1);
      }

      return source;
    }



    /// <summary>
    /// Remove leading characters from StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <param name="chars">The characters to remove</param>
    /// <returns></returns>
    public StringBuilder TrimLeft(params char[] chars) {
      if (source.Length == 0) {
        return source;
      }
      if (chars is null || chars.Length == 0) {
        return source;
      }

      while (chars.Contains(source[0])) {
        source = source.Remove(0, 1);
      }

      return source;
    }



    /// <summary>
    /// Remove leading and trailing characters from StringBuilder
    /// </summary>
    /// <param name="source">The string builder</param>
    /// <param name="chars">The characters to remove</param>
    /// <returns></returns>
    public StringBuilder TrimAll(params char[] chars) {

      if (source.Length == 0) {
        return source;
      }
      if (chars is null || chars.Length == 0) {
        return source;
      }

      while (chars.Contains(source[^1])) {
        source = source.Remove(source.Length - 1, 1);
      }

      while (chars.Contains(source[0])) {
        source = source.Remove(0, 1);
      }

      return source;
    }
  }
  extension(StringBuilder builder) {

    /// <summary>
    /// Append some string to the string builder with an indentation
    /// </summary>
    /// <param name="builder">The StringBuilder source</param>
    /// <param name="source">The string source</param>
    /// <param name="indent">The indentation</param>
    /// <returns>A StringBuilder</returns>
    public StringBuilder AppendIndent(string source, int indent = 2) {
      string IndentSpace = new string(' ', indent);
      foreach (ReadOnlySpan<char> LineItem in source.Split(CRLF, StringSplitOptions.RemoveEmptyEntries)) {
        builder.AppendLine($"{IndentSpace}{LineItem}");
      }
      return builder;
    }



    /// <summary>
    /// Append some object converted to string to the string builder with an indentation
    /// </summary>
    /// <param name="builder">The StringBuilder source</param>
    /// <param name="source">The object source</param>
    /// <param name="indent">The indentation</param>
    /// <returns>A StringBuilder</returns>
    public StringBuilder AppendIndent(object source, int indent = 2) {
      string IndentSpace = new string(' ', indent);
      string DisplayData = source?.ToString() ?? "";
      foreach (string LineItem in DisplayData.Split(CRLF, StringSplitOptions.RemoveEmptyEntries)) {
        builder.AppendLine($"{IndentSpace}{LineItem}");
      }
      return builder;
    }
  }

}
