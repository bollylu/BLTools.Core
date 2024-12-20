﻿using System.Numerics;

using static System.Math;

namespace BLTools.Core;

/// <summary>
/// Extensions for number manipulation and verification
/// </summary>
public static partial class NumberExtension {

  /// <summary>
  /// Verifies if a value is within limits (limits are included)
  /// </summary>
  /// <param name="value">The value to test</param>
  /// <param name="lowerLimit">The lower limit (included)</param>
  /// <param name="upperLimit">The upper limit (included)</param>
  /// <returns>The value if it is within limits, the closest limit otherwise</returns>
  /// <example>var value = 5.WithinLimits(0, 10); gives 5</example>
  /// <example>var value = 12.WithinLimits(0, 10); gives 10</example>
  /// <example>var value = 1.WithinLimits(5, 10); gives 5</example>
  public static T WithinLimits<T>(this T value, T lowerLimit, T upperLimit) where T : IComparable<T>, INumber<T> {
    return lowerLimit.CompareTo(upperLimit) <= 0
        ? (value.CompareTo(lowerLimit) < 0 ? lowerLimit : (value.CompareTo(upperLimit) > 0 ? upperLimit : value))
        : (value.CompareTo(upperLimit) < 0 ? upperLimit : (value.CompareTo(lowerLimit) > 0 ? lowerLimit : value));
  }

  ///// <summary>
  ///// Verifies if a value is within limits (limits are included)
  ///// </summary>
  ///// <param name="value">The value to test</param>
  ///// <param name="lowerLimit">The lower limit (included)</param>
  ///// <param name="upperLimit">The upper limit (included)</param>
  ///// <returns>true if value is within limits, false otherwise</returns>
  //public static long WithinLimits(this long value, long lowerLimit, long upperLimit) {
  //  return Max(lowerLimit, Min(upperLimit, value));
  //}

  ///// <summary>
  ///// Verifies if a value is within limits (limits are included)
  ///// </summary>
  ///// <param name="value">The value to test</param>
  ///// <param name="lowerLimit">The lower limit (included)</param>
  ///// <param name="upperLimit">The upper limit (included)</param>
  ///// <returns>true if value is within limits, false otherwise</returns>
  //public static float WithinLimits(this float value, float lowerLimit, float upperLimit) {
  //  return Max(lowerLimit, Min(upperLimit, value));
  //}

  ///// <summary>
  ///// Verifies if a value is within limits (limits are included)
  ///// </summary>
  ///// <param name="value">The value to test</param>
  ///// <param name="lowerLimit">The lower limit (included)</param>
  ///// <param name="upperLimit">The upper limit (included)</param>
  ///// <returns>true if value is within limits, false otherwise</returns>
  //public static double WithinLimits(this double value, double lowerLimit, double upperLimit) {
  //  return Max(lowerLimit, Min(upperLimit, value));
  //}

  ///// <summary>
  ///// Verifies if a value is within limits (limits are included)
  ///// </summary>
  ///// <param name="value">The value to test</param>
  ///// <param name="lowerLimit">The lower limit (included)</param>
  ///// <param name="upperLimit">The upper limit (included)</param>
  ///// <returns>true if value is within limits, false otherwise</returns>
  //public static decimal WithinLimits(this decimal value, decimal lowerLimit, decimal upperLimit) {
  //  return Max(lowerLimit, Min(upperLimit, value));
  //}

  /// <summary>
  /// Calculate an aligned value on a specific frontier. Alignment is made towards zero.
  /// If alignement value is zero, no alignment is done and the original value is returned.
  /// </summary>
  /// <param name="value">The value to align</param>
  /// <param name="alignmentValue">The frontier to align the value against</param>
  /// <param name="direction">The direction to align the number (default to lower, meaning closer to zero)</param>
  /// <returns>The aligned value</returns>
  public static int AlignedTo(this int value, int alignmentValue, ENumberAlignmentDirection direction = ENumberAlignmentDirection.Lower) {
    if (alignmentValue == 0) {
      return value;
    }
    switch (direction) {
      case ENumberAlignmentDirection.Upper:
        return Math.DivRem(value + alignmentValue, alignmentValue, out int RemU) * alignmentValue;

      case ENumberAlignmentDirection.Lower:
      default:
        return Math.DivRem(value, alignmentValue, out int RemL) * alignmentValue;
    }

  }

  /// <summary>
  /// Calculate an aligned value on a specific frontier. Alignment is made towards zero.
  /// If alignement value is zero, no alignment is done and the original value is returned.
  /// </summary>
  /// <param name="value">The value to align</param>
  /// <param name="alignmentValue">The frontier to align the value against</param>
  /// <param name="direction">The direction to align the number (default to lower, meaning closer to zero)</param>
  /// <returns>The aligned value</returns>
  public static long AlignedTo(this long value, long alignmentValue, ENumberAlignmentDirection direction = ENumberAlignmentDirection.Lower) {
    if (alignmentValue == 0) {
      return value;
    }
    switch (direction) {
      case ENumberAlignmentDirection.Upper:
        return Math.DivRem(value + alignmentValue, alignmentValue, out long RemU) * alignmentValue;

      case ENumberAlignmentDirection.Lower:
      default:
        return Math.DivRem(value, alignmentValue, out long RemL) * alignmentValue;
    }
  }

  /// <summary>
  /// Calculate an aligned value on a specific frontier. Alignment is made towards zero.
  /// If alignement value is zero, no alignment is done and the original value is returned.
  /// </summary>
  /// <param name="value">The value to align</param>
  /// <param name="alignmentValue">The frontier to align the value against</param>
  /// <param name="direction">The direction to align the number (default to lower, meaning closer to zero)</param>
  /// <returns>The aligned value</returns>
  public static float AlignedTo(this float value, float alignmentValue, ENumberAlignmentDirection direction = ENumberAlignmentDirection.Lower) {
    if (alignmentValue == 0) {
      return value;
    }
    float Remainder;
    switch (direction) {
      case ENumberAlignmentDirection.Upper:
        Remainder = (value + alignmentValue) % alignmentValue;
        return value + alignmentValue - Remainder;

      case ENumberAlignmentDirection.Lower:
      default:
        Remainder = value % alignmentValue;

        return value - Remainder;
    }
  }

  /// <summary>
  /// Calculate an aligned value on a specific frontier. Alignment is made towards zero.
  /// If alignement value is zero, no alignment is done and the original value is returned.
  /// </summary>
  /// <param name="value">The value to align</param>
  /// <param name="alignmentValue">The frontier to align the value against</param>
  /// <param name="direction">The direction to align the number (default to lower, meaning closer to zero)</param>
  /// <returns>The aligned value</returns>
  public static double AlignedTo(this double value, double alignmentValue, ENumberAlignmentDirection direction = ENumberAlignmentDirection.Lower) {
    if (alignmentValue == 0) {
      return value;
    }
    double Remainder;
    switch (direction) {
      case ENumberAlignmentDirection.Upper:
        Remainder = (value + alignmentValue) % alignmentValue;
        return value + alignmentValue - Remainder;

      case ENumberAlignmentDirection.Lower:
      default:
        Remainder = value % alignmentValue;
        return value - Remainder;
    }
  }

  /// <summary>
  /// Calculate an aligned value on a specific frontier. Alignment is made towards zero.
  /// If alignment value is zero, no alignment is done and the original value is returned.
  /// </summary>
  /// <param name="value">The value to align</param>
  /// <param name="alignmentValue">The frontier to align the value against</param>
  /// <param name="direction">The direction to align the number (default to lower, meaning closer to zero)</param>
  /// <returns>The aligned value</returns>
  public static decimal AlignedTo(this decimal value, decimal alignmentValue, ENumberAlignmentDirection direction = ENumberAlignmentDirection.Lower) {
    if (alignmentValue == 0) {
      return value;
    }
    decimal Remainder;
    switch (direction) {
      case ENumberAlignmentDirection.Upper:
        Remainder = (value + alignmentValue) % alignmentValue;
        return value + alignmentValue - Remainder;

      case ENumberAlignmentDirection.Lower:
      default:
        Remainder = value % alignmentValue;
        return value - Remainder;
    }
  }
}

