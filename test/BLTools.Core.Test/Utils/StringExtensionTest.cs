namespace BLTools.Core.Test.Extensions.StringEx;

/// <summary>
///This is a test class for StringExtensionTest and is intended
///to contain all StringExtensionTest Unit Tests
///</summary>
public class StringExtensionTest {

  #region Left and right
  [Test]
  public void StringExtension_Left7_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    int length = 7;
    string expected = "A brown";
    string actual = sourceString.Left(length);
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_Right8_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    int length = 8;
    string expected = "lazy dog";
    string? actual = sourceString.Right(length);
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion Left and right

  #region ToBool
  [Test]
  public void ToBool_False_ResultFalse() {
    string booleanString = "false";
    bool expected = false;
    bool actual = booleanString.ToBool();
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void ToBool_True_ResultTrue() {
    string booleanString = "true";
    bool expected = true;
    bool actual = booleanString.ToBool();
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void ToBool_BadValue_ResultFalse() {
    string booleanString = "fal64se";
    bool expected = false;
    bool actual = booleanString.ToBool();
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion ToBool

  #region IsAlpha
  [Test]
  public void IsAlpha_AlphaValue_ResultTrue() {
    string SourceValue = "OnlyAlphA";
    Assert.That(SourceValue.IsAlpha(), Is.True);
  }

  [Test]
  public void IsAlpha_NonAlphaValue_ResultFalse() {
    string SourceValue = "Only1AlphA";
    Assert.That(SourceValue.IsAlpha(), Is.False);
  }
  #endregion IsAlpha

  #region IsAlphaNumeric
  [Test]
  public void IsAlphaNumeric_AlphaValue_ResultTrue() {
    string SourceValue = "OnlyAlphA";
    Assert.That(SourceValue.IsAlphaNumeric(), Is.True);
  }

  [Test]
  public void IsAlphaNumeric_NumericValue_ResultTrue() {
    string SourceValue = "1259763";
    Assert.That(SourceValue.IsAlphaNumeric(), Is.True);
  }

  [Test]
  public void IsAlphaNumeric_AlphaNumericValue_ResultTrue() {
    string SourceValue = "Only123AlphA";
    Assert.That(SourceValue.IsAlphaNumeric(), Is.True);
  }

  [Test]
  public void IsAlphaNumeric_NonAlphaNumericValue_ResultFalse() {
    string SourceValue = "Only1@lph@";
    Assert.That(SourceValue.IsAlphaNumeric(), Is.False);
  }
  #endregion IsAlphaNumeric

  #region IsNumeric
  [Test]
  public void IsNumeric_NumericValue_ResultTrue() {
    string SourceValue = "12354";
    Assert.That(SourceValue.IsNumeric(), Is.True);
  }

  [Test]
  public void IsNumeric_NegativeNumericValue_ResultTrue() {
    string SourceValue = "-12354";
    Assert.That(SourceValue.IsNumeric(), Is.True);
  }

  [Test]
  public void IsNumeric_NumericValueWithSeparator_ResultTrue() {
    string SourceValue = "12.354,123";
    Assert.That(SourceValue.IsNumeric(), Is.True);
  }

  [Test]
  public void IsNumeric_NonNumericValue_ResultFalse() {
    string SourceValue = "231655abc";
    Assert.That(SourceValue.IsNumeric(), Is.False);
  }

  [Test]
  public void IsNumeric_BadNegativeNumericValue_ResultFalse() {
    string SourceValue = "231655-";
    Assert.That(SourceValue.IsNumeric(), Is.False);
  }
  #endregion IsNumeric

  #region IsAlphaOrBlank
  [Test]
  public void IsAlphaOrBlank_AlphaValue_ResultTrue() {
    string SourceValue = "Only AlphA";
    Assert.That(SourceValue.IsAlphaOrBlank(), Is.True);
  }

  [Test]
  public void IsAlphaOrBlank_NonAlphaValue_ResultFalse() {
    string SourceValue = "Only1 AlphA";
    Assert.That(SourceValue.IsAlphaOrBlank(), Is.False);
  }
  #endregion IsAlphaOrBlank

  #region IsAlphaNumericOrBlank
  [Test]
  public void IsAlphaNumericOrBlank_AlphaValue_ResultTrue() {
    string SourceValue = "Only AlphA";
    bool expected = true;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void IsAlphaNumericOrBlank_NumericValue_ResultTrue() {
    string SourceValue = "1259 763";
    bool expected = true;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void IsAlphaNumericOrBlank_AlphaNumericValue_ResultTrue() {
    string SourceValue = "Only123 AlphA";
    bool expected = true;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void IsAlphaNumericOrBlank_NonAlphaNumericValue_ResultFalse() {
    string SourceValue = "Only1 @lph@";
    bool expected = false;
    bool actual = SourceValue.IsAlphaNumericOrBlank();
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion IsAlphaNumeric

  #region IsNumericOrBlank
  [Test]
  public void IsNumericOrBlank_NumericValue_ResultTrue() {
    string SourceValue = "1235 4";
    Assert.That(SourceValue.IsNumericOrBlank(), Is.True);
  }

  [Test]
  public void IsNumericOrBlank_NumericValueWithSeparator_ResultTrue() {
    string SourceValue = " 12.354,123 ";
    Assert.That(SourceValue.IsNumericOrBlank(), Is.True);
  }

  [Test]
  public void IsNumericOrBlank_NonNumericValue_ResultFalse() {
    string SourceValue = "231655 abc";
    Assert.That(SourceValue.IsNumericOrBlank(), Is.False);
  }
  #endregion IsNumericOrBlank

  #region --- ReplaceControlChars --------------------------------------------
  [Test]
  public void ReplaceControlChars_EmptyString_EmptyString() {
    string SourceValue = "";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo(SourceValue));
  }

  [Test]
  public void ReplaceControlChars_NoControlChars_ResultOk() {
    string SourceValue = "AbC 123 =+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo(SourceValue));
  }

  [Test]
  public void ReplaceControlChars_RawControlCharsTabAndCRLF_ResultOk() {
    string SourceValue = "AbC\t123\r\n=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo(SourceValue));
  }

  [Test]
  public void ReplaceControlChars_EncodedControlCharsTabAndCRLF_ResultOk() {
    string SourceValue = "AbC\\\t123\\\r\\\n=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo("AbC\t123\r\n=+"));
  }

  [Test]
  public void ReplaceControlChars_EncodedControlCharsQuotes_ResultOk() {
    string SourceValue = "AbC\\\"123\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo("AbC\"123\"=+"));
  }

  [Test]
  public void ReplaceControlChars_EncodedControlCharsQuoteInquotes_ResultOk() {
    string SourceValue = "AbC\\\"1\\\"23\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo("AbC\"1\"23\"=+"));
  }

  [Test]
  public void ReplaceControlChars_EncodedControlCharsQuotesInquotes_ResultOk() {
    string SourceValue = "AbC\\\"1\\\"2\\\"3\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo("AbC\"1\"2\"3\"=+"));
  }

  [Test]
  public void ReplaceControlChars_EncodedControlCharsRawQuoteInquotes_ResultOk() {
    string SourceValue = "AbC\"1\\\"2\\\"3\\\"=+";
    string actual = SourceValue.ReplaceControlChars();
    Assert.That(actual, Is.EqualTo("AbC\"1\\\"2\"3\"=+"));
  }
  #endregion --- ReplaceControlChars --------------------------------------------
}
