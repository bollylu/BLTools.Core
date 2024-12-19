using BLTools.Core.Logging;

namespace BLTools.Core.Test;

[TestFixture]
public class StringToIntConversionsTest {

  [Test]
  [TestCase("42")]
  [TestCase("136")]
  [TestCase("-2658")]
  [TestCase("1234567")]
  public void ConvertToInt_ValuesOk(string source) {

    using ILogger Logger = new TConsoleLogger<StringToIntConversionsTest>(TLoggerOptions.MessageOnly);
    Logger.Message("ConvertToInt_ValuesOk");

    Logger.Message($"{source.WithQuotes()} is Ok");
    Assert.That(source.ToBinaryInteger(0), Is.EqualTo(int.Parse(source)));

    Logger.Ok();
  }

  [Test]
  [TestCase("12.3", 42)]
  [TestCase("feivuj", 28)]
  [TestCase("1234567890123456789", 28)]
  public void ConvertToInt_GetDefault(string source, int defaultValue) {

    using ILogger Logger = new TConsoleLogger<StringToIntConversionsTest>(TLoggerOptions.MessageOnly);
    Logger.Message("ConvertToInt_GetDefault");
    Logger.Dump(source);
    Logger.Dump(defaultValue);

    Logger.Message($"{source.WithQuotes()} is incorrect, getting {defaultValue}");
    Assert.That(source.ToBinaryInteger<int>(defaultValue), Is.EqualTo(defaultValue));

    Logger.Ok();
  }
}
