namespace BLTools.Core.Text;
public static class TTextHRow {

  public const int DEFAULT_FIXED_WIDTH = 80;

  static private readonly Dictionary<EHorizontalRowType, char> _CharFinder = new Dictionary<EHorizontalRowType, char> {
    { EHorizontalRowType.Single, '-' },
    { EHorizontalRowType.Double, '=' },
    { EHorizontalRowType.Dot, '.' },
    { EHorizontalRowType.Underline, '_' },
    { EHorizontalRowType.Stars, '*' },
    { EHorizontalRowType.FullLight, '░' },
    { EHorizontalRowType.FullMedium, '▒' },
    { EHorizontalRowType.FullBold, '▓' },
    { EHorizontalRowType.Solid, '█' },
    { EHorizontalRowType.SingleIBM, '─' },
    { EHorizontalRowType.SingleIBMBold, '━' },
    { EHorizontalRowType.DoubleIBM, '═' },
    { EHorizontalRowType.Slash, '/' },
    { EHorizontalRowType.Backslash, '\\' },
    { EHorizontalRowType.Pipe, '|' }
  };


  /// <summary>
  /// Build a string figuring an horizontal line. The length of the line is current console width.
  /// </summary>
  /// <returns>A string containing the horizontal line</returns>
  public static string BuildHorizontalRow() {
    return BuildHorizontalRow(-1, EHorizontalRowType.Single);
  }

  /// <summary>
  /// Build a string figuring an horizontal line. The length of the line is current console width.
  /// </summary>
  /// <param name="rowType">The type of char to use for the drawing</param>
  /// <returns>A string containing the horizontal line</returns>
  public static string BuildHorizontalRow(EHorizontalRowType rowType = EHorizontalRowType.Single) {
    return BuildHorizontalRow(-1, rowType);
  }

  /// <summary>
  /// Build a string figuring an horizontal line.
  /// </summary>
  /// <param name="width">The length of the line. -1 means current console width</param>
  /// <param name="rowType">The type of char to use for the drawing</param>
  /// <returns>A string containing the horizontal line</returns>
  public static string BuildHorizontalRow(int width, EHorizontalRowType rowType = EHorizontalRowType.Single) {

    if (width == -1) {
      width = Console.WindowWidth;
    }

    if (width <= 0) {
      width = DEFAULT_FIXED_WIDTH;
    }

    return _CharFinder[rowType].Repeat(width);

  }

  /// <summary>
  /// Build a string figuring an horizontal line, with a message in it.  The length of the line is current console width.
  /// </summary>
  /// <param name="message">The text message</param>
  /// <param name="rowType">The type of char to use for the drawing</param>
  /// <returns>A string containing the horizontal line with the message embedded</returns>
  public static string BuildHorizontalRowWithText(string message, EHorizontalRowType rowType = EHorizontalRowType.Single) {
    return BuildHorizontalRowWithText(message, -1, rowType);
  }

  /// <summary>
  /// Build a string figuring an horizontal line, with a message in it
  /// </summary>
  /// <param name="message">The text message</param>
  /// <param name="width">The length of the line. -1 means current console width. If the message is bigger than the width, it is truncated.</param>
  /// <param name="rowType">The type of char to use for the drawing</param>
  /// <returns>A string containing the horizontal line with the message embedded</returns>
  public static string BuildHorizontalRowWithText(string message, int width = -1, EHorizontalRowType rowType = EHorizontalRowType.Single) {

    if (width == -1) {
      width = Console.WindowWidth;
    }

    if (width <= 0) {
      width = DEFAULT_FIXED_WIDTH;
    }

    int SourceStringLength = message.Length;

    if (width < SourceStringLength) {
      width = SourceStringLength;
    }

    int BeforeText = 2;
    int AfterText = Math.Max(0, width - BeforeText - SourceStringLength - 2);

    StringBuilder RetVal = new StringBuilder();
    RetVal.Append(_CharFinder[rowType].Repeat(BeforeText));
    RetVal.Append(message);
    RetVal.Append(_CharFinder[rowType].Repeat(AfterText));

    return RetVal.ToString().Left(width);

  }

}
