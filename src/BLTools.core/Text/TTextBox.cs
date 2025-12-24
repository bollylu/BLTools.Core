namespace BLTools.Core.Text;

public class TTextBox : ITextBox {

  #region --- Constants --------------------------------------------
  private const char CHAR_CR = '\r';
  private const char CHAR_LF = '\n';
  private const string CRLF = "\r\n";
  private const string NEWLINE = "\n";
  #endregion --- Constants

  public TTextBoxOptions Options { get; } = TTextBoxOptions.Default;
  public string Title { get; set; } = string.Empty;
  public string Content { get; set; } = string.Empty;

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  public TTextBox() { }
  #endregion --- Constructor(s) ------

  public string Render() {
    var preprocessedContent = Content.Replace(CRLF, NEWLINE);
    var sourceLines = preprocessedContent.Split(CHAR_LF);

    return Options.IsDynamicWidth
      ? RenderDynamicWidth(sourceLines)
      : RenderFixedWidth(sourceLines);
  }

  private string RenderDynamicWidth(ReadOnlySpan<string> lines) {
    var buffer = new StringBuilder();

    int maxLength = GetMaxLineLength(lines);

    // Render top border
    AppendTopBorder(ref buffer, maxLength);

    // Render content
    foreach (var line in lines) {
      AppendContentLine(ref buffer, line, maxLength);
    }

    // Render bottom border
    AppendBottomBorder(ref buffer, maxLength);

    return buffer.ToString();
  }

  private string RenderFixedWidth(ReadOnlySpan<string> lines) {
    var buffer = new StringBuilder();

    int innerWidth = Options.Width - (Options.Margin * 2);

    // Render top border
    AppendTopBorderFixed(ref buffer, innerWidth);

    // Render content
    foreach (var line in lines) {
      AppendContentLineFixed(ref buffer, line, innerWidth);
    }

    // Render bottom border
    AppendBottomBorderFixed(ref buffer, innerWidth);

    return buffer.ToString();
  }

  #region --- Dynamic Width Helpers -------

  private static int GetMaxLineLength(ReadOnlySpan<string> lines) {
    int maxLength = 0;
    foreach (var line in lines) {
      if (line.Length > maxLength) {
        maxLength = line.Length;
      }
    }
    return maxLength;
  }

  private void AppendTopBorder(ref StringBuilder buffer, int maxLength) {

    buffer.Append(Options.Border.TopLeft);

    if (string.IsNullOrEmpty(Title)) {
      buffer.Append(Options.Border.Horizontal.Repeat(maxLength + (Options.Margin * 2) + 2));
    } else {
#if NET8_0_OR_GREATER
      buffer.Append($"{Options.Border.Horizontal}[{Title}]");
      int remainingLength = maxLength + (Options.Margin * 2) - 1 - Title.Length;
      buffer.Append(Options.Border.Horizontal.Repeat(Math.Max(remainingLength, 0)));
#else
      buffer.Append(Options.Border.Horizontal);
      buffer.Append('[');
      buffer.Append(Title);
      buffer.Append(']');
      buffer.Append(Options.Border.Horizontal.Repeat(maxLength + (Options.Margin * 2) - 1 - Title.Length));
#endif
    }

    buffer.Append(Options.Border.TopRight);
    buffer.AppendLine();
  }

  private void AppendContentLine(ref StringBuilder buffer, string line, int maxLength) {

    var (leftPadding, rightPadding) = CalculatePadding(Options.Alignment, line.Length, maxLength);

    buffer.Append(Options.Border.Vertical);
    buffer.Append(Options.Filler.Repeat(Options.Margin));
    buffer.Append(Options.Filler.Repeat(leftPadding));
    buffer.Append(line);
    buffer.Append(Options.Filler.Repeat(rightPadding));
    buffer.Append(Options.Filler.Repeat(Options.Margin));
    buffer.Append(Options.Border.Vertical);
    buffer.AppendLine();
  }

  private void AppendBottomBorder(ref StringBuilder buffer, int maxLength) {

    buffer.Append(Options.Border.BottomLeft);
    buffer.Append(Options.Border.Horizontal.Repeat(maxLength + (Options.Margin * 2) + 2));
    buffer.Append(Options.Border.BottomRight);
  }

  #endregion

  #region --- Fixed Width Helpers -------

  private void AppendTopBorderFixed(ref StringBuilder buffer, int innerWidth) {

    buffer.Append(Options.Border.TopLeft);

    if (string.IsNullOrEmpty(Title)) {
      buffer.Append(Options.Border.Horizontal.Repeat(innerWidth + (Options.Margin * 2)));
    } else {
      string displayTitle = Title.Left(innerWidth - 2);
      int remainingLength = Math.Max(innerWidth - displayTitle.Length - 3, 0);

      buffer.Append(Options.Border.Horizontal);
      buffer.Append('[');
      buffer.Append(displayTitle);
      buffer.Append(']');
      buffer.Append(Options.Border.Horizontal.Repeat(remainingLength));
    }

    buffer.Append(Options.Border.TopRight);
    buffer.AppendLine();
  }

  private void AppendContentLineFixed(ref StringBuilder buffer, string line, int innerWidth) {

    int startPtr = 0;

    while (startPtr < line.Length) {
      int chunkLength = Math.Min(line.Length - startPtr, innerWidth);
      string workString = line.Substring(startPtr, chunkLength);
      startPtr += chunkLength;

      buffer.Append(Options.Border.Vertical);
      buffer.Append(Options.Filler.Repeat(Options.Margin));

      var (leftPadding, rightPadding) = CalculatePadding(Options.Alignment, workString.Length, innerWidth);

      buffer.Append(Options.Filler.Repeat(leftPadding));
      buffer.Append(workString);
      buffer.Append(Options.Filler.Repeat(rightPadding));

      buffer.Append(Options.Filler.Repeat(Options.Margin));
      buffer.Append(Options.Border.Vertical);
      buffer.AppendLine();
    }
  }

  private void AppendBottomBorderFixed(ref StringBuilder buffer, int innerWidth) {

    buffer.Append(Options.Border.BottomLeft);
    buffer.Append(Options.Border.Horizontal.Repeat(innerWidth + (Options.Margin * 2)));
    buffer.Append(Options.Border.BottomRight);
  }

  #endregion

  #region --- Helpers -------

  private static (int leftPadding, int rightPadding) CalculatePadding(
    ETextBoxAlignment alignment,
    int currentLength,
    int maxLength) {

    if (alignment == ETextBoxAlignment.Left) {
      return (0, Math.Max(maxLength - currentLength, 0));
    }

    if (alignment == ETextBoxAlignment.Right) {
      return (Math.Max(maxLength - currentLength, 0), 0);
    }

    // Center alignment
    int leftPad = Math.Max((int)Math.Floor((maxLength - currentLength) / 2d), 0);
    int rightPad = Math.Max(maxLength - currentLength - leftPad, 0);
    return (leftPad, rightPad);
  }

  #endregion
}