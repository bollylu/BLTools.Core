using BLTools.Core.Test.Extensions.IPAddressEx;

namespace BLTools.Test.Extensions.StringEx;

/// <summary>
///This is a test class for StringExtensionTest and is intended
///to contain all StringExtensionTest Unit Tests
///</summary>
public class StringExtensionBeforeAfterTest {

  private static ILogger Logger => new TConsoleLogger<StringExtensionBeforeAfterTest>(TLoggerOptions.MessageOnly);

  #region --- Before --------------------------------------------
  [Test]
  public void StringExtension_BeforeWord_ResultOK() {
    string Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source);
    string Expected = "A brown";
    Logger.Dump(Expected);
    string Actual = Source.Before(" fox");
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_BeforeWordCaseInsensitive_ResultOK() {
    string Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source);
    string Expected = "A brown";
    Logger.Dump(Expected);
    string Actual = Source.Before(" Fox", StringComparison.CurrentCultureIgnoreCase);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_BeforeLetter_ResultOK() {
    string Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source);
    string Expected = "A br";
    Logger.Dump(Expected);
    string Actual = Source.Before("o");
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_BeforeChar_ResultOK() {
    string Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source);
    string Expected = "A brown fo";
    Logger.Dump(Expected);
    string Actual = Source.Before('x');
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_BeforeDotInSmallText_ResultOK() {
    string Source = "1.10";
    Logger.Dump(Source);
    string Expected = "1";
    Logger.Dump(Expected);
    string actual = Source.Before('.');
    Logger.Dump(actual);
    Assert.That(actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_BeforeEmptyString_CompleteSource() {
    string Source = "1.10";
    Logger.Dump(Source);
    string Expected = "1.10";
    Logger.Dump(Expected);
    string Actual = Source.Before("");
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_SourceEmptyBeforeString_EmptyString() {
    string Source = "";
    Logger.Dump(Source);
    string Expected = "";
    Logger.Dump(Expected);
    string Actual = Source.Before("toto");
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_BeforeInexistantString_ResultEmpty() {
    string Source = "1.10";
    Logger.Dump(Source);
    string Expected = "";
    Logger.Dump(Expected);
    string Actual = Source.Before("toto");
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(Expected));
    Logger.Ok();
  }
  #endregion --- Before --------------------------------------------

  #region --- Before ReadOnlySpan<char> --------------------------------------------
  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeWord() {
    ReadOnlySpan<char> Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "A brown";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before(" fox");
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeWordCaseInsensitive() {
    ReadOnlySpan<char> Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "A brown";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before(" Fox", StringComparison.CurrentCultureIgnoreCase);
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeLetter() {
    ReadOnlySpan<char> Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "A br";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before("o");
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeChar() {
    ReadOnlySpan<char> Source = "A brown fox jumps over a lazy dog";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "A brown fo";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before('x');
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeDotInSmallText() {
    ReadOnlySpan<char> Source = "1.10";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "1";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> actual = Source.Before('.');
    Logger.Dump(actual.ToString());
    Assert.That(Expected.SequenceEqual(actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeEmptyString() {
    ReadOnlySpan<char> Source = "1.10";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "1.10";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before("");
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_SourceEmptyBeforeString() {
    ReadOnlySpan<char> Source = "";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before("toto");
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_ReadOnlySpanChar_BeforeInexistantString() {
    ReadOnlySpan<char> Source = "1.10";
    Logger.Dump(Source.ToString());
    ReadOnlySpan<char> Expected = "";
    Logger.Dump(Expected.ToString());
    ReadOnlySpan<char> Actual = Source.Before("toto");
    Logger.Dump(Actual.ToString());
    Assert.That(Expected.SequenceEqual(Actual), Is.True);
    Logger.Ok();
  }
  #endregion --- Before ReadOnlySpan<char> --------------------------------------------

  #region --- BeforeLast --------------------------------------------
  [Test]
  public void StringExtension_BeforeLastWord_ResultOK() {
    string sourceString = "A brown fox fox jumps over a lazy foxy-dog";
    string expected = "A brown fox fox jumps over a lazy ";
    string actual = sourceString.BeforeLast("fox");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BeforeLastLetter_ResultOK() {
    string sourceString = @"\\server\sharename\folder\file.txt";
    string expected = @"\\server\sharename\folder";
    string actual = sourceString.BeforeLast(@"\");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BeforeLastChar_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    string expected = "A brown fox jumps over a lazy d";
    string actual = sourceString.BeforeLast('o');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BeforeLastDotInSmallText_ResultOK() {
    string sourceString = "1.10";
    string expected = "1";
    string actual = sourceString.BeforeLast('.');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BeforeLastEmptyString_CompleteSource() {
    string sourceString = "1.10";
    string expected = "1.10";
    string actual = sourceString.BeforeLast("");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_SourceEmptyBeforeLastString_EmptyString() {
    string sourceString = "";
    string expected = "";
    string actual = sourceString.BeforeLast("toto");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BeforeLastInexistantString_ResultEmpty() {
    string sourceString = "1.10";
    string expected = "";
    string actual = sourceString.BeforeLast("toto");
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion --- BeforeLast --------------------------------------------

  #region --- After --------------------------------------------
  [Test]
  public void StringExtension_AfterWord_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    string expected = " jumps over a lazy dog";
    string actual = sourceString.After(" fox");
    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtensionAsSpan_AfterWord_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    ReadOnlySpan<char> expected = " jumps over a lazy dog";
    ReadOnlySpan<char> actual = sourceString.AsSpan().After(" fox");
    Assert.That(actual.SequenceEqual(expected), Is.True);
    Logger.Ok();
  }

  [Test]
  public void StringExtension_AfterLetter_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    string expected = "wn fox jumps over a lazy dog";
    string actual = sourceString.After("o");
    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_AfterChar_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    string expected = " jumps over a lazy dog";
    string actual = sourceString.After('x');
    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_AfterDotInSmallText_ResultOK() {
    string sourceString = "1.10";
    string expected = "10";
    string actual = sourceString.After('.');
    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }

  [Test]
  public void StringExtension_AfterEmptyString_CompleteSource() {
    string sourceString = "1.10";
    string expected = "1.10";
    string actual = sourceString.After("");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_SourceEmptyAfterString_ResultEmpty() {
    string sourceString = "";
    string expected = "";
    string actual = sourceString.After("toto");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterInexistantString_ResultEmpty() {
    string sourceString = "1.10";
    string expected = "";
    string actual = sourceString.After("toto");
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion --- After --------------------------------------------

  #region --- AfterLast --------------------------------------------
  [Test]
  public void StringExtension_AfterLastWord_ResultOK() {
    string sourceString = "A brown fox fox jumps over a lazy foxy-dog";
    string expected = "y-dog";
    string actual = sourceString.AfterLast("fox");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterLastLetterOnlyOne_ResultOK() {
    string sourceString = "A brown fox fox jumps over a lazy foxy-dog";
    string expected = "g";
    string actual = sourceString.AfterLast("o");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterLastLetter_ResultOK() {
    string sourceString = @"\\server\sharename\folder\file.txt";
    string expected = @"file.txt";
    string actual = sourceString.AfterLast(@"\");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterLastChar_ResultOK() {
    string sourceString = "A brown fox jumps over a lazy dog";
    string expected = "g";
    string actual = sourceString.AfterLast('o');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterLastDotInSmallText_ResultOK() {
    string sourceString = "1.10";
    string expected = "10";
    string actual = sourceString.AfterLast('.');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterLastEmptyString_CompleteSource() {
    string sourceString = "1.10";
    string expected = "1.10";
    string actual = sourceString.AfterLast("");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_SourceEmptyAfterLastString_EmptyString() {
    string sourceString = "";
    string expected = "";
    string actual = sourceString.AfterLast("toto");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_AfterLastInexistantString_ResultEmpty() {
    string sourceString = "1.10";
    string expected = "";
    string actual = sourceString.AfterLast("toto");
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion --- AfterLast --------------------------------------------

  #region --- Between chars --------------------------------------------
  [Test]
  public void StringExtension_BetweenCharSourceEmpty_ResultEmpty() {
    string sourceString = "";
    string expected = "";
    string actual = sourceString.Between('[', ']');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenCharSourceNormal_ResultOK() {
    string sourceString = "This  is a test [various data; example]";
    string expected = "various data; example";
    string actual = sourceString.Between('[', ']');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenCharBothDelimiterMissing_ResultEmpty() {
    string sourceString = "This  is a test various data; example";
    string expected = "";
    string actual = sourceString.Between('[', ']');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenCharFirstDelimiterMissing_ResultEmpty() {
    string sourceString = "This  is a test [various data; example";
    string expected = "";
    string actual = sourceString.Between('[', ']');
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenCharSecondDelimiterMissing_ResultEmpty() {
    string sourceString = "This  is a test various data; example]";
    string expected = "";
    string actual = sourceString.Between('[', ']');
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion --- Between chars --------------------------------------------

  #region --- Between strings --------------------------------------------
  [Test]
  public void StringExtension_BetweenStringsSourceEmpty_ResultEmpty() {
    string sourceString = "";
    string expected = "";
    string actual = sourceString.Between("=[", "]=");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsSourceNormal_ResultOK() {
    string sourceString = "This  is a test =[Live]= =[blabla]=";
    string expected = "Live";
    string actual = sourceString.Between("=[", "]=");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsSourceNormalCaseInsensitive_ResultOK() {
    string sourceString = "This  is a test DelimLiveDElim =[blabla]=";
    string expected = "Live";
    string actual = sourceString.Between("delim", "delim", StringComparison.InvariantCultureIgnoreCase);
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsBothDelimiterMissing_ResultEmpty() {
    string sourceString = "This  is a test various data; example";
    string expected = "";
    string actual = sourceString.Between("=[", "]=");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsFirstDelimiterMissing_ResultEmpty() {
    string sourceString = "This  is a test =[various data; example";
    string expected = "";
    string actual = sourceString.Between("=[", "]=");
    Assert.That(actual, Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsSecondDelimiterMissing_ResultEmpty() {
    string sourceString = "This  is a test various data; example]=";
    string expected = "";
    string actual = sourceString.Between("=[", "]=");
    Assert.That(actual, Is.EqualTo(expected));
  }
  #endregion --- Between strings --------------------------------------------

  #region --- ItemsBetween chars --------------------------------------------
  [Test]
  public void StringExtension_BetweenCharsMultipleValuesSourceEmpty_ResultZero() {
    string sourceString = "";
    int expected = 0;
    IEnumerable<string> actual = sourceString.ItemsBetween('[', ']');
    Assert.That(actual.Count(), Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenCharsMultipleValues_ResultOK() {
    string sourceString = "source value [item1] [Item2] [item3;item4]";
    int expected = 3;
    IEnumerable<string> actual = sourceString.ItemsBetween('[', ']');
    Assert.That(actual.Count(), Is.EqualTo(expected));
    Assert.That(actual.First(), Is.EqualTo("item1"));
    Assert.That(actual.ElementAt(1), Is.EqualTo("Item2"));
    Assert.That(actual.Last(), Is.EqualTo("item3;item4"));
  }

  [Test]
  public void StringExtension_BetweenCharsMultipleValuesErrors_ResultOK() {
    string sourceString = "source value [item1] [Item2 [item3;item4]";
    int expected = 2;
    IEnumerable<string> actual = sourceString.ItemsBetween('[', ']');
    Assert.That(actual.Count(), Is.EqualTo(expected));
    Assert.That(actual.First(), Is.EqualTo("item1"));
    Assert.That(actual.Last(), Is.EqualTo("Item2 [item3;item4"));
  }

  [Test]
  public void StringExtension_BetweenCharsMultipleValuesDeStartlimiterMissing_ResultZero() {
    string sourceString = "This  is a test various data; example]=";
    int expected = 0;
    IEnumerable<string> actual = sourceString.ItemsBetween('[', ']');
    Assert.That(actual.Count(), Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenCharMultipleValuesEndDelimiterMissing_ResultZero() {
    string sourceString = "source value [item1 [Item2 [item3;item4";
    int expected = 0;
    IEnumerable<string> actual = sourceString.ItemsBetween('[', ']');
    Assert.That(actual.Count(), Is.EqualTo(expected));
  }
  #endregion --- ItemsBetween chars --------------------------------------------

  #region --- ItemsBetween strings --------------------------------------------
  [Test]
  public void StringExtension_BetweenStringMultipleValuesSourceEmpty_ResultZero() {
    string sourceString = "";
    int expected = 0;
    IEnumerable<string> actual = sourceString.ItemsBetween("=[", "]=");
    Assert.That(actual.Count(), Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsMultipleValues_ResultOK() {
    string sourceString = "source value =[item1]= =[Item2]==[item3;item4]=";
    int expected = 3;
    IEnumerable<string> actual = sourceString.ItemsBetween("=[", "]=");
    Assert.That(actual.Count(), Is.EqualTo(expected));
    Assert.That(actual.First(), Is.EqualTo("item1"));
    Assert.That(actual.ElementAt(1), Is.EqualTo("Item2"));
    Assert.That(actual.Last(), Is.EqualTo("item3;item4"));
  }

  [Test]
  public void StringExtension_BetweenStringsMultipleValuesErrors_ResultOK() {
    string sourceString = "source value =[item1]= =[Item2] =[item3;item4]=";
    int expected = 2;
    IEnumerable<string> actual = sourceString.ItemsBetween("=[", "]=");
    Assert.That(actual.Count(), Is.EqualTo(expected));
    Assert.That(actual.First(), Is.EqualTo("item1"));
    Assert.That(actual.Last(), Is.EqualTo("Item2] =[item3;item4"));
  }

  [Test]
  public void StringExtension_BetweenStringsMultipleValuesStartDelimiterMissing_ResultZero() {
    string sourceString = "This  is a test various data; example]=";
    int expected = 0;
    IEnumerable<string> actual = sourceString.ItemsBetween("=[", "]=");
    Assert.That(actual.Count(), Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsMultipleValuesEndDelimiterMissing_ResultZero() {
    string sourceString = "source value =[item1 =[Item2 =[item3;item4";
    int expected = 0;
    IEnumerable<string> actual = sourceString.ItemsBetween("=[", "]=");
    Assert.That(actual.Count(), Is.EqualTo(expected));
  }

  [Test]
  public void StringExtension_BetweenStringsMultipleValuesCaseInsensitive_ResultZero() {
    string sourceString = "source value Debutitem1FINdeButItem2 fin";
    int expected = 2;
    IEnumerable<string> actual = sourceString.ItemsBetween("debut", "fin", StringComparison.InvariantCultureIgnoreCase);
    Assert.That(actual.Count(), Is.EqualTo(expected));
    Assert.That(actual.First(), Is.EqualTo("item1"));
    Assert.That(actual.Last(), Is.EqualTo("Item2 "));
  }
  #endregion --- ItemsBetween strings --------------------------------------------

}
