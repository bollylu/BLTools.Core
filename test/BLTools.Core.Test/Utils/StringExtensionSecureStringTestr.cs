using System.Security;

namespace BLTools.Test.Extensions.StringEx.SecureStringEx;

/// <summary>
///This is a test class for StringExtensionTest and is intended
///to contain all StringExtensionTest Unit Tests
///</summary>
public class SecureStringExtensionTest {

  #region SecureString
  [Test]
  public void ConvertToSecureString_ReverseConversion_ResultTrue() {
    string SourceValue = "1235 4";
    SecureString actual = SourceValue.ConvertToSecureString();
    Assert.That(actual.ConvertToUnsecureString(), Is.EqualTo(SourceValue));
  }

  [Test]
  public void ConvertToSecureString_Compare_ResultTrue() {
    string SourceValue = "1235 4";
    SecureString actual = SourceValue.ConvertToSecureString();
    Assert.That(actual, Is.EqualTo(SourceValue.ConvertToSecureString()));
  }

  [Test]
  public void ConvertToSecureString_CompareDifferentStringLength_ResultFalse() {
    string SourceValue = "1235 4";
    SecureString actual = "1234".ConvertToSecureString();
    Assert.That(actual, Is.Not.EqualTo(SourceValue.ConvertToSecureString()));
  }

  [Test]
  public void ConvertToSecureString_CompareSameStringLength_ResultFalse() {
    string SourceValue = "1235 4";
    SecureString actual = "1234 5".ConvertToSecureString();
    Assert.That(actual, Is.Not.EqualTo(SourceValue.ConvertToSecureString()));
  }

  [Test]
  public void RemoveExternalQuotes_EmptyString_ResultEmptyString() {
    string SourceValue = "";
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.That(Actual, Is.EqualTo(""));
  }

  [Test]
  public void RemoveExternalQuotes_NormalString_ResultNormalString() {
    string SourceValue = "this is a message";
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.That(Actual, Is.EqualTo(SourceValue));
  }

  [Test]
  public void RemoveExternalQuotes_StringWithQuotes_ResultNormalString() {
    string SourceValue = "this is a message".WithQuotes();
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.That(Actual, Is.EqualTo("this is a message"));
  }

  [Test]
  public void RemoveExternalQuotes_QuotesInside_ResultNormalString() {
    string SourceValue = "this is \"a\" message".WithQuotes();
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.That(Actual, Is.EqualTo("this is \"a\" message"));
  }
  #endregion SecureString

}
