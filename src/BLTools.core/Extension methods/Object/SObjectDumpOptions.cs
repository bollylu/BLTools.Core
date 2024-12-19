namespace BLTools.Core;

public struct SObjectDumpOptions {

  public int MaxDepth { get => _MaxDepth; set { _MaxDepth = value.WithinLimits(0, 10); } }
  private int _MaxDepth = 4;

  private int _BoxWidth = 120;
  public int BoxWidth { get => _BoxWidth; set { _BoxWidth = value.WithinLimits(10, 512); } }

  private bool _DumpPrivateFields = false;
  public bool DumpPrivateFields { get => _DumpPrivateFields; set => _DumpPrivateFields = value; }

  private bool _DumpPrivateProperties = false;
  public bool DumpPrivateProperties { get => _DumpPrivateProperties; set => _DumpPrivateProperties = value; }

  private bool _DumpPublicFields = true;
  public bool DumpPublicFields { get => _DumpPublicFields; set => _DumpPublicFields = value; }

  private bool _DumpPublicProperties = true;
  public bool DumpPublicProperties { get => _DumpPublicProperties; set => _DumpPublicProperties = value; }

  private bool _WithTitle = false;
  public bool WithTitle { get => _WithTitle; set => _WithTitle = value; }

  private CultureInfo _Culture = CultureInfo.InvariantCulture;
  public CultureInfo Culture { get => _Culture; set => _Culture = value; }

  private bool _WithColors = false;
  public bool WithColors { get => _WithColors; set => _WithColors = value; }

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
