namespace BLTools.Core;

public static class AnsiColorExtensions {

  public static string Reset(this string source) => source.EndsWith("\e[0m") ? "" : "\e[0m";
  public static string FG_Black(this string source) => $"\e[30m{source}{source.Reset()}";
  public static string FG_Red(this string source) => $"\e[31m{source}{source.Reset()}";
  public static string FG_Green(this string source) => $"\e[32m{source}{source.Reset()}";
  public static string FG_Yellow(this string source) => $"\e[33m{source}{source.Reset()}";
  public static string FG_Blue(this string source) => $"\e[34m{source}{source.Reset()}";
  public static string FG_Magenta(this string source) => $"\e[35m{source}{source.Reset()}";
  public static string FG_Cyan(this string source) => $"\e[36m{source}{source.Reset()}";
  public static string FG_White(this string source) => $"\e[37m{source}{source.Reset()}";

  public static string FG_LightRed(this string source) => $"\e[91m{source}{source.Reset()}";
  public static string FG_LightGreen(this string source) => $"\e[92m{source}{source.Reset()}";
  public static string FG_LightYellow(this string source) => $"\e[93m{source}{source.Reset()}";
  public static string FG_LightBlue(this string source) => $"\e[94m{source}{source.Reset()}";
  public static string FG_LightMagenta(this string source) => $"\e[95m{source}{source.Reset()}";
  public static string FG_LightCyan(this string source) => $"\e[96m{source}{source.Reset()}";
  public static string FG_LightWhite(this string source) => $"\e[97m{source}{source.Reset()}";

  public static string BG_Black(this string source) => $"\e[40m{source}{source.Reset()}";
  public static string BG_Red(this string source) => $"\e[41m{source}{source.Reset()}";
  public static string BG_Green(this string source) => $"\e[42m{source}{source.Reset()}";
  public static string BG_Yellow(this string source) => $"\e[43m{source}{source.Reset()}";
  public static string BG_Blue(this string source) => $"\e[44m{source}{source.Reset()}";
  public static string BG_Magenta(this string source) => $"\e[45m{source}{source.Reset()}";
  public static string BG_Cyan(this string source) => $"\e[46m{source}{source.Reset()}";
  public static string BG_White(this string source) => $"\e[47m{source}{source.Reset()}";

  public static string BG_LightRed(this string source) => $"\e[101m{source}{source.Reset()}";
  public static string BG_LightGreen(this string source) => $"\e[102m{source}{source.Reset()}";
  public static string BG_LightYellow(this string source) => $"\e[103m{source}{source.Reset()}";
  public static string BG_LightBlue(this string source) => $"\e[104m{source}{source.Reset()}";
  public static string BG_LightMagenta(this string source) => $"\e[105m{source}{source.Reset()}";
  public static string BG_LightCyan(this string source) => $"\e[106m{source}{source.Reset()}";
  public static string BG_LightWhite(this string source) => $"\e[107m{source}{source.Reset()}";

  public static string Bold(this string source) => $"\e[1m{source}{source.Reset()}";
  public static string Underline(this string source) => $"\e[4m{source}{source.Reset()}";
  public static string Italics(this string source) => $"\e[3m{source}{source.Reset()}";

  public static string Dim(this string source) => $"\e[2m{source}{source.Reset()}";
  public static string Strikethrough(this string source) => $"\e[9m{source}{source.Reset()}";
  public static string Reverse(this string source) => $"\e[7m{source}{source.Reset()}";

  // Compositions courantes et utiles
  public static string Success(this string source) => source.FG_LightGreen();
  public static string Error(this string source) => source.FG_LightRed();
  public static string Warning(this string source) => source.FG_LightYellow();
  public static string Info(this string source) => source.FG_LightCyan();
  public static string Debug(this string source) => source.FG_LightBlue();

  // Avec fond pour plus de visibilité
  public static string ErrorHighlight(this string source) => source.FG_White().BG_LightRed();
  public static string WarningHighlight(this string source) => source.FG_Black().BG_LightYellow();
  public static string SuccessHighlight(this string source) => source.FG_Black().BG_LightGreen();
  
}
