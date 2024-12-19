using BLTools.Core.Diagnostic;

namespace BLTools.Core;

/// <summary>
/// Global settings for the namespace BLTools
/// </summary>
public static class GlobalSettings {
  /// <summary>
  /// Default size for displaying debug boxes
  /// </summary>
  public const int DEBUG_BOX_WIDTH = 132;

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  static GlobalSettings() {
    Initialize();
  }

  private static bool _IsInitialized = false;
  private static bool _IsInitializing = false;

  /// <summary>
  /// Initialize the class data content
  /// </summary>
  public static void Initialize() {
    if (_IsInitialized) {
      return;
    }
    if (_IsInitializing) {
      return;
    }
    _IsInitializing = true;

    ExecutingAbout.Initialize();

    _IsInitializing = false;
    _IsInitialized = true;
  }

  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  /// <summary>
  /// The TAbout of this assembly
  /// </summary>
  public static TAbout ExecutingAbout {
    get {
      return _ExecutingAbout ??= new TAbout(AppDomain.CurrentDomain.GetAssemblies().Single(a => a.GetName().Name == nameof(BLTools.Core)));
    }
  }
  private static TAbout? _ExecutingAbout;


}
