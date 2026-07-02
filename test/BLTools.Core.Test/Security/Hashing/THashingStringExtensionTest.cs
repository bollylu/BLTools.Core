using BLTools.Core.Encryption;

namespace BLTools.Test.Extensions.SecurityEx;

public class THashingStringExtensionTest {
  #region MD5
  [Test]
  public void TestHashMD5_StandardString_HashIsOK() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.MD5);
    Assert.That(SourceString.VerifyHashFromBase64(Base64Hash, EHashingMethods.MD5), Is.True);
  }

  [Test]
  public void TestHashMD5_StandardStringModifiedForVerify_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.MD5);
    string TestString = SourceString + "0";
    Assert.That(TestString.VerifyHashFromBase64(Base64Hash, EHashingMethods.MD5), Is.False);
  }

  [Test]
  public void TestHashMD5_StandardStringHashModified_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.MD5);
    string TestHash = Base64Hash + "0";
    Assert.That(SourceString.VerifyHashFromBase64(TestHash, EHashingMethods.MD5), Is.False);
  }
  #endregion MD5

  #region SHA1
  [Test]
  public void TestHashSHA1_StandardString_HashIsOK() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA1);
    Assert.That(SourceString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA1), Is.True);
  }

  [Test]
  public void TestHashSHA1_StandardStringModifiedForVerify_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA1);
    string TestString = SourceString + "0";
    Assert.That(TestString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA1), Is.False);
  }

  [Test]
  public void TestHashSHA1_StandardStringHashModified_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA1);
    string TestHash = Base64Hash + "0";
    Assert.That(SourceString.VerifyHashFromBase64(TestHash, EHashingMethods.SHA1), Is.False);
  }
  #endregion SHA1

  #region SHA256
  [Test]
  public void TestHashSHA256_StandardString_HashIsOK() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA256);
    Assert.That(SourceString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA256), Is.True);
  }

  [Test]
  public void TestHashSHA256_StandardStringModifiedForVerify_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA256);
    string TestString = SourceString + "0";
    Assert.That(TestString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA256), Is.False);
  }

  [Test]
  public void TestHashSHA256_StandardStringHashModified_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA256);
    string TestHash = Base64Hash + "0";
    Assert.That(SourceString.VerifyHashFromBase64(TestHash, EHashingMethods.SHA256), Is.False);
  }
  #endregion SHA256 

  #region SHA384
  [Test]
  public void TestHashSHA384_StandardString_HashIsOK() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA384);
    Assert.That(SourceString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA384), Is.True);
  }

  [Test]
  public void TestHashSHA384_StandardStringModifiedForVerify_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA384);
    string TestString = SourceString + "0";
    Assert.That(TestString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA384), Is.False);
  }

  [Test]
  public void TestHashSHA384_StandardStringHashModified_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA384);
    string TestHash = Base64Hash + "0";
    Assert.That(SourceString.VerifyHashFromBase64(TestHash, EHashingMethods.SHA384), Is.False);
  }
  #endregion SHA384

  #region SHA512
  [Test]
  public void TestHashSHA512_StandardString_HashIsOK() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA512);
    Assert.That(SourceString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA512), Is.True);
  }

  [Test]
  public void TestHashSHA512_StandardStringModifiedForVerify_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA512);
    string TestString = SourceString + "0";
    Assert.That(TestString.VerifyHashFromBase64(Base64Hash, EHashingMethods.SHA512), Is.False);
  }

  [Test]
  public void TestHashSHA512_StandardStringHashModified_VerifyIsFalse() {
    string SourceString = "This is a test";
    string Base64Hash = SourceString.HashToBase64(EHashingMethods.SHA512);
    string TestHash = Base64Hash + "0";
    Assert.That(SourceString.VerifyHashFromBase64(TestHash, EHashingMethods.SHA512), Is.False);
  }
  #endregion SHA512
}
