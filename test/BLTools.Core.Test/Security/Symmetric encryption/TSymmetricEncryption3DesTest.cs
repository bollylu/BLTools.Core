using BLTools.Core.Encryption;

namespace BLTools.Test.Security;

public class TSymmetricEncryption3DesTest {
  [Test]
  public void TestEncryptSymmetric3Des_ParametersOk128_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 128);
    string DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 128);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetric3Des_ParametersOk192_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetric3Des_SourceEmpty_EncryptionDecryptionOK() {
    string SourceText = "";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string? DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }
  [Test]
  public void TestEncryptSymmetric3Des_BadKeyLengthTooSmall_Exception() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    Assert.That(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 125), Throws.ArgumentException);
  }

  [Test]
  public void TestEncryptSymmetric3Des_BadKeyLengthZero_Exception() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    Assert.That(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 0), Throws.ArgumentException);
  }

  [Test]
  public void TestEncryptSymmetric3Des_BadKeyLengthTooBig_Exception() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    Assert.That(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 1024), Throws.ArgumentException);
  }

  [Test]
  public void TestEncryptSymmetric3Des_NoPassword_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string DecipheredText = EncryptedBase64.DecryptFromBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  //[TestCategory("SymmetricEncryption"), TestMethod, TestCategory("3DES")]
  //public void TestEncryptSymmetric3Des_SourceTextIsNull_Exception() {
  //  string? SourceText = null;
  //  string Password = "";
  //  Assert.ThrowsException<ArgumentNullException>(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192));
  //}

  //[TestCategory("SymmetricEncryption"), TestMethod, TestCategory("3DES")]
  //public void TestEncryptSymmetric3Des_NullPassword_Exception() {
  //  string SourceText = "Je vais bien, merci.";
  //  string? Password = null;
  //  Assert.ThrowsException<ArgumentNullException>(() => SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192));
  //}

  [Test]
  public void TestEncryptSymmetric3Des_WrongPassword_DecryptionFailed() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string DecryptPassword = "az12df34vc";
    Assert.That(() => EncryptedBase64.DecryptFromBase64(DecryptPassword, ESymmetricEncryptionAlgorithm.TripleDES, 192), Throws.Exception);
  }

  [Test]
  public void TestEncryptSymmetric3Des_ParametersOKEncodingUTF8_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.UTF8, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.UTF8, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetric3Des_ParametersOKWrongEncoding_DecryptionFailed() {
    string SourceText = "Je vais bien, merci. Célébration.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.UTF8, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.Not.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetric3Des_ParametersASCIIEncodingASCII_EncryptionDecryptionOK() {
    string SourceText = "Je vais bien, merci. Celebration.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.EqualTo(DecipheredText));
  }

  [Test]
  public void TestEncryptSymmetric3Des_ParametersASCII_Accents_EncodingASCII_DecryptionFailed() {
    string SourceText = "Je vais bien, merci. Célébration.";
    string Password = "az12df34vb";
    string EncryptedBase64 = SourceText.EncryptToBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    string DecipheredText = EncryptedBase64.DecryptFromBase64(Password, Encoding.ASCII, ESymmetricEncryptionAlgorithm.TripleDES, 192);
    Assert.That(SourceText, Is.Not.EqualTo(DecipheredText));
  }
}
