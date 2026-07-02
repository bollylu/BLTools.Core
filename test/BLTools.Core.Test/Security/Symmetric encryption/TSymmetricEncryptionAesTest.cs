using BLTools.Core.Encryption;

namespace BLTools.Test.Security;

public class TSymmetricEncryptionAesTest {
  [Test]
  public void TestEncryptSymmetricAes_ParametersOk128_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 128);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.AES, 128);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_ParametersOk192_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 192);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.AES, 192);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_ParametersOk256_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_SourceEmpty_EncryptionDecryptionOK() {
    string SourceText = "";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_BadKeyLengthTooSmall_Exception() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    Assert.That(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 253), Throws.ArgumentException);
  }

  [Test]
  public void TestEncryptSymmetricAes_BadKeyLengthZero_Exception() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    Assert.That(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 0), Throws.ArgumentException);
  }

  [Test]
  public void TestEncryptSymmetricAes_BadKeyLengthTooBig_Exception() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    Assert.That(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 1024), Throws.ArgumentException);
  }

  [Test]
  public void TestEncryptSymmetricAes_NoPassword_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  //[TestCategory("SymmetricEncryption"), TestMethod, TestCategory("AES")]
  //public void TestEncryptSymmetricAes_NullPassword_Exception() {
  //  string SourceText = "Je vais bien, merci.";
  //  string? Password = null;
  //  Assert.ThrowsException<ArgumentNullException>(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256));
  //}

  [Test]
  public void TestEncryptSymmetricAes_WrongPassword_DecryptionFailed() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.AES, 256);
    string DecryptPassword = "az12df34vc";
    Assert.That(() => EncryptedBase64.DecryptFromBase64(DecryptPassword, ESymmetricEncryptionAlgorithm.AES, 256), Throws.Exception);
  }

  [Test]
  public void TestEncryptSymmetricAes_ParametersOKEncodingUTF8_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.UTF8, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.UTF8, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_ParametersOKWrongEncoding_DecryptionFailed() {
    string SourceText = "Je vais bien, merci. Célébration.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.UTF8, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.Not.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_ParametersASCIIEncodingASCII_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci. Celebration.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetricAes_ParametersASCII_Accents_EncodingASCII_DecryptionFailed() {
    string SourceText = "Je vais bien, merci. Célébration.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.AES, 256);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.AES, 256);
    Assert.That(SourceText, Is.Not.EqualTo(DecipheredText));
  }
}
