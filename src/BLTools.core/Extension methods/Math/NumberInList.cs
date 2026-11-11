using System.Numerics;

namespace BLTools.Core;

public static class NumberInList {

  extension<T>(T number) where T : INumber<T> {
    public bool IsIn(params T[] list) => list.Contains(number);
  }

}
