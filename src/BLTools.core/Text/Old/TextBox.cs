namespace BLTools.Core.Text;

/// <summary>
/// Helpers and extensions for text formatting
/// </summary>
public static class TextBox {

  #region --- Constants --------------------------------------------
  internal const char CHAR_CR = '\r';
  internal const char CHAR_LF = '\n';
  internal const string CRLF = "\r\n";
  internal const string NEWLINE = "\n";
  #endregion --- Constants --------------------------------------------

  /// <summary>
  /// Default width for text boxes
  /// </summary>
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

  #region --- Boxes --------------------------------------------
  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated. The border is filled with IBM boxes characters
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="margin">The margin around the message within the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <returns>A string containing the message in the box</returns>
  public static string BuildDynamicIBM(string sourceString, int margin = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = '·') {
    return BuildDynamic(sourceString, margin, alignment, filler, "╒═╕│╛═╘│");
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated.
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="margin">The margin around the message within the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string containing the message in the box</returns>
  public static string BuildDynamic(string sourceString, int margin = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = ' ', string border = "") {
    #region Validate parameters
    if (margin < 0) {
      margin = 0;
    }
    #endregion Validate parameters

    string CompletedBorder = $"{border}+-+|+-+|".Left(8);

    char TopLeft = CompletedBorder[0];
    char TopBar = CompletedBorder[1];
    char TopRight = CompletedBorder[2];
    char LeftBar = CompletedBorder[7];
    char RightBar = CompletedBorder[3];
    char BottomLeft = CompletedBorder[6];
    char BottomBar = CompletedBorder[5];
    char BottomRight = CompletedBorder[4];

    StringBuilder RetVal = new StringBuilder();

    string PreProcessedSourceString = sourceString.Replace(CRLF, NEWLINE);

    string[] SourceStringLines = PreProcessedSourceString.Split(CHAR_LF);

    // Find larger string
    int MaxLength = 0;
    foreach (string StringItem in SourceStringLines) {
      if (StringItem.Length > MaxLength) {
        MaxLength = StringItem.Length;
      }
    }

    RetVal.AppendLine($"{TopLeft}{new string(TopBar, MaxLength + (margin * 2) + 2)}{TopRight}");

    foreach (string StringItem in SourceStringLines) {
      int LeftPadding = 0;
      int RightPadding = 0;

      switch (alignment) {
        case ETextBoxAlignment.Left:
          RightPadding = MaxLength - StringItem.Length;
          break;
        case ETextBoxAlignment.Right:
          LeftPadding = MaxLength - StringItem.Length;
          break;
        case ETextBoxAlignment.Center:
          LeftPadding = Convert.ToInt32(Math.Floor((MaxLength - StringItem.Length) / 2d));
          RightPadding = MaxLength - StringItem.Length - LeftPadding;
          break;
      }

      RetVal.AppendLine($"{LeftBar}{new string(filler, margin)}{new string(filler, LeftPadding)} {StringItem} {new string(filler, RightPadding)}{new string(filler, margin)}{RightBar}");
    }
    RetVal.Append($"{BottomLeft}{new string(BottomBar, MaxLength + (margin * 2) + 2)}{BottomRight}");

    return RetVal.ToString();
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated.
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="title">The optional title of the box</param>
  /// <param name="margin">The margin around the message within the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string containing the message in the box</returns>
  public static string BuildDynamicWithTitle(string sourceString, string title, int margin = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = ' ', string border = "") {
    #region Validate parameters
    title ??= "";

    if (margin < 0) {
      margin = 0;
    }
    #endregion Validate parameters

    string CompletedBorder = $"{border}+-+|+-+|".Left(8);

    char TopLeft = CompletedBorder[0];
    char TopBar = CompletedBorder[1];
    char TopRight = CompletedBorder[2];
    char LeftBar = CompletedBorder[7];
    char RightBar = CompletedBorder[3];
    char BottomLeft = CompletedBorder[6];
    char BottomBar = CompletedBorder[5];
    char BottomRight = CompletedBorder[4];

    StringBuilder RetVal = new StringBuilder();

    string PreProcessedSourceString = sourceString.Replace(CRLF, NEWLINE);

    string[] SourceStringLines = PreProcessedSourceString.Split(CHAR_LF);

    // Find larger string
    int MaxLength = 0;
    foreach (string StringItem in SourceStringLines) {
      if (StringItem.Length > MaxLength) {
        MaxLength = StringItem.Length;
      }
    }

    if (title.Length > MaxLength + margin * 2 - 2) {
      title = title.Left(MaxLength + margin * 2 - 2);
    }

    if (title.IsEmpty()) {
      RetVal.AppendLine($"{TopLeft}{new string(TopBar, MaxLength + (margin * 2) + 2)}{TopRight}");
    } else {
      RetVal.Append(TopLeft);
      RetVal.Append($"{TopBar}[");
      RetVal.Append(title);
      RetVal.Append(']');
      RetVal.Append(new string(TopBar, MaxLength + (margin * 2) - 1 - title.Length));
      RetVal.Append(TopRight);
      RetVal.AppendLine();
    }

    foreach (string StringItem in SourceStringLines) {
      int LeftPadding = 0;
      int RightPadding = 0;

      switch (alignment) {
        case ETextBoxAlignment.Left:
          RightPadding = MaxLength - StringItem.Length;
          break;
        case ETextBoxAlignment.Right:
          LeftPadding = MaxLength - StringItem.Length;
          break;
        case ETextBoxAlignment.Center:
          LeftPadding = Convert.ToInt32(Math.Floor((MaxLength - StringItem.Length) / 2d));
          RightPadding = MaxLength - StringItem.Length - LeftPadding;
          break;
      }

      RetVal.AppendLine($"{LeftBar}{new string(filler, margin)}{new string(filler, LeftPadding)} {StringItem} {new string(filler, RightPadding)}{new string(filler, margin)}{RightBar}");
    }
    RetVal.Append($"{BottomLeft}{new string(BottomBar, MaxLength + (margin * 2) + 2)}{BottomRight}");

    return RetVal.ToString();
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is DEFAULT_FIXED_WIDTH. If the message is larger than the box, it is split in several lines
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string with the box and the message</returns>
  public static string BuildFixedWidth(string sourceString, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = ' ', string border = "") {
    return BuildFixedWidth(sourceString, DEFAULT_FIXED_WIDTH, alignment, filler, border);
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is fixed. If the message is larger than the box, it is split in several lines
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="width">The width of the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string with the box and the message</returns>
  public static string BuildFixedWidth(string sourceString, int width, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = ' ', string border = "") {
    #region Validate parameters
    if (width <= 0) {
      width = DEFAULT_FIXED_WIDTH;
    }
    #endregion Validate parameters

    string CompletedBorder = $"{border}+-+|+-+|";

    char TopLeft = CompletedBorder[0];
    char TopBar = CompletedBorder[1];
    char TopRight = CompletedBorder[2];
    char LeftBar = CompletedBorder[7];
    char RightBar = CompletedBorder[3];
    char BottomLeft = CompletedBorder[6];
    char BottomBar = CompletedBorder[5];
    char BottomRight = CompletedBorder[4];

    int InnerWidth = width - 4;
    string PreProcessedSourceString = sourceString.Replace(CRLF, NEWLINE);

    StringBuilder RetVal = new StringBuilder();

    RetVal.AppendLine($"{TopLeft}{new string(TopBar, InnerWidth + 2)}{TopRight}");

    foreach (string StringItem in PreProcessedSourceString.Split(CHAR_LF)) {
      int StartPtr = 0;
      while (StartPtr < StringItem.Length) {
        string WorkString = StringItem.Substring(StartPtr, Math.Min(StringItem.Length - StartPtr, InnerWidth));
        StartPtr += WorkString.Length;

        int LeftPadding = 0;
        int RightPadding = 0;

        switch (alignment) {
          case ETextBoxAlignment.Left:
            RightPadding = Math.Max(InnerWidth - WorkString.Length, 0);
            RetVal.AppendLine($"{LeftBar} {WorkString} {new string(filler, RightPadding)}{RightBar}");
            break;
          case ETextBoxAlignment.Right:
            LeftPadding = Math.Max(InnerWidth - WorkString.Length, 0);
            RetVal.AppendLine($"{LeftBar}{new string(filler, LeftPadding)} {WorkString} {RightBar}");
            break;
          case ETextBoxAlignment.Center:
            LeftPadding = Math.Max(Convert.ToInt32(Math.Floor((InnerWidth - WorkString.Length) / 2d)), 0);
            RightPadding = Math.Max(InnerWidth - WorkString.Length - LeftPadding, 0);
            RetVal.AppendLine($"{LeftBar}{new string(filler, LeftPadding)} {WorkString} {new string(filler, RightPadding)}{RightBar}");
            break;
        }


      }
    }
    RetVal.Append($"{BottomLeft}{new string(BottomBar, InnerWidth + 2)}{BottomRight}");
    return RetVal.ToString();
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is fixed. If the message is larger than the box, it is split in several lines
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="title">The title</param>
  /// <param name="width">The width of the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string with the box and the message</returns>
  public static string BuildFixedWidth(string sourceString, string title, int width, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = ' ', string border = "") {
    #region Validate parameters
    if (title is null) {
      title = "";
    }

    if (width <= 0) {
      width = DEFAULT_FIXED_WIDTH;
    }
    #endregion Validate parameters

    string CompletedBorder = $"{border}+-+|+-+|";

    char TopLeft = CompletedBorder[0];
    char TopBar = CompletedBorder[1];
    char TopRight = CompletedBorder[2];
    char LeftBar = CompletedBorder[7];
    char RightBar = CompletedBorder[3];
    char BottomLeft = CompletedBorder[6];
    char BottomBar = CompletedBorder[5];
    char BottomRight = CompletedBorder[4];

    int InnerWidth = width - 4;
    string PreProcessedSourceString = sourceString.Replace(CRLF, NEWLINE);

    StringBuilder RetVal = new StringBuilder();

    if (title.Length > InnerWidth - 2) {
      title = title.Left(InnerWidth - 2);
    }

    if (title.IsEmpty()) {
      RetVal.AppendLine($"{TopLeft}{new string(TopBar, InnerWidth + 2)}{TopRight}");
    } else {
      RetVal.Append(TopLeft);
      RetVal.Append($"{TopBar}[");
      RetVal.Append(title);
      RetVal.Append(']');
      RetVal.Append(new string(TopBar, InnerWidth - title.Length - 1));
      RetVal.Append(TopRight);
      RetVal.AppendLine();
    }

    foreach (string StringItem in PreProcessedSourceString.Split(CHAR_LF)) {
      int StartPtr = 0;
      while (StartPtr < StringItem.Length) {
        string WorkString = StringItem.Substring(StartPtr, Math.Min(StringItem.Length - StartPtr, InnerWidth));
        StartPtr += WorkString.Length;

        int LeftPadding = 0;
        int RightPadding = 0;

        switch (alignment) {
          case ETextBoxAlignment.Left:
            RightPadding = Math.Max(InnerWidth - WorkString.Length, 0);
            RetVal.AppendLine($"{LeftBar} {WorkString} {new string(filler, RightPadding)}{RightBar}");
            break;
          case ETextBoxAlignment.Right:
            LeftPadding = Math.Max(InnerWidth - WorkString.Length, 0);
            RetVal.AppendLine($"{LeftBar}{new string(filler, LeftPadding)} {WorkString} {RightBar}");
            break;
          case ETextBoxAlignment.Center:
            LeftPadding = Math.Max(Convert.ToInt32(Math.Floor((InnerWidth - WorkString.Length) / 2d)), 0);
            RightPadding = Math.Max(InnerWidth - WorkString.Length - LeftPadding, 0);
            RetVal.AppendLine($"{LeftBar}{new string(filler, LeftPadding)} {WorkString} {new string(filler, RightPadding)}{RightBar}");
            break;
        }


      }
    }
    RetVal.Append($"{BottomLeft}{new string(BottomBar, InnerWidth + 2)}{BottomRight}");
    return RetVal.ToString();
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is fixed. If the message is larger than the box, it is split in several lines.
  /// IBM characters are used for the border
  /// </summary>
  /// <param name="sourceString">The message</param>
  /// <param name="width">The width of the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <returns>A string with the box and the message</returns>
  public static string BuildFixedWidthIBM(string sourceString, int width = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Center, char filler = '·') {
    return BuildFixedWidth(sourceString, width, alignment, filler, "╒═╕│╛═╘│");
  }

  #endregion --- Boxes --------------------------------------------
}
