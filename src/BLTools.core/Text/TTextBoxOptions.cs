namespace BLTools.Core.Text;

public class TTextBoxOptions {

  public const int DEFAULT_WIDTH = 80;
  public const int DEFAULT_MARGIN = 0;
  private const char DEFAULT_FILLER = ' ';
  private const ETextBoxAlignment DEFAULT_ALIGNMENT = ETextBoxAlignment.Left;
  private const ETextBoxType DEFAULT_TEXTBOX_TYPE = ETextBoxType.Standard;
  private static readonly TTextBoxBorder DEFAULT_BORDER = TTextBoxBorder.Standard;
  private const bool DEFAULT_IS_DYNAMIC_WIDTH = false;

  public int Width { get; set => field = value.WithinLimits(10, 512); } = DEFAULT_WIDTH;

  public int Margin { get; set => field = value.WithinLimits(0, 20); } = DEFAULT_MARGIN;

  public char Filler { get; set; } = DEFAULT_FILLER;

  public ETextBoxAlignment Alignment { get; set; } = DEFAULT_ALIGNMENT;

  public ETextBoxType TextBoxType { get; set; } = DEFAULT_TEXTBOX_TYPE;

  public TTextBoxBorder Border { get; set; } = DEFAULT_BORDER;

  public bool IsDynamicWidth { get; set; } = DEFAULT_IS_DYNAMIC_WIDTH;

  public static TTextBoxOptions Default => new TTextBoxOptions() { };
}
