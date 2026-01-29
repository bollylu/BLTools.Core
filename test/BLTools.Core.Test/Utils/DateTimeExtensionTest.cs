using BLTools.Test.Diagnostics;

namespace BLTools.Test.Extensions.DateTimeEx;

/// <summary>
///This is a test class for ArgElementTest and is intended
///to contain all ArgElementTest Unit Tests
///</summary>
public class DateTimeExtensionTest {

  private ILogger Logger => new TConsoleLogger<DateTimeExtensionTest>(TLoggerOptions.MessageOnly);
  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeToYMD_ResultOK() {
    DateTime Source = new DateTime(2015, 04, 28);
    string target = "2015-04-28";
    Assert.That(target, Is.EqualTo(Source.ToYMD()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeToYMDHMS_ResultOK() {
    DateTime Source = new DateTime(2015, 04, 28, 18, 6, 30);
    string target = "2015-04-28 18:06:30";
    Assert.That(target, Is.EqualTo(Source.ToYMDHMS()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeToDMY_ResultOK() {
    DateTime Source = new DateTime(2015, 04, 28);
    string target = "28/04/2015";
    Assert.That(target, Is.EqualTo(Source.ToDMY()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeToDMYHMS_ResultOK() {
    DateTime Source = new DateTime(2015, 04, 28, 18, 6, 30);
    string target = "28/04/2015 18:06:30";
    Assert.That(target, Is.EqualTo(Source.ToDMYHMS()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeToHMS_ResultOK() {
    DateTime Source = new DateTime(2015, 04, 28, 18, 6, 30);
    string target = "18:06:30";
    Assert.That(target, Is.EqualTo(Source.ToHMS()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeFromUTC_ResultOK() {
    DateTime Source = new DateTime(2015, 04, 28, 18, 6, 30);
    DateTime target = Source.ToLocalTime();
    Assert.That(target, Is.EqualTo(Source.FromUTC()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeEmptyDateAsDash_ResultOK() {
    DateTime Source = DateTime.MinValue;
    string target = "-";
    Assert.That(target, Is.EqualTo(Source.EmptyDateAsDash()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeEmptyDateAsBlank_ResultOK() {
    DateTime Source = DateTime.MinValue;
    string target = "";
    Assert.That(target, Is.EqualTo(Source.EmptyDateAsBlank()));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTimeEmptyDateAs_CustomValue_ResultOK() {
    DateTime Source = DateTime.MinValue;
    string target = "../../....";
    Assert.That(target, Is.EqualTo(Source.EmptyDateAs("../../....")));
  }

  /// <summary>
  ///A test for DateTime extension
  ///</summary>
  [Test]
  public void DateTime_TimeConsistent_ResultOK() {
    DateTime Source = new DateTime(2015, 3, 10, 10, 33, 0);
    DateTime target = DateTime.MinValue.Add(new TimeSpan(10, 33, 0));
    Trace.WriteLine(target.ToString());
    Assert.That(target == Source.Time(), Is.True);
  }

}
