using BLTools.Test.Diagnostics;

namespace BLTools.Test.Extensions.ByteArrayEx;

/// <summary>
///This is a test class for ByteArrayExtensionTest and is intended
///to contain all ByteArrayExtensionTest Unit Tests
///</summary>
public class ByteArrayExtensionTest {

  private ILogger Logger => new TConsoleLogger<ByteArrayExtensionTest>(TLoggerOptions.MessageOnly);

  /// <summary>
  ///A test for ToHexString
  ///</summary>
  [Test]
  public void ToHexString_CommaSeparator_ResultOK() {
    byte[] rawData = new byte[] { 0x0C, 0x17, 0x22, 0x2D, 0x38 };
    string separator = ",";
    string expected = "0C,17,22,2D,38";
    string actual;
    actual = rawData.ToHexString(separator);
    Assert.That(actual, Is.EqualTo(expected));
  }

  /// <summary>
  ///A test for ToHexString
  ///</summary>
  [Test]
  public void ToHexString_NoSeparator_ResultOK() {
    byte[] rawData = new byte[] { 0x0C, 0x17, 0x22, 0x2D, 0x38 };
    string expected = "0C 17 22 2D 38";
    string actual;
    actual = rawData.ToHexString();
    Assert.That(actual, Is.EqualTo(expected));
  }

  /// <summary>
  ///A test for ToCharString
  ///</summary>
  [Test]
  public void ToCharString_NormalCharacters_ResultOK() {
    byte[] rawData = "abcdef".ToByteArray();
    string expected = "abcdef";
    string actual;
    actual = rawData.ToCharString();
    Assert.That(actual, Is.EqualTo(expected));
  }

  /// <summary>
  ///A test for ToCharString
  ///</summary>
  [Test]
  public void ToCharString_EmptySource_ResultOK() {
    byte[] rawData = "".ToByteArray();
    string expected = "";
    string actual;
    actual = rawData.ToCharString();
    Assert.That(actual, Is.EqualTo(expected));
  }

  /// <summary>
  ///A test for ToCharString
  ///</summary>
  [Test]
  public void ToCharString_SpecialChars_ResultOK() {
    byte[] rawData = "Text\r\n".ToByteArray();
    Logger.Dump(rawData);
    string expected = "Text\r\n";
    Logger.Dump(expected);
    string actual;
    actual = rawData.ToCharString();
    Logger.Dump(actual);
    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }

  /// <summary>
  ///A test for building a byte array from a hex string
  ///</summary>
  [Test]
  public void ToByteArrayFromHexString_InputOk_ResultOK() {
    string Source = "2365A2B7";
    Logger.Dump(Source);
    byte[] Target = Source.ToByteArrayFromHex();
    Logger.Dump(Target);
    string Compare = Target.ToHexString("");
    Logger.Dump(Compare);
    Assert.That(Compare, Is.EqualTo(Source));
    Logger.Ok();
  }

  /// <summary>
  ///A test for building a byte array from a hex string
  ///</summary>
  [Test]
  public void ToByteArrayFromHexString_InputEmpty_ResultOK() {
    string Source = "";
    Logger.Dump(Source);
    byte[] Target = Source.ToByteArrayFromHex();
    Logger.Dump(Target);
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  /// <summary>
  ///A test for building a byte array from a hex string
  ///</summary>
  [Test]
  public void ToByteArrayFromHexString_InputInvalidLength_Exception() {
    string Source = "A3A";
    Logger.Message($"Attempt to convert invalid Hex string : {Source}");
    Assert.That(() => Source.ToByteArrayFromHex(), Throws.Exception.TypeOf<FormatException>());
    Logger.Message("Got exception");
    Logger.Ok();
  }

  /// <summary>
  ///A test for building a byte array from a hex string
  ///</summary>
  [Test]
  public void ToByteArrayFromHexString_InputInvalid_Exception() {
    string Source = "A3Z4";
    Logger.Message($"Attempt to convert invalid Hex string : {Source}");
    Assert.That(() => Source.ToByteArrayFromHex(), Throws.Exception.TypeOf<FormatException>());
    Logger.Message("Got exception");
    Logger.Ok();
  }

  /// <summary>
  ///A test for building a byte array from a hex string
  ///</summary>
  [Test]
  public void ToByteArrayFromHexString_InputMAC_ResultOK() {
    Logger.Message("Building source");
    string Source = "A3:B4:DE:34:67:1C";
    Logger.Dump(Source);
    Logger.Message("Converting to array");
    byte[] Target = Source.ToByteArrayFromHex();
    string Compare = Target.ToHexString(":");
    Console.WriteLine(Compare);
    Assert.That(Compare, Is.EqualTo(Source));
    Logger.Dump(Target);
    Assert.That(Target.ToHexString(":"), Is.EqualTo(Source));
    Logger.Ok();
  }
}
