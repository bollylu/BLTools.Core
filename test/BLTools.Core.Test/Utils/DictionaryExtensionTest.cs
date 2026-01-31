using BLTools.Test.Diagnostics;

namespace BLTools.Test.Extensions.DictionaryEx;

public class DictionaryExtensionTest {

  private static readonly Dictionary<string, string> SourceDictStringString = new();
  private static readonly Dictionary<string, string> EmptySourceDict = new();
  private static readonly Dictionary<string, int> SourceDictStringInt = new();
  private static readonly Dictionary<string, bool> SourceDictStringBool = new();
  private static readonly Dictionary<int, string> SourceDictIntString = new();

  private const string KEY_STRING1 = "Key1";
  private const string KEY_STRING2 = "Key2";
  private const string KEY_STRING3 = "Key3";

  private const string VALUE_STRING1 = "Value1";
  private const string VALUE_STRING2 = "Value2";

  private const int VALUE_INT1 = 36;
  private const int VALUE_INT2 = 72;

  private const bool VALUE_BOOL1 = true;
  private const bool VALUE_BOOL2 = false;

  private const int KEY_INT1 = 30;
  private const int KEY_INT2 = 40;
  private const int KEY_INT3 = 60;

  private const string DEFAULT_STRING = "(default)";
  private const int DEFAULT_INT = -1;
  private const bool DEFAULT_BOOL = false;

  private static ILogger Logger => new TConsoleLogger<DiagnosticsTest_Dump>(TLoggerOptions.MessageOnly);

  [OneTimeSetUp]
  public void MyClassInitialize() {
    SourceDictStringString.Add(KEY_STRING1, VALUE_STRING1);
    SourceDictStringString.Add(KEY_STRING2, VALUE_STRING2);

    SourceDictStringInt.Add(KEY_STRING1, VALUE_INT1);
    SourceDictStringInt.Add(KEY_STRING2, VALUE_INT2);

    SourceDictStringBool.Add(KEY_STRING1, VALUE_BOOL1);
    SourceDictStringBool.Add(KEY_STRING2, VALUE_BOOL2);

    SourceDictIntString.Add(KEY_INT1, VALUE_STRING1);
    SourceDictIntString.Add(KEY_INT2, VALUE_STRING2);
  }

  [Test]
  public void DictStringString_GetStringKeyOk_ValueOk() {
    Logger.Dump(SourceDictStringString);
    Logger.Message($"Get string value {KEY_STRING1.WithQuotes()} with default {DEFAULT_STRING.WithQuotes()}");
    string? Actual = SourceDictStringString.SafeGetValue(KEY_STRING1, DEFAULT_STRING);
    Assert.That(Actual, Is.Not.Null);
    Logger.Dump(Actual);

    Assert.That(Actual, Is.EqualTo(VALUE_STRING1));
    Logger.Ok();
  }

  [Test]
  public void DictStringString_GetStringKeyInvalid_ValueDefault() {
    Logger.Dump(SourceDictStringString);
    Logger.Message($"Get string value {KEY_STRING3.WithQuotes()} with default {DEFAULT_STRING.WithQuotes()}");
    string? Actual = SourceDictStringString.SafeGetValue(KEY_STRING3, DEFAULT_STRING);
    Assert.That(Actual, Is.Not.Null);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(DEFAULT_STRING));
    Logger.Ok();
  }

  [Test]
  public void DictStringString_GetStringKeyNull_ValueDefault() {
    Logger.Dump(SourceDictStringString);
    Logger.Message("Get value with a null key, should get default");
    string? LookupKey = null;
    string? Actual = SourceDictStringString.SafeGetValue(LookupKey, DEFAULT_STRING);
    Assert.That(Actual, Is.Not.Null);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(DEFAULT_STRING));
    Logger.Ok();
  }

  [Test]
  public void DictStringString_EmptyDictionary_ValueDefault() {
    Logger.Dump(SourceDictStringString);
    Logger.Message($"Get value {KEY_STRING3.WithQuotes()} but dictionary is empty, should get default");
    string? Actual = EmptySourceDict.SafeGetValue(KEY_STRING3, DEFAULT_STRING);
    Assert.That(Actual, Is.Not.Null);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(DEFAULT_STRING));
    Logger.Ok();
  }

  [Test]
  public void DictStringInt_GetIntKeyOk_ValueOk() {
    Logger.Dump(SourceDictStringInt);
    Logger.Message($"Get value {KEY_STRING2.WithQuotes()}");
    int Actual = SourceDictStringInt.SafeGetValue(KEY_STRING2, DEFAULT_INT);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(VALUE_INT2));
    Logger.Ok();
  }

  [Test]
  public void DictStringInt_GetIntKeyInvalid_ValueDefault() {
    Logger.Dump(SourceDictStringInt);
    Logger.Message($"Default is {DEFAULT_INT}");
    Logger.Message($"Get value {KEY_STRING3.WithQuotes()}, should get default");
    int Actual = SourceDictStringInt.SafeGetValue(KEY_STRING3, DEFAULT_INT);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(DEFAULT_INT));
    Logger.Ok();
  }

  [Test]
  public void DictStringBool_GetBoolKeyOk_ValueOk() {
    Logger.Dump(SourceDictStringBool);
    Logger.Message($"Get value {KEY_STRING2.WithQuotes()}");
    bool Actual = SourceDictStringBool.SafeGetValue(KEY_STRING2, DEFAULT_BOOL);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(VALUE_BOOL2));
    Logger.Ok();
  }

  [Test]
  public void DictStringBool_GetBoolKeyInvalid_ValueDefault() {
    Logger.Dump(SourceDictStringBool);
    Logger.Message($"Get value {KEY_STRING3.WithQuotes()}, should get default");
    bool Actual = SourceDictStringBool.SafeGetValue(KEY_STRING3, DEFAULT_BOOL);
    Assert.That(Actual, Is.EqualTo(DEFAULT_BOOL));
    Logger.Dump(Actual);
    Logger.Ok();
  }

  [Test]
  public void DictIntString_GetStringKeyOk_ValueOk() {
    Logger.Dump(SourceDictIntString);
    Logger.Message($"Get value {KEY_INT2}");
    string? Actual = SourceDictIntString.SafeGetValue(KEY_INT2, DEFAULT_STRING);
    Assert.That(Actual, Is.Not.Null);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(VALUE_STRING2));
    Logger.Ok();
  }

  [Test]
  public void DictIntString_GetStringKeyInvalid_ValueDefault() {
    Logger.Dump(SourceDictIntString);
    Logger.Message($"Get value {KEY_INT3}, should get default");
    string? Actual = SourceDictIntString.SafeGetValue(KEY_INT3, DEFAULT_STRING);
    Assert.That(Actual, Is.Not.Null);
    Logger.Dump(Actual);
    Assert.That(Actual, Is.EqualTo(DEFAULT_STRING));
    Logger.Ok();
  }

}

