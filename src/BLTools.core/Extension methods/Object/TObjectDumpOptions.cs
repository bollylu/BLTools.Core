namespace BLTools.Core;

public class TObjectDumpOptions {

  public int MaxDepth { get; set { field = value.WithinLimits(0, 10); } } = 4;

  public int BoxWidth { get; set { field = value.WithinLimits(10, 512); } } = 120;

  public bool DumpPrivateFields { get; set; } = false;
  public bool DumpPrivateProperties { get; set; } = false;

  public bool DumpPublicFields { get; set; } = true;
  public bool DumpPublicProperties { get; set; } = true;

  public bool WithTitle { get; set; } = false;

  public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;

  public bool WithColors { get; set; } = false;

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  public TObjectDumpOptions() { }
  public TObjectDumpOptions(TObjectDumpOptions other) {
    MaxDepth = other.MaxDepth;
    BoxWidth = other.BoxWidth;
    DumpPrivateFields = other.DumpPrivateFields;
    DumpPrivateProperties = other.DumpPrivateProperties;
    DumpPublicFields = other.DumpPublicFields;
    DumpPublicProperties = other.DumpPublicProperties;
    WithTitle = other.WithTitle;
    Culture = other.Culture;
    WithColors = other.WithColors;
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public static TObjectDumpOptions Default => new();
  public static TObjectDumpOptions All => new() {
    DumpPrivateFields = true,
    DumpPrivateProperties = true,
    DumpPublicFields = true,
    DumpPublicProperties = true
  };
}
