namespace BLTools.Core.Text;

public class TTextBoxBorder {

  public char TopLeft { get; set; } = ' ';
  public char TopRight { get; set; } = ' ';
  public char BottomLeft { get; set; } = ' ';
  public char BottomRight { get; set; } = ' ';
  public char Horizontal { get; set; } = ' ';
  public char Vertical { get; set; } = ' ';

  public static TTextBoxBorder Standard => new TTextBoxBorder {
    TopLeft = '+',
    TopRight = '+',
    BottomLeft = '+',
    BottomRight = '+',
    Horizontal = '-',
    Vertical = '|'
  };

  public static TTextBoxBorder Ibm => new TTextBoxBorder {
    TopLeft = '┌',
    TopRight = '┐',
    BottomLeft = '└',
    BottomRight = '┘',
    Horizontal = '─',
    Vertical = '│'
  };

  public static TTextBoxBorder IbmDouble => new TTextBoxBorder {
    TopLeft = '╔',
    TopRight = '╗',
    BottomLeft = '╚',
    BottomRight = '╝',
    Horizontal = '═',
    Vertical = '║'
  };
}
