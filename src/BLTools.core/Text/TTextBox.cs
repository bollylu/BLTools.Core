namespace BLTools.Core.Text;
public class TTextBox : ITextBox {

  #region --- Constants --------------------------------------------
  internal const char CHAR_CR = '\r';
  internal const char CHAR_LF = '\n';
  internal const string CRLF = "\r\n";
  internal const string NEWLINE = "\n";
  #endregion --- Constants --------------------------------------------

  public TTextBoxOptions Options { get; } = TTextBoxOptions.Default;

  public string Title { get; set; } = "";

  public string Content { get; set; } = "";

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  public TTextBox() { }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public string Render() {
    string PreProcessedSourceString = Content.Replace(CRLF, NEWLINE);
    IEnumerable<string> SourceStringLines = PreProcessedSourceString.Split(CHAR_LF);

    StringBuilder RetVal = new StringBuilder();

    if (Options.IsDynamicWidth) {
      // Find largest string
      int MaxLength = SourceStringLines.Max(x => x.Length);

      if (Title.IsEmpty()) {
        RetVal.Append(Options.Border.TopLeft);
        RetVal.Append(Options.Border.Horizontal.Repeat(MaxLength + (Options.Margin * 2) + 2));
        RetVal.Append(Options.Border.TopRight);
        RetVal.AppendLine();
      } else {
        RetVal.Append(Options.Border.TopLeft);
        RetVal.Append($"{Options.Border.Horizontal}[");
        RetVal.Append(Title);
        RetVal.Append(']');
        RetVal.Append(Options.Border.Horizontal.Repeat(MaxLength + (Options.Margin * 2) - 1 - Title.Length));
        RetVal.Append(Options.Border.TopRight);
        RetVal.AppendLine();
      }

      foreach (string StringItem in SourceStringLines) {
        int LeftPadding = 0;
        int RightPadding = 0;

        switch (Options.Alignment) {
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

        RetVal.Append(Options.Border.Vertical);
        RetVal.Append(Options.Filler.Repeat(Options.Margin));
        RetVal.Append(Options.Filler.Repeat(LeftPadding));
        RetVal.Append(StringItem);
        RetVal.Append(Options.Filler.Repeat(RightPadding));
        RetVal.Append(Options.Filler.Repeat(Options.Margin));
        RetVal.Append(Options.Border.Vertical);
        RetVal.AppendLine();
      }

      RetVal.Append(Options.Border.BottomLeft);
      RetVal.Append(Options.Border.Horizontal.Repeat(MaxLength + (Options.Margin * 2) + 2));
      RetVal.Append(Options.Border.BottomRight);

      return RetVal.ToString();

    } else {

      int InnerWidth = Options.Width - (Options.Margin * 2);

      if (Title.IsEmpty()) {
        RetVal.Append(Options.Border.TopLeft);
        RetVal.Append(Options.Border.Horizontal.Repeat(InnerWidth + Options.Margin * 2));
        RetVal.Append(Options.Border.TopRight);
        RetVal.AppendLine();
      } else {
        string DisplayTitle = Title.Left(InnerWidth - 2);
        RetVal.Append(Options.Border.TopLeft);
        RetVal.Append(Options.Border.Horizontal);
        RetVal.Append('[');
        RetVal.Append(DisplayTitle);
        RetVal.Append(']');
        RetVal.Append(Options.Border.Horizontal.Repeat(InnerWidth - DisplayTitle.Length - 3));
        RetVal.Append(Options.Border.TopRight);
        RetVal.AppendLine();
      }

      foreach (string StringItem in SourceStringLines) {

        int StartPtr = 0;
        while (StartPtr < StringItem.Length) {
          string WorkString = StringItem.Substring(StartPtr, Math.Min(StringItem.Length - StartPtr, InnerWidth));
          StartPtr += WorkString.Length;

          RetVal.Append(Options.Border.Vertical);
          RetVal.Append(Options.Filler.Repeat(Options.Margin));

          switch (Options.Alignment) {
            case ETextBoxAlignment.Left:
              RetVal.Append(WorkString);
              RetVal.Append(Options.Filler.Repeat(Math.Max(InnerWidth - WorkString.Length, 0)));
              break;

            case ETextBoxAlignment.Right:
              RetVal.Append(Options.Filler.Repeat(Math.Max(InnerWidth - WorkString.Length, 0)));
              RetVal.Append(WorkString);
              break;

            case ETextBoxAlignment.Center:
              int LeftPadding = Math.Max(Convert.ToInt32(Math.Floor((InnerWidth - WorkString.Length) / 2d)), 0);
              int RightPadding = Math.Max(InnerWidth - WorkString.Length - LeftPadding, 0);
              RetVal.Append(Options.Filler.Repeat(LeftPadding));
              RetVal.Append(WorkString);
              RetVal.Append(Options.Filler.Repeat(RightPadding));
              break;
          }

          RetVal.Append(Options.Filler.Repeat(Options.Margin));
          RetVal.Append(Options.Border.Vertical);
          RetVal.AppendLine();

        }
      }

      RetVal.Append(Options.Border.BottomLeft);
      RetVal.Append(Options.Border.Horizontal.Repeat(InnerWidth + Options.Margin * 2));
      RetVal.Append(Options.Border.BottomRight);

      return RetVal.ToString();
    }
  }
}
