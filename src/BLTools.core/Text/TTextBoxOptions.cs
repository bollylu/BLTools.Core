namespace BLTools.Core.Text;

public class TTextBoxOptions {

  public const int DEFAULT_WIDTH = 80;
  public const int DEFAULT_MARGIN = 0;

  private int _Width = DEFAULT_WIDTH;
  public int Width { get => _Width; set => _Width = value.WithinLimits(10, 512); }

  private int _Margin = DEFAULT_MARGIN;
  public int Margin { get => _Margin; set => _Margin = value.WithinLimits(0, 20); }

  private char _Filler = ' ';
  public char Filler { get => _Filler; set => _Filler = value; }

  private ETextBoxAlignment _Alignment = ETextBoxAlignment.Left;
  public ETextBoxAlignment Alignment { get => _Alignment; set => _Alignment = value; }

  private ETextBoxType _TextBoxType = ETextBoxType.Standard;
  public ETextBoxType TextBoxType { get => _TextBoxType; set => _TextBoxType = value; }

  private TTextBoxBorder _Border = TTextBoxBorder.Standard;
  public TTextBoxBorder Border { get => _Border; set => _Border = value; }

  private bool _IsDynamicWidth = false;
  public bool IsDynamicWidth { get => _IsDynamicWidth; set => _IsDynamicWidth = value; }

  public static TTextBoxOptions Default => new TTextBoxOptions() { };
}
