﻿namespace BLTools.Core.Text;

/// <summary>
/// Extensions to ease the use of TextBox
/// </summary>
public static class TextBoxExtensions {
  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated. The border is filled with IBM boxes characters
  /// </summary>
  /// <param name="source">The message</param>
  /// <param name="margin">The margin around the message within the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <returns>A string containing the message in the box</returns>
  public static string BoxIBM(this string source, int margin = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Left, char filler = ' ') {
    return TextBox.BuildDynamicIBM(source, margin, alignment, filler);
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated.
  /// </summary>
  /// <param name="source">The message</param>
  /// <param name="margin">The margin around the message within the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string containing the message in the box</returns>
  public static string Box(this string source, int margin = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Left, char filler = ' ', string border = "") {
    return TextBox.BuildDynamic(source, margin, alignment, filler, border);
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated.
  /// </summary>
  /// <param name="source">The message</param>
  /// <param name="width">The width of the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string containing the message in the box</returns>
  public static string BoxFixedWidth(this string source, int width = TextBox.DEFAULT_FIXED_WIDTH, ETextBoxAlignment alignment = ETextBoxAlignment.Left, char filler = ' ', string border = "") {
    return TextBox.BuildFixedWidth(source, width, alignment, filler, border);
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is dynamically calculated. The title is in the border
  /// </summary>
  /// <param name="source">The message</param>
  /// <param name="title">The title</param>
  /// <param name="margin">The margin around the message within the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string containing the message in the box</returns>
  public static string Box(this string source, string title, int margin = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Left, char filler = ' ', string border = "") {
    return TextBox.BuildDynamicWithTitle(source, title, margin, alignment, filler, border);
  }

  /// <summary>
  /// Generate a box with the message inside it. The width of the box is fixed. The title is in the border
  /// </summary>
  /// <param name="source">The message</param>
  /// <param name="title">The title</param>
  /// <param name="width">The width of the box</param>
  /// <param name="alignment">The alignment of the message within the box</param>
  /// <param name="filler">The character used to full the extra space aound the message within the box</param>
  /// <param name="border">The border string (top-left/top/top-right/right/bottom-right/bottom/bottom-left/left)</param>
  /// <returns>A string containing the message in the box</returns>
  public static string BoxFixedWidth(this string source, string title, int width = 0, ETextBoxAlignment alignment = ETextBoxAlignment.Left, char filler = ' ', string border = "") {
    return TextBox.BuildFixedWidth(source, title, width, alignment, filler, border);

  }
}
