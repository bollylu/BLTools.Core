using BLTools.Core.Encryption;

namespace BLTools.Test.Extensions.SecurityEx;

public class THashMacExtensionStringTest {
  [Test]
  public void TestHMACMD5_StandardString_HMacIsOK() {
    string SourceString = "The quick brown fox jumps over the lazy dog";
    string Key = "key";
    string Base64HMac = SourceString.HMacToBase64(Key, EHashingMethods.MD5);
    Assert.That(SourceString.VerifyHMACFromBase64(Key, Base64HMac, EHashingMethods.MD5), Is.True);
  }

  [Test]
  public void TestHMACSHA1_StandardString_HMacIsOK() {
    string SourceString = "The quick brown fox jumps over the lazy dog";
    string Key = "key";
    string Base64HMac = SourceString.HMacToBase64(Key, EHashingMethods.SHA1);
    Assert.That(SourceString.VerifyHMACFromBase64(Key, Base64HMac, EHashingMethods.SHA1), Is.True);
  }

  [Test]
  public void TestHMACSHA256_StandardString_HMacIsOK() {
    string SourceString = "The quick brown fox jumps over the lazy dog";
    string Key = "key";
    string Base64HMac = SourceString.HMacToBase64(Key, EHashingMethods.SHA256);
    Assert.That(SourceString.VerifyHMACFromBase64(Key, Base64HMac, EHashingMethods.SHA256), Is.True);
  }

  [Test]
  public void TestHMACSHA384_StandardString_HMacIsOK() {
    string SourceString = "The quick brown fox jumps over the lazy dog";
    string Key = "key";
    string Base64HMac = SourceString.HMacToBase64(Key, EHashingMethods.SHA384);
    Assert.That(SourceString.VerifyHMACFromBase64(Key, Base64HMac, EHashingMethods.SHA384), Is.True);
  }

  [Test]
  public void TestHMACSHA512_StandardString_HMacIsOK() {
    string SourceString = "The quick brown fox jumps over the lazy dog";
    string Key = "key";
    string Base64HMac = SourceString.HMacToBase64(Key, EHashingMethods.SHA512);
    Assert.That(SourceString.VerifyHMACFromBase64(Key, Base64HMac, EHashingMethods.SHA512), Is.True);
  }

  [Test]
  public void TestHMACSHA256_StandardStringWrongKeyForDecode_VerifyIsFalse() {
    string SourceString = "The quick brown fox jumps over the lazy dog";
    string Key = "key";
    string WrongKey = "anotherKey";
    string Base64HMac = SourceString.HMacToBase64(Key, EHashingMethods.SHA256);
    Assert.That(SourceString.VerifyHMACFromBase64(WrongKey, Base64HMac, EHashingMethods.SHA256), Is.False);
  }
}
