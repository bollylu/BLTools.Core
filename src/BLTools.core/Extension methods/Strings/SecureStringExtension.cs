using System.Runtime.InteropServices;
using System.Security;

namespace BLTools.Core;

/// <summary>
/// Extensions for SecureString
/// </summary>
public static class SecureStringExtension {

  /// <summary>
  /// Convert a SecureString to a normal string
  /// </summary>
  /// <param name="source">The source SecureString</param>
  /// <returns>The string</returns>
  public static string? ConvertToUnsecureString(this SecureString source) {
    if (source is null) {
      return null;
    }

    IntPtr UnmanagedString = IntPtr.Zero;
    try {
      UnmanagedString = Marshal.SecureStringToGlobalAllocUnicode(source);
      return Marshal.PtrToStringUni(UnmanagedString);
    } finally {
      Marshal.ZeroFreeGlobalAllocUnicode(UnmanagedString);
    }
  }

  /// <summary>
  /// Converts a string to a SecureString
  /// </summary>
  /// <param name="unsecureString">The source string</param>
  /// <returns>The SecureString</returns>
  public static SecureString ConvertToSecureString(this string unsecureString) {
    SecureString RetVal = new SecureString();
    foreach (char CharItem in unsecureString) {
      RetVal.AppendChar(CharItem);
    }
    RetVal.MakeReadOnly();
    return RetVal;
  }

  /// <summary>
  /// Compares two SecureString for equality without converting them to string
  /// </summary>
  /// <param name="stringA">The first secure string</param>
  /// <param name="stringB">The second secure string</param>
  /// <returns><see langword="true"/> if both string are identical, <see langword="false"/> otherwise</returns>
  public static bool IsEqualTo(this SecureString stringA, SecureString stringB) {
    IntPtr bstrA = IntPtr.Zero;
    IntPtr bstrB = IntPtr.Zero;
    try {
      bstrA = Marshal.SecureStringToBSTR(stringA);
      bstrB = Marshal.SecureStringToBSTR(stringB);
      int length1 = Marshal.ReadInt32(bstrA, -4);
      int length2 = Marshal.ReadInt32(bstrB, -4);
      if (length1 != length2) {
        return false;
      }
      for (int x = 0; x < length1; ++x) {
        byte b1 = Marshal.ReadByte(bstrA, x);
        byte b2 = Marshal.ReadByte(bstrB, x);
        if (b1 != b2) {
          return false;
        }
      }
      return true;
    } finally {
      if (bstrB != IntPtr.Zero) {
        Marshal.ZeroFreeBSTR(bstrB);
      }
      if (bstrA != IntPtr.Zero) {
        Marshal.ZeroFreeBSTR(bstrA);
      }
    }
  }
}
