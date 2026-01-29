namespace BLTools.Core;

public struct SObjectDumpOptions {

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
  public SObjectDumpOptions() { }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  public static SObjectDumpOptions Default => new();
  public static SObjectDumpOptions All => new() {
    DumpPrivateFields = true,
    DumpPrivateProperties = true,
    DumpPublicFields = true,
    DumpPublicProperties = true
  };
}
