using BLTools.Core.Logging;

namespace BLTools.Core.Test;

[TestFixture]
public class StringToBoolConversionsTest {

  [Test]
  [TestCase("1")]
  [TestCase("true")]
  [TestCase("yes")]
  [TestCase("y")]
  public void ConvertToBool_TrueOk(string source) {

    using ILogger Logger = new TConsoleLogger<StringToBoolConversionsTest>(TLoggerOptions.MessageOnly);
    Logger.Message("ConvertToBool_TrueOk");

    Logger.Message($"{source.WithQuotes()} must be true");
    Assert.That(source.ToBool(), Is.True);

    Logger.Ok();
  }

  [Test]
  [TestCase("0")]
  [TestCase("false")]
  [TestCase("no")]
  [TestCase("n")]
  public void ConvertToBool_FalseOk(string source) {

    using ILogger Logger = new TConsoleLogger<StringToBoolConversionsTest>(TLoggerOptions.MessageOnly);
    Logger.Message("ConvertToBool_FalseOk");
    Logger.Dump(source);

    Logger.Message($"{source.WithQuotes()} must be false");
    Assert.That(source.ToBool(), Is.False);

    Logger.Ok();
  }

  [Test]
  [TestCase("x", true)]
  [TestCase("feivuj", false)]
  public void ConvertToBool_GetDefault(string source, bool defaultValue) {

    using ILogger Logger = new TConsoleLogger<StringToBoolConversionsTest>(TLoggerOptions.MessageOnly);
    Logger.Message("ConvertFromBoolGetDefaultTest");
    Logger.Dump(source);
    Logger.Dump(defaultValue);

    Logger.Message($"{source.WithQuotes()} must be {defaultValue}");
    Assert.That(source.ToBool(defaultValue), Is.EqualTo(defaultValue));

    Logger.Ok();
  }
}
