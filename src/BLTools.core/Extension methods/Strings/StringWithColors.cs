namespace BLTools.Core;

public static class StringWithColors {

  public static string Style_FG_Red(this string source) => $"\e[31m{source}\e[0m";
  public static string Style_FG_Green(this string source) => $"\e[32m{source}\e[0m";
  public static string Style_FG_Yellow(this string source) => $"\e[33m{source}\e[0m";
  public static string Style_FG_Blue(this string source) => $"\e[34m{source}\e[0m";
  public static string Style_FG_Magenta(this string source) => $"\e[35m{source}\e[0m";
  public static string Style_FG_Cyan(this string source) => $"\e[36m{source}\e[0m";

  public static string Style_BG_Red(this string source) => $"\e[41m{source}\e[0m";
  public static string Style_BG_Green(this string source) => $"\e[42m{source}\e[0m";
  public static string Style_BG_Yellow(this string source) => $"\e[43m{source}\e[0m";
  public static string Style_BG_Blue(this string source) => $"\e[44m{source}\e[0m";
  public static string Style_BG_Magenta(this string source) => $"\e[45m{source}\e[0m";
  public static string Style_BG_Cyan(this string source) => $"\e[46m{source}\e[0m";
  public static string Style_BG_White(this string source) => $"\e[47m{source}\e[0m";

  public static string Style_Bold(this string source) => $"\e[1m{source}\e[0m";
  public static string Style_Underline(this string source) => $"\e[4m{source}\e[0m";
  public static string Style_Italics(this string source) => $"\e[3m{source}\e[0m";
}
