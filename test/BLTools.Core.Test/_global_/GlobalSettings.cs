
using System.Reflection;

using BLTools.Core.Diagnostic;

namespace BLTools.Core.Test;

/// <summary>
/// Global settings for the namespace BLTools
/// </summary>
public static class GlobalSettings {
  /// <summary>
  /// Default size for displaying debug boxes
  /// </summary>
  public const int DEBUG_BOX_WIDTH = 132;

  //public static TLoggerPool LoggerPool { get; } = new();

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
#if NET10_0_OR_GREATER
  public static TAbout ExecutingAbout => field ??= new TAbout(Assembly.GetExecutingAssembly());
  public static TAbout CallingAbout => field ??= new TAbout(Assembly.GetCallingAssembly());
  public static TAbout EntryAbout => field ??= new TAbout(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
#else
  public static TAbout ExecutingAbout => _ExecutingAbout ??= new TAbout(Assembly.GetExecutingAssembly());
  private static TAbout? _ExecutingAbout;

  public static TAbout CallingAbout => _CallingAbout ??= new TAbout(Assembly.GetCallingAssembly());
  private static TAbout? _CallingAbout;

  public static TAbout EntryAbout => _EntryAbout ??= new TAbout(Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly());
  private static TAbout? _EntryAbout;
#endif



}
