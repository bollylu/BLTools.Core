using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace BLTools.Core;

public static partial class NumberExtensions {

  extension<T>(T source) where T : INumber<T> {
    /// <summary>
    /// Checks if the value is in the given range (both ends count into the range)
    /// </summary>
    /// <param name="min">The minimum value of the range (included)</param>
    /// <param name="max">The maximum value of the range (included)</param>
    /// <returns>true if the value is comprised within the range, false otherwise</returns>
    public bool IsInRange(T min, T max) {
      return source >= min && source <= max;
    }

    /// <summary>
    /// Checks if the value is outside the given range (both ends count into the range)
    /// </summary>
    /// <param name="min">The minimum value of the range (included)</param>
    /// <param name="max">The maximum value of the range (included)</param>
    /// <returns>true if outside of range, false otherwise</returns>
    public bool IsOutsideRange(T min, T max) {
      return source < min || source > max;
    }
  }

  extension(int source) {
    /// <summary>
    /// Checks if the value is outside the given range (both ends count into the range)
    /// </summary>
    /// <param name="range">The range to check against (both ends included)</param>
    /// <returns>true if outside of range, false otherwise</returns>
    public bool IsOutsideRange(Range range) {
      return source < range.Start.Value || source > range.End.Value;
    }

    /// <summary>
    /// Checks if the value is in the given range (both ends count into the range)
    /// </summary>
    /// <param name="range">The range to check against (both ends included)</param>
    /// <returns>true if the value is comprised within the range, false otherwise</returns>
    public bool IsInRange(Range range) {
      return source >= range.Start.Value && source <= range.End.Value;
    }
  }

}
