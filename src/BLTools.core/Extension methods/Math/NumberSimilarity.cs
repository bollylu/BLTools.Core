using static System.Math;

namespace BLTools.Core;

public static partial class NumberExtensions {

  #region --- IsSimilarByPercent-------------------------------------------
  extension(double value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum percentage of value1 between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 allowed between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= percentageMargin</returns>
    public bool IsSimilarByPercent(double value2, float percentageMargin = 0) {
      double Delta = Abs(value1 - value2);
      double PercentageValue = value1 / 100d * percentageMargin;
      return (Delta <= PercentageValue);
    }
  }
  extension(float value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum percentage of value1 between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 allowed between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= percentageMargin</returns>
    public bool IsSimilarByPercent(float value2, float percentageMargin = 0) {
      float Delta = Abs(value1 - value2);
      float PercentageValue = value1 / 100f * percentageMargin;
      return (Delta <= PercentageValue);
    }
  }
  extension(decimal value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum percentage of value1 between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 allowed between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= percentageMargin</returns>
    public bool IsSimilarByPercent(decimal value2, float percentageMargin = 0) {
      decimal Delta = Abs(value1 - value2);
      decimal PercentageValue = value1 / 100m * (decimal)percentageMargin;
      return (Delta <= PercentageValue);
    }
  }
  extension(int value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum percentage of value1 between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 allowed between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= percentageMargin</returns>
    public bool IsSimilarByPercent(int value2, float percentageMargin = 0) {
      int Delta = Abs(value1 - value2);
      double PercentageValue = value1 / 100d * percentageMargin;
      return (Delta <= PercentageValue);
    }
  }
  extension(long value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum percentage of value1 between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 allowed between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= percentageMargin</returns>
    public bool IsSimilarByPercent(long value2, float percentageMargin = 0) {
      long Delta = Abs(value1 - value2);
      double PercentageValue = value1 / 100d * percentageMargin;
      return (Delta <= PercentageValue);
    }
  }
  #endregion --- IsSimilarByPercent -----------------------------------------

  #region --- IsSimilarByValue --------------------------------------------
  extension(double value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum delta between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= maxDelta</returns>
    public bool IsSimilarByValue(double value2, double maxDelta = 0) {
      double Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(float value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum delta between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= maxDelta</returns>
    public bool IsSimilarByValue(float value2, float maxDelta = 0) {
      float Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(decimal value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum delta between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= maxDelta</returns>
    public bool IsSimilarByValue(decimal value2, decimal maxDelta = 0) {
      decimal Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(int value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum delta between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= maxDelta</returns>
    public bool IsSimilarByValue(int value2, int maxDelta = 0) {
      int Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(long value1) {
    /// <summary>
    /// Indicate if value1 is close to value2, with a maximum delta between the two of them
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between value1 and value2</param>
    /// <returns><see langword="true"/> if the delta between value1 and value2 is &lt;= maxDelta</returns>
    public bool IsSimilarByValue(long value2, long maxDelta = 0) {
      long Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  #endregion --- IsSimilarByValue -----------------------------------------

  #region --- IsBelowByPercent-------------------------------------------
  extension(double value1) {
    /// <summary>
    /// Indicate if a value is below another by a maximum margin
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the margin, <see langword="false"/> otherwise</returns>
    public bool IsBelowByPercent(double value2, float percentageMargin = 0) {
      if (value1 >= value2) {
        return false;
      }
      double Delta = value2 - value1;
      double DeltaMaxPerPercentageMargin = value1 / 100d * percentageMargin;
      return (Delta <= DeltaMaxPerPercentageMargin);
    }
  }
  extension(float value1) {
    /// <summary>
    /// Indicate if a value is below another by a maximum margin
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the margin, <see langword="false"/> otherwise</returns>
    public bool IsBelowByPercent(float value2, float percentageMargin = 0) {
      if (value1 >= value2) {
        return false;
      }
      float Delta = value2 - value1;
      float DeltaMaxPerPercentageMargin = value1 / 100f * percentageMargin;
      return (Delta <= DeltaMaxPerPercentageMargin);
    }
  }
  extension(decimal value1) {
    /// <summary>
    /// Indicate if a value is below another by a maximum margin
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the margin, <see langword="false"/> otherwise</returns>
    public bool IsBelowByPercent(decimal value2, float percentageMargin = 0) {
      if (value1 >= value2) {
        return false;
      }
      decimal Delta = value2 - value1;
      decimal DeltaMaxPerPercentageMargin = value1 / 100m * (decimal)percentageMargin;
      return (Delta <= DeltaMaxPerPercentageMargin);
    }
  }
  extension(int value1) {
    /// <summary>
    /// Indicate if a value is below another by a maximum margin
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the margin, <see langword="false"/> otherwise</returns>
    public bool IsBelowByPercent(int value2, float percentageMargin = 0) {
      if (value1 >= value2) {
        return false;
      }
      int Delta = value2 - value1;
      double DeltaMaxPerPercentageMargin = value1 / 100 * percentageMargin;
      return (Delta <= DeltaMaxPerPercentageMargin);
    }
  }
  extension(long value1) {
    /// <summary>
    /// Indicate if a value is below another by a maximum margin
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="percentageMargin">The maximum percentage of value1 between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the margin, <see langword="false"/> otherwise</returns>
    public bool IsBelowByPercent(long value2, float percentageMargin = 0) {
      if (value1 >= value2) {
        return false;
      }
      long Delta = value2 - value1;
      double DeltaMaxPerPercentageMargin = value1 / 100 * percentageMargin;
      return (Delta <= DeltaMaxPerPercentageMargin);
    }
  }
  #endregion --- IsBelowByPercent -----------------------------------------

  #region --- IsBelowByValue --------------------------------------------
  extension(double value1) {
    /// <summary>
    /// Indicate if a value is below another by a maximum delta
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the delta, <see langword="false"/> otherwise</returns>
    public bool IsBelowByValue(double value2, double maxDelta = 0) {
      if (value1 >= value2) {
        return false;
      }
      double Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(float value1) {

    /// <summary>
    /// Indicate if a value is below another by a maximum delta
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the delta, <see langword="false"/> otherwise</returns>
    public bool IsBelowByValue(float value2, float maxDelta = 0) {
      if (value1 >= value2) {
        return false;
      }
      float Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(decimal value1) {

    /// <summary>
    /// Indicate if a value is below another by a maximum delta
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the delta, <see langword="false"/> otherwise</returns>
    public bool IsBelowByValue(decimal value2, decimal maxDelta = 0) {
      if (value1 >= value2) {
        return false;
      }
      decimal Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(int value1) {

    /// <summary>
    /// Indicate if a value is below another by a maximum delta
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the delta, <see langword="false"/> otherwise</returns>
    public bool IsBelowByValue(int value2, int maxDelta = 0) {
      if (value1 >= value2) {
        return false;
      }
      int Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  extension(long value1) {

    /// <summary>
    /// Indicate if a value is below another by a maximum delta
    /// </summary>
    /// <param name="value1">The first value</param>
    /// <param name="value2">The second value to compare with</param>
    /// <param name="maxDelta">The maximum delta between the two values</param>
    /// <returns><see langword="true"/> if second value is below the first by less or equal to the delta, <see langword="false"/> otherwise</returns>
    public bool IsBelowByValue(long value2, long maxDelta = 0) {
      if (value1 >= value2) {
        return false;
      }
      long Delta = Abs(value1 - value2);
      return Delta <= maxDelta;
    }
  }
  #endregion --- IsBelowByValue -----------------------------------------

}
