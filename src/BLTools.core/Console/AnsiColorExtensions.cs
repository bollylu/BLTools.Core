namespace BLTools.Core;

public static class AnsiColorExtensions {

  public const string ESC = "\e[";
  public readonly static string FG_BLACK = $"{ESC}30m";
  public readonly static string FG_RED = $"{ESC}31m";
  public readonly static string FG_GREEN = $"{ESC}32m";
  public readonly static string FG_YELLOW = $"{ESC}33m";
  public readonly static string FG_BLUE = $"{ESC}34m";
  public readonly static string FG_MAGENTA = $"{ESC}35m";
  public readonly static string FG_CYAN = $"{ESC}36m";
  public readonly static string FG_WHITE = $"{ESC}37m";

  public readonly static string FG_LIGHTBLACK = $"{ESC}90m";
  public readonly static string FG_LIGHTRED = $"{ESC}91m";
  public readonly static string FG_LIGHTGREEN = $"{ESC}92m";
  public readonly static string FG_LIGHTYELLOW = $"{ESC}93m";
  public readonly static string FG_LIGHTBLUE = $"{ESC}94m";
  public readonly static string FG_LIGHTMAGENTA = $"{ESC}95m";
  public readonly static string FG_LIGHTCYAN = $"{ESC}96m";
  public readonly static string FG_LIGHTWHITE = $"{ESC}97m";

  public readonly static string BG_BLACK = $"{ESC}40m";
  public readonly static string BG_RED = $"{ESC}41m";
  public readonly static string BG_GREEN = $"{ESC}42m";
  public readonly static string BG_YELLOW = $"{ESC}43m";
  public readonly static string BG_BLUE = $"{ESC}44m";
  public readonly static string BG_MAGENTA = $"{ESC}45m";
  public readonly static string BG_CYAN = $"{ESC}46m";
  public readonly static string BG_WHITE = $"{ESC}47m";

  public readonly static string BG_LIGHTBLACK = $"{ESC}100m";
  public readonly static string BG_LIGHTRED = $"{ESC}101m";
  public readonly static string BG_LIGHTGREEN = $"{ESC}102m";
  public readonly static string BG_LIGHTYELLOW = $"{ESC}103m";
  public readonly static string BG_LIGHTBLUE = $"{ESC}104m";
  public readonly static string BG_LIGHTMAGENTA = $"{ESC}105m";
  public readonly static string BG_LIGHTCYAN = $"{ESC}106m";
  public readonly static string BG_LIGHTWHITE = $"{ESC}107m";

  public readonly static string BOLD = $"{ESC}1m";
  public readonly static string UNDERLINE = $"{ESC}4m";
  public readonly static string ITALICS = $"{ESC}3m";
  public readonly static string DIM = $"{ESC}2m";
  public readonly static string STRIKETHROUGH = $"{ESC}9m";
  public readonly static string REVERSE = $"{ESC}7m";

  public readonly static string RESET = $"{ESC}0m";

  extension(string source) {

    public string Reset() => source.EndsWith($"{ESC}{RESET}") ? "" : $"{ESC}{RESET}";
    public string FG_Black() => $"{ESC}{FG_BLACK}{source}{source.Reset()}";
    public string FG_Red() => $"{ESC}{FG_RED}{source}{source.Reset()}";
    public string FG_Green() => $"{ESC}{FG_GREEN}{source}{source.Reset()}";
    public string FG_Yellow() => $"{ESC}{FG_YELLOW}{source}{source.Reset()}";
    public string FG_Blue() => $"{ESC}{FG_BLUE}{source}{source.Reset()}";
    public string FG_Magenta() => $"{ESC}{FG_MAGENTA}{source}{source.Reset()}";
    public string FG_Cyan() => $"{ESC}{FG_CYAN}{source}{source.Reset()}";
    public string FG_White() => $"{ESC}{FG_WHITE}{source}{source.Reset()}";

    public string FG_LightRed() => $"{ESC}{FG_LIGHTRED}{source}{source.Reset()}";
    public string FG_LightGreen() => $"{ESC}{FG_LIGHTGREEN}{source}{source.Reset()}";
    public string FG_LightYellow() => $"{ESC}{FG_LIGHTYELLOW}{source}{source.Reset()}";
    public string FG_LightBlue() => $"{ESC}{FG_LIGHTBLUE}{source}{source.Reset()}";
    public string FG_LightMagenta() => $"{ESC}{FG_LIGHTMAGENTA}{source}{source.Reset()}";
    public string FG_LightCyan() => $"{ESC}{FG_LIGHTCYAN}{source}{source.Reset()}";
    public string FG_LightWhite() => $"{ESC}{FG_LIGHTWHITE}{source}{source.Reset()}";

    public string BG_Black() => $"{ESC}{BG_BLACK}{source}{source.Reset()}";
    public string BG_Red() => $"{ESC}{BG_RED}{source}{source.Reset()}";
    public string BG_Green() => $"{ESC}{BG_GREEN}{source}{source.Reset()}";
    public string BG_Yellow() => $"{ESC}{BG_YELLOW}{source}{source.Reset()}";
    public string BG_Blue() => $"{ESC}{BG_BLUE}{source}{source.Reset()}";
    public string BG_Magenta() => $"{ESC}{BG_MAGENTA}{source}{source.Reset()}";
    public string BG_Cyan() => $"{ESC}{BG_CYAN}{source}{source.Reset()}";
    public string BG_White() => $"{ESC}{BG_WHITE}{source}{source.Reset()}";

    public string BG_LightRed() => $"{ESC}{BG_LIGHTRED}{source}{source.Reset()}";
    public string BG_LightGreen() => $"{ESC}{BG_LIGHTGREEN}{source}{source.Reset()}";
    public string BG_LightYellow() => $"{ESC}{BG_LIGHTYELLOW}{source}{source.Reset()}";
    public string BG_LightBlue() => $"{ESC}{BG_LIGHTBLUE}{source}{source.Reset()}";
    public string BG_LightMagenta() => $"{ESC}{BG_LIGHTMAGENTA}{source}{source.Reset()}";
    public string BG_LightCyan() => $"{ESC}{BG_LIGHTCYAN}{source}{source.Reset()}";
    public string BG_LightWhite() => $"{ESC}{BG_LIGHTWHITE}{source}{source.Reset()}";

    public string Bold() => $"{ESC}{BOLD}{source}{source.Reset()}";
    public string Underline() => $"{ESC}{UNDERLINE}{source}{source.Reset()}";
    public string Italics() => $"{ESC}{ITALICS}{source}{source.Reset()}";

    public string Dim() => $"{ESC}{DIM}{source}{source.Reset()}";
    public string Strikethrough() => $"{ESC}{STRIKETHROUGH}{source}{source.Reset()}";
    public string Reverse() => $"{ESC}{REVERSE}{source}{source.Reset()}";

    // Compositions courantes et utiles
    public string Success() => source.FG_LightGreen();
    public string Error() => source.FG_LightRed();
    public string Warning() => source.FG_LightYellow();
    public string Info() => source.FG_LightCyan();
    public string Debug() => source.FG_LightBlue();

    // Avec fond pour plus de visibilité
    public string ErrorHighlight() => source.FG_White().BG_LightRed();
    public string WarningHighlight() => source.FG_Black().BG_LightYellow();
    public string SuccessHighlight() => source.FG_Black().BG_LightGreen();
  }
}
