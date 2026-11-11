namespace BLTools.Core;

public static class AnsiColorExtensions {

  extension(string source) {
    public string Reset() => source.EndsWith("\e[0m") ? "" : "\e[0m";
    public string FG_Black() => $"\e[30m{source}{source.Reset()}";
    public string FG_Red() => $"\e[31m{source}{source.Reset()}";
    public string FG_Green() => $"\e[32m{source}{source.Reset()}";
    public string FG_Yellow() => $"\e[33m{source}{source.Reset()}";
    public string FG_Blue() => $"\e[34m{source}{source.Reset()}";
    public string FG_Magenta() => $"\e[35m{source}{source.Reset()}";
    public string FG_Cyan() => $"\e[36m{source}{source.Reset()}";
    public string FG_White() => $"\e[37m{source}{source.Reset()}";

    public string FG_LightRed() => $"\e[91m{source}{source.Reset()}";
    public string FG_LightGreen() => $"\e[92m{source}{source.Reset()}";
    public string FG_LightYellow() => $"\e[93m{source}{source.Reset()}";
    public string FG_LightBlue() => $"\e[94m{source}{source.Reset()}";
    public string FG_LightMagenta() => $"\e[95m{source}{source.Reset()}";
    public string FG_LightCyan() => $"\e[96m{source}{source.Reset()}";
    public string FG_LightWhite() => $"\e[97m{source}{source.Reset()}";

    public string BG_Black() => $"\e[40m{source}{source.Reset()}";
    public string BG_Red() => $"\e[41m{source}{source.Reset()}";
    public string BG_Green() => $"\e[42m{source}{source.Reset()}";
    public string BG_Yellow() => $"\e[43m{source}{source.Reset()}";
    public string BG_Blue() => $"\e[44m{source}{source.Reset()}";
    public string BG_Magenta() => $"\e[45m{source}{source.Reset()}";
    public string BG_Cyan() => $"\e[46m{source}{source.Reset()}";
    public string BG_White() => $"\e[47m{source}{source.Reset()}";

    public string BG_LightRed() => $"\e[101m{source}{source.Reset()}";
    public string BG_LightGreen() => $"\e[102m{source}{source.Reset()}";
    public string BG_LightYellow() => $"\e[103m{source}{source.Reset()}";
    public string BG_LightBlue() => $"\e[104m{source}{source.Reset()}";
    public string BG_LightMagenta() => $"\e[105m{source}{source.Reset()}";
    public string BG_LightCyan() => $"\e[106m{source}{source.Reset()}";
    public string BG_LightWhite() => $"\e[107m{source}{source.Reset()}";

    public string Bold() => $"\e[1m{source}{source.Reset()}";
    public string Underline() => $"\e[4m{source}{source.Reset()}";
    public string Italics() => $"\e[3m{source}{source.Reset()}";

    public string Dim() => $"\e[2m{source}{source.Reset()}";
    public string Strikethrough() => $"\e[9m{source}{source.Reset()}";
    public string Reverse() => $"\e[7m{source}{source.Reset()}";

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
