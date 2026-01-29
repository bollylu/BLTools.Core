using System.Security;

namespace BLTools.Test.Extensions.StringEx.SecureStringEx;

/// <summary>
///This is a test class for StringExtensionTest and is intended
///to contain all StringExtensionTest Unit Tests
///</summary>
[TestClass()]
public class SecureStringExtensionTest {

  #region SecureString
  [TestMethod(), TestCategory("String")]
  public void ConvertToSecureString_ReverseConversion_ResultTrue() {
    string SourceValue = "1235 4";
    SecureString actual = SourceValue.ConvertToSecureString();
    Assert.AreEqual(SourceValue, actual.ConvertToUnsecureString());
  }

  [TestMethod(), TestCategory("String")]
  public void ConvertToSecureString_Compare_ResultTrue() {
    string SourceValue = "1235 4";
    SecureString actual = SourceValue.ConvertToSecureString();
    Assert.IsTrue(SourceValue.ConvertToSecureString().IsEqualTo(actual));
  }

  [TestMethod(), TestCategory("String")]
  public void ConvertToSecureString_CompareDifferentStringLength_ResultFalse() {
    string SourceValue = "1235 4";
    SecureString actual = "1234".ConvertToSecureString();
    Assert.IsFalse(SourceValue.ConvertToSecureString().IsEqualTo(actual));
  }

  [TestMethod(), TestCategory("String")]
  public void ConvertToSecureString_CompareSameStringLength_ResultFalse() {
    string SourceValue = "1235 4";
    SecureString actual = "1234 5".ConvertToSecureString();
    Assert.IsFalse(SourceValue.ConvertToSecureString().IsEqualTo(actual));
  }

  [TestMethod(), TestCategory("String")]
  public void RemoveExternalQuotes_EmptyString_ResultEmptyString() {
    string SourceValue = "";
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.AreEqual("", Actual);
  }

  [TestMethod(), TestCategory("String")]
  public void RemoveExternalQuotes_NormalString_ResultNormalString() {
    string SourceValue = "this is a message";
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.AreEqual(SourceValue, Actual);
  }

  [TestMethod(), TestCategory("String")]
  public void RemoveExternalQuotes_StringWithQuotes_ResultNormalString() {
    string SourceValue = "\"this is a message\"";
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.AreEqual("this is a message", Actual);
  }

  [TestMethod(), TestCategory("String")]
  public void RemoveExternalQuotes_QuotesInside_ResultNormalString() {
    string SourceValue = "\"this is \"a\" message\"";
    string Actual = SourceValue.RemoveExternalQuotes();
    Assert.AreEqual("this is \"a\" message", Actual);
  }
  #endregion SecureString

}
