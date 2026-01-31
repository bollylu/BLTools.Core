namespace BLTools.Core.Test.Extensions.StringBuilderEx;

/// <summary>
///This is a test class for StringBuilderExtension and is intended
///to contain all StringExtensionTest Unit Tests
///</summary>
public class StringBuilderExtensionTest {

  #region Truncate
  [Test]
  public void StringBuilderExtension_Truncate_ResultOK() {
    StringBuilder source = new StringBuilder("A brown fox jumps over a lazy dog");
    int length = 4;
    string expected = "A brown fox jumps over a lazy";
    StringBuilder actual = source.Truncate(length);
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }
  #endregion Truncate

  #region Trim
  [Test]
  public void StringBuilderExtension_Trim_ResultOK() {
    StringBuilder source = new StringBuilder("A brown fox jumps over a lazy dog  ");
    string expected = "A brown fox jumps over a lazy dog";
    StringBuilder actual = source.Trim();
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }

  [Test]
  public void StringBuilderExtension_TrimLeft_ResultOK() {
    StringBuilder source = new StringBuilder("  A brown fox jumps over a lazy dog  ");
    string expected = "A brown fox jumps over a lazy dog  ";
    StringBuilder actual = source.TrimLeft();
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }

  [Test]
  public void StringBuilderExtension_TrimAll_ResultOK() {
    StringBuilder source = new StringBuilder("  A brown fox jumps over a lazy dog  ");
    string expected = "A brown fox jumps over a lazy dog";
    StringBuilder actual = source.TrimAll();
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }

  [Test]
  public void StringBuilderExtension_TrimWithChars_ResultOK() {
    StringBuilder source = new StringBuilder("A brown fox jumps over a lazy dog *+");
    string expected = "A brown fox jumps over a lazy dog";
    StringBuilder actual = source.Trim('*', '+', ' ');
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }

  [Test]
  public void StringBuilderExtension_TrimLeftWithChars_ResultOK() {
    StringBuilder source = new StringBuilder("===  A brown fox jumps over a lazy dog  ");
    string expected = "A brown fox jumps over a lazy dog  ";
    StringBuilder actual = source.TrimLeft('=', ' ');
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }

  [Test]
  public void StringBuilderExtension_TrimAllWithChars_ResultOK() {
    StringBuilder source = new StringBuilder("*** A brown fox jumps over a lazy dog ***");
    string expected = "A brown fox jumps over a lazy dog";
    StringBuilder actual = source.TrimAll(' ', '*');
    Assert.That(actual.ToString(), Is.EqualTo(expected));
  }
  #endregion Trim

}
