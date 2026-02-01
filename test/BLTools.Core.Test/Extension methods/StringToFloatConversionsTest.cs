using BLTools.Core.Logging;
using BLTools.Core.Test.Extensions.IPAddressEx;

namespace BLTools.Core.Test;

public class StringToFloatConversionsTest {

  private static ILogger Logger => new TConsoleLogger<StringToFloatConversionsTest>(TLoggerOptions.MessageOnly);

  [Test]
  [TestCase("42")]
  [TestCase("136.123")]
  [TestCase("-2658.123")]
  [TestCase("1234567.987654")]
  public void ConvertToFloat_ValuesOk(string source) {

    Logger.Message("ConvertToFloat_ValuesOk");

    CultureInfo Invariant = CultureInfo.InvariantCulture;
    Logger.Message($"{source.WithQuotes()} should be Ok");
    Assert.That(source.ToFloatingPoint(0.0f, Invariant), Is.EqualTo(float.Parse(source, Invariant)));

    Logger.Ok();
  }

  [Test]
  [TestCase("42")]
  [TestCase("136,123")]
  [TestCase("-2658,123")]
  [TestCase("1234567,987654")]
  public void ConvertToFloat_ValuesOkWithBeFrCulture(string source) {

    CultureInfo FrBe = new CultureInfo("fr-BE") ;
    Logger.Message("ConvertToFloat_ValuesOkWithBeFrCulture");

    Logger.Message($"{source.WithQuotes()} should be Ok");
    Assert.That(source.ToFloatingPoint(0.0f, FrBe), Is.EqualTo(float.Parse(source, FrBe)));

    Logger.Ok();
  }

  [Test]
  [TestCase("12,3", 42.0f)]
  [TestCase("feivuj", 28)]
  [TestCase("12345.6789012.3456789123456789", 28f)]
  public void ConvertToFloat_GetDefault(string source, float defaultValue) {

    Logger.Message("ConvertToFloat_GetDefault");
    Logger.Dump(source);
    Logger.Dump(defaultValue);

    CultureInfo Invariant = CultureInfo.InvariantCulture;
    Logger.Message($"{source.WithQuotes()} should be incorrect, getting {defaultValue}");
    Assert.That(source.ToFloatingPoint(defaultValue, Invariant), Is.EqualTo(defaultValue));

    Logger.Ok();
  }
}
