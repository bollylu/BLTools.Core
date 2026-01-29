namespace BLTools.Test.Extensions.StringEx;

/// <summary>
///This is a test class for StringExtensionTest and is intended
///to contain all StringExtensionTest Unit Tests
///</summary>
[TestClass()]
public class StringExtensionTest {

  #region Left and right
  [TestMethod(), TestCategory("String")]
  public void StringExtension_Left7_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    int length = 7;
    string expected = "A brown";
    string actual = sourceString.Left(length);
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void StringExtension_Right8_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    int length = 8;
    string expected = "lazy dog";
    string? actual = sourceString.Right(length);
    Assert.AreEqual(expected, actual);
  }
  #endregion Left and right

  #region ToBool
  [TestMethod(), TestCategory("String")]
  public void ToBool_False_ResultFalse() {
    string booleanString = "false";
    bool expected = false;
    bool actual = booleanString.ToBool();
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ToBool_True_ResultTrue() {
    string booleanString = "true";
    bool expected = true;
    bool actual = booleanString.ToBool();
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ToBool_BadValue_ResultFalse() {
    string booleanString = "fal64se";
    bool expected = false;
    bool actual = booleanString.ToBool();
    Assert.AreEqual(expected, actual);
  }
  #endregion ToBool

  #region IsAlpha
  [TestMethod(), TestCategory("String")]
  public void IsAlpha_AlphaValue_ResultTrue() {
    string SourceValue = "OnlyAlphA";
    Assert.IsTrue(SourceValue.IsAlpha());
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlpha_NonAlphaValue_ResultFalse() {
    string SourceValue = "Only1AlphA";
    Assert.IsFalse(SourceValue.IsAlpha());
  }
  #endregion IsAlpha

  #region IsAlphaNumeric
  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumeric_AlphaValue_ResultTrue() {
    string SourceValue = "OnlyAlphA";
    Assert.IsTrue(SourceValue.IsAlphaNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumeric_NumericValue_ResultTrue() {
    string SourceValue = "1259763";
    Assert.IsTrue(SourceValue.IsAlphaNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumeric_AlphaNumericValue_ResultTrue() {
    string SourceValue = "Only123AlphA";
    Assert.IsTrue(SourceValue.IsAlphaNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumeric_NonAlphaNumericValue_ResultFalse() {
    string SourceValue = "Only1@lph@";
    Assert.IsFalse(SourceValue.IsAlphaNumeric());
  }
  #endregion IsAlphaNumeric

  #region IsNumeric
  [TestMethod(), TestCategory("String")]
  public void IsNumeric_NumericValue_ResultTrue() {
    string SourceValue = "12354";
    Assert.IsTrue(SourceValue.IsNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsNumeric_NegativeNumericValue_ResultTrue() {
    string SourceValue = "-12354";
    Assert.IsTrue(SourceValue.IsNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsNumeric_NumericValueWithSeparator_ResultTrue() {
    string SourceValue = "12.354,123";
    Assert.IsTrue(SourceValue.IsNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsNumeric_NonNumericValue_ResultFalse() {
    string SourceValue = "231655abc";
    Assert.IsFalse(SourceValue.IsNumeric());
  }

  [TestMethod(), TestCategory("String")]
  public void IsNumeric_BadNegativeNumericValue_ResultFalse() {
    string SourceValue = "231655-";
    Assert.IsFalse(SourceValue.IsNumeric());
  }
  #endregion IsNumeric

  #region IsAlphaOrBlank
  [TestMethod(), TestCategory("String")]
  public void IsAlphaOrBlank_AlphaValue_ResultTrue() {
    string SourceValue = "Only AlphA";
    Assert.IsTrue(SourceValue.IsAlphaOrBlank());
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaOrBlank_NonAlphaValue_ResultFalse() {
    string SourceValue = "Only1 AlphA";
    Assert.IsFalse(SourceValue.IsAlphaOrBlank());
  }
  #endregion IsAlphaOrBlank

  #region IsAlphaNumericOrBlank
  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumericOrBlank_AlphaValue_ResultTrue() {
    string SourceValue = "Only AlphA";
    bool expected = true;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumericOrBlank_NumericValue_ResultTrue() {
    string SourceValue = "1259 763";
    bool expected = true;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumericOrBlank_AlphaNumericValue_ResultTrue() {
    string SourceValue = "Only123 AlphA";
    bool expected = true;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.AreEqual(expected, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void IsAlphaNumericOrBlank_NonAlphaNumericValue_ResultFalse() {
    string SourceValue = "Only1 @lph@";
    bool expected = false;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.AreEqual(expected, actual);
  }
  #endregion IsAlphaNumeric

  #region IsNumericOrBlank
  [TestMethod(), TestCategory("String")]
  public void IsNumericOrBlank_NumericValue_ResultTrue() {
    string SourceValue = "1235 4";
    Assert.IsTrue(SourceValue.IsNumericOrBlank());
  }

  [TestMethod(), TestCategory("String")]
  public void IsNumericOrBlank_NumericValueWithSeparator_ResultTrue() {
    string SourceValue = " 12.354,123 ";
    Assert.IsTrue(SourceValue.IsNumericOrBlank());
  }

  [TestMethod(), TestCategory("String")]
  public void IsNumericOrBlank_NonNumericValue_ResultFalse() {
    string SourceValue = "231655 abc";
    Assert.IsFalse(SourceValue.IsNumericOrBlank());
  }
  #endregion IsNumericOrBlank

  #region --- ReplaceControlChars --------------------------------------------
  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_EmptyString_EmptyString() {
    string SourceValue = "";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual(SourceValue, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_NoControlChars_ResultOk() {
    string SourceValue = "AbC 123 =+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual(SourceValue, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_RawControlCharsTabAndCRLF_ResultOk() {
    string SourceValue = "AbC\t123\r\n=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual(SourceValue, actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_EncodedControlCharsTabAndCRLF_ResultOk() {
    string SourceValue = "AbC\\\t123\\\r\\\n=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual("AbC\t123\r\n=+", actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_EncodedControlCharsQuotes_ResultOk() {
    string SourceValue = "AbC\\\"123\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual("AbC\"123\"=+", actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_EncodedControlCharsQuoteInquotes_ResultOk() {
    string SourceValue = "AbC\\\"1\\\"23\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual("AbC\"1\"23\"=+", actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_EncodedControlCharsQuotesInquotes_ResultOk() {
    string SourceValue = "AbC\\\"1\\\"2\\\"3\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual("AbC\"1\"2\"3\"=+", actual);
  }

  [TestMethod(), TestCategory("String")]
  public void ReplaceControlChars_EncodedControlCharsRawQuoteInquotes_ResultOk() {
    string SourceValue = "AbC\"1\\\"2\\\"3\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.AreEqual("AbC\"1\\\"2\"3\"=+", actual);
  }
  #endregion --- ReplaceControlChars --------------------------------------------
}
