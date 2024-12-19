using System.Numerics;

namespace BLTools.Core;

public static class NumberInList {

  public static bool IsIn<T>(this T number, params T[] list) where T : INumber<T> => list.Contains(number);

}
