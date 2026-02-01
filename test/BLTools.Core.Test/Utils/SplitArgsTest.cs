using System.Collections.Specialized;
using System.Web;

using BLTools.Core.Test.Extensions.IPAddressEx;

namespace BLTools.Core.Test.Cli;

/// <summary>
///This is a test class for SplitArgsTest and is intended
///to contain all SplitArgsTest Unit Tests
///</summary>
public class SplitArgsTest {

  private static ILogger Logger => new TConsoleLogger<IPAddressExtensionTest>(TLoggerOptions.MessageOnly);

  #region Tests for constructors
  /// <summary>
  ///Verifies the number of arguments from the command line
  ///</summary>
  [Test]
  public void SplitArgsConstructor_CommandLine_Gets3Args() {
    string cmdLine = "program.exe par1=val1 par2=val2";
    ISplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.That(Args.Count(), Is.EqualTo(3));
  }

  /// <summary>
  ///Verifies the number of arguments from the command line
  ///</summary>
  [Test]
  public void SplitArgsConstructor_CommandLineWithSpaces_Gets3Args() {
    string cmdLine = "program.exe par1=\"val1 with spaces\" par2=val2";
    ISplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.That(Args.Count(), Is.EqualTo(3));
  }

  /// <summary>
  ///First argument name is the program name (no value)
  ///</summary>
  [Test]
  public void SplitArgsConstructor_CommandLine_FirstArgIsProgram() {
    string cmdLine = "program.exe par1=val1 par2=val2";
    SplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.That(Args.Any(), Is.True);
    Assert.That(Args[0]?.Name ?? "", Is.EqualTo("program.exe"));
  }

  /// <summary>
  /// Contains a specific argument
  ///</summary>
  [Test]
  public void SplitArgsConstructor_CommandLine_ContainsArgElement() {
    string cmdLine = "program.exe par1=val1 par2=val2";
    SplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.That(Args.GetAll(), Does.Contain(new ArgElement(1, "par1", "val1")));
  }

  /// <summary>
  /// Verifies the number of arguments from an array
  ///</summary>
  [Test]
  public void SplitArgsConstructor_FromArray_Gets3Args() {
    IEnumerable<string> Source = ["program.exe", "par1=val1", "par2=val2"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.Count(), Is.EqualTo(3));
  }

  /// <summary>
  ///First argument name is the program name (no value)
  ///</summary>
  [Test]
  public void SplitArgsConstructor_FromArray_FirstArgIsProgram() {
    IEnumerable<string> Source = ["program.exe", "par1=val1", "par2=val2"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args[0]?.Name ?? "", Is.EqualTo("program.exe"));
  }

  /// <summary>
  /// Contains a specific argument
  ///</summary>
  [Test]
  public void SplitArgsConstructor_FromArray_ContainsArgElement() {
    IEnumerable<string> Source = ["program.exe", "par1=val1", "par2=val2"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetAll(), Does.Contain(new ArgElement(1, "par1", "val1")));
  }

#if NETCOREAPP
  /// <summary>
  /// Built from a url
  ///</summary>
  [Test]
  public void SplitArgsConstructor_FromUrl_ContainsArgElement() {
    string Url = "arg1=value1";
    NameValueCollection TestCollection = HttpUtility.ParseQueryString(Url);
    SplitArgs Args = new SplitArgs();
    Args.Parse(TestCollection);
    Assert.That(Args.GetAll(), Does.Contain(new ArgElement(0, "arg1", "value1")));
  }

  /// <summary>
  /// Built from a url, second argument
  ///</summary>
  [Test]
  public void SplitArgsConstructor_FromUrlTwoArgs_ContainsArgElement() {
    string Url = "arg1=value1&arg2=value2";
    NameValueCollection TestCollection = HttpUtility.ParseQueryString(Url);
    SplitArgs Args = new SplitArgs();
    Args.Parse(TestCollection);
    Assert.That(Args.GetAll(), Does.Contain(new ArgElement(0, "arg2", "value2")));
  }
#endif

  #endregion Tests for constructors

  #region Tests for IsDefined
  /// <summary>
  ///A test for IsDefined
  ///</summary>
  [Test]
  public void IsDefined_ValidParam_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.IsDefined("verbose"), Is.True);
  }

  /// <summary>
  ///A test for IsDefined
  ///</summary>
  [Test]
  public void IsDefined_BadParam_IsFalse() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.IsDefined("otherthanverbose"), Is.False);
  }
  #endregion Tests for IsDefined

  #region Tests for GetValue<T>(key, default)
  /// <summary>
  ///A test for GetValue&st;string&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericString_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>("par1", ""), Is.EqualTo("val1"));
  }
  ///// <summary>
  /////A test for GetValue&st;string[]&gt;
  /////</summary>
  //[Test]
  //public void GetValue_KeyGenericStringArray_IsTrue() {
  //  IEnumerable<string> Source = ["program.exe", "/par1=val1;val2;val3", "/verbose"];
  //  SplitArgs Args = new SplitArgs();
  //  Args.Parse(Source);
  //  string[] DataRead = Args.GetValue<string[]>("par1", []);
  //  Assert.Multiple(() =>
  //  {
  //    Assert.That(DataRead[0], Is.EqualTo("val1"));
  //    Assert.That(DataRead[1], Is.EqualTo("val2"));
  //    Assert.That(DataRead[2], Is.EqualTo("val3"));
  //  });
  //}
  ///// <summary>
  /////A test for GetValue&st;string[]&gt;
  /////</summary>
  //[Test]
  //public void GetValue_KeyGenericIntArray_IsTrue() {
  //  IEnumerable<string> Source = ["program.exe", "/par1=18;4568;123", "/verbose"];
  //  SplitArgs Args = new SplitArgs();
  //  Args.Parse(Source);
  //  int[] DataRead = Args.GetValue<int[]>("par1", 0);
  //  Assert.Multiple(() =>
  //  {
  //    Assert.That(DataRead[0], Is.EqualTo(18));
  //    Assert.That(DataRead[1], Is.EqualTo(4568));
  //    Assert.That(DataRead[2], Is.EqualTo(123));
  //  });
  //}

  ///// <summary>
  /////A test for GetValue&st;string[]&gt;
  /////</summary>
  //[Test]
  //public void GetValue_KeyGenericLongArray_IsTrue() {
  //  IEnumerable<string> Source = ["program.exe", "/par1=456879;9874563;123654789", "/verbose"];
  //  SplitArgs Args = new SplitArgs();
  //  Args.Parse(Source);
  //  long[] DataRead = Args.GetValue<long[]>("par1", []);
  //  Assert.Multiple(() =>
  //  {
  //    Assert.That(DataRead[0], Is.EqualTo(456879));
  //    Assert.That(DataRead[1], Is.EqualTo(9874563));
  //    Assert.That(DataRead[2], Is.EqualTo(123654789));
  //  });
  //}

  /// <summary>
  ///A test for GetValue&st;string&gt; with spaces
  ///</summary>
  [Test]
  public void GetValue_KeyGenericStringWithSpaces_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1 complex", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>("par1", ""), Is.EqualTo("val1 complex"));
  }
  /// <summary>
  ///A test for GetValue&st;int&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericInt_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<int>("par1", 0), Is.EqualTo(1236));
  }
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericDouble_IsTrue() {
    IEnumerable<string> Source = [ "program.exe",
                                                  $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}2365",
                                                  "/verbose"
                                                ];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<double>("par1", 0, CultureInfo.CurrentCulture), Is.EqualTo(1236.2365D));
  }
  /// <summary>
  ///A test for GetValue&st;long&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericLong_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=654321987", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<long>("par1", 0), Is.EqualTo(654321987L), Args.GetValue<float>("par1", 0).ToString());
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericFloat_IsTrue() {
    IEnumerable<string> Source = [ "program.exe",
                                                  $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}23",
                                                  "/verbose"
                                                ];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<float>("par1", 0, CultureInfo.CurrentCulture), Is.EqualTo(1236.23F), Args.GetValue<float>("par1", 0).ToString());
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [Test]
  public void GetValue_KeyGenericDateTime_IsTrue() {
    Logger.Message("Generating source");
    IEnumerable<string> Source = ["program.exe", "/par1=12/6/1998", "/verbose"];
    Logger.Dump(Source);

    SplitArgs Args = new SplitArgs();
    Logger.Message("Parsing data");
    Args.Parse(Source);

    Logger.Dump(Args, 1);

    DateTime Par1 = Args.GetValue<DateTime>("par1", DateTime.MinValue, CultureInfo.GetCultureInfo("FR-BE"));
    Logger.Dump(Par1, 1);

    Assert.That(Par1, Is.EqualTo(new DateTime(1998, 6, 12)));

    Logger.Ok();
  }
  #endregion Tests for GetValue<T>(key, default)

  #region Tests for GetValue<T>(1, default)
  /// <summary>
  ///A test for GetValue&st;string&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericString_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>(1, ""), Is.EqualTo("val1"));
  }
  /// <summary>
  ///A test for GetValue&st;string&gt; with spaces
  ///</summary>
  [Test]
  public void GetValue_PosGenericStringWithSpaces_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1 complex", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>(1, ""), Is.EqualTo("val1 complex"));
  }
  /// <summary>
  ///A test for GetValue&st;int&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericInt_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<int>(1, 0), Is.EqualTo(1236));
  }
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericDouble_IsTrue() {
    IEnumerable<string> Source = ["program.exe", $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}2365", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<double>(1, 0, CultureInfo.CurrentCulture), Is.EqualTo(1236.2365D));
  }
  /// <summary>
  ///A test for GetValue&st;long&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericLong_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=654321987", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<long>(1, 0), Is.EqualTo(654321987L));
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericFloat_IsTrue() {
    IEnumerable<string> Source = ["program.exe", $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}23", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<float>(1, 0, CultureInfo.CurrentCulture), Is.EqualTo(1236.23F));
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [Test]
  public void GetValue_PosGenericDateTime_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=12/6/1998", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<DateTime>(1, DateTime.MinValue, CultureInfo.GetCultureInfo("FR-BE")), Is.EqualTo(new DateTime(1998, 6, 12)));
  }
  #endregion Tests for GetValue<T>(1, default)

  #region Tests for GetValue<T>(3, default)
  /// <summary>
  ///A test for GetValue&st;string&gt;
  ///</summary>
  [Test]
  public void GetValue_Pos3GenericString_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1", "/verbose", "/par2=val2"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>(3, ""), Is.EqualTo("val2"));
  }
  /// <summary>
  ///A test for GetValue&st;string&gt; with spaces
  ///</summary>
  [Test]
  public void GetValue_Pos3GenericStringWithSpaces_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=val1 complex", "/verbose", "/par2=val2 complex"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>(3, ""), Is.EqualTo("val2 complex"));
  }
  /// <summary>
  ///A test for GetValue&st;int&gt;
  ///</summary>
  [Test]
  public void GetValue_Pos3GenericInt_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236", "/verbose", "/par2=98764"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<int>(3, 0), Is.EqualTo(98764));
  }
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [Test]
  public void GetValue_Pos3GenericDouble_IsTrue() {
    IEnumerable<string> Source = [ "program.exe",
                                                             $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}2365",
                                                             "/verbose",
                                                             $"/par2=654789{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}123456" ];
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<double>(3, 0, CultureInfo.CurrentCulture), Is.EqualTo(654789.123456D));
  }
  /// <summary>
  ///A test for GetValue&st;long&gt;
  ///</summary>
  [Test]
  public void GetValue_Pos3GenericLong_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=654321987", "/verbose", "/par2=987641234"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<long>(3, 0), Is.EqualTo(987641234L));
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [Test]
  public void GetValue_Pos3GenericFloat_IsTrue() {
    IEnumerable<string> Source = ["program.exe", string.Format("/par1=1236{0}2365", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), "/verbose", string.Format("/par2=98764{0}1234", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<float>(3, 0, CultureInfo.CurrentCulture), Is.EqualTo(98764.1234F));
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [Test]
  public void GetValue_Pos3GenericDateTime_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=12/6/1998", "/verbose", "/par2=28/04/1966"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<DateTime>(3, DateTime.MinValue, CultureInfo.GetCultureInfo("FR-BE")), Is.EqualTo(new DateTime(1966, 4, 28)));
  }
  #endregion Tests for GetValue<T>(3, default)

  #region Tests for GetValue<T>(key, default, culture)
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericDoubleCultureUs_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236.2365", "/verbose"];
    Logger.Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Logger.Dump(Args);
    Logger.Message("Get par1 as double");
    double Target = Args.GetValue<double>("par1", 0, CultureInfo.GetCultureInfo("en-us"));
    Logger.Dump(Target);
    Assert.That(Target, Is.EqualTo(1236.2365D));
    Logger.Ok();
  }

  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericFloatCultureUs_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236.23", "/verbose"];
    Logger.Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Logger.Dump(Args);
    Logger.Message("Get par1 as float");
    float Target = Args.GetValue<float>("par1", 0, CultureInfo.GetCultureInfo("en-us"));
    Logger.Dump(Target);
    Assert.That(Target, Is.EqualTo(1236.23F));
    Logger.Ok();
  }

  /// <summary>
  ///A test for GetValue&st;Int&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericIntCultureUs_IsFalse() {
    IEnumerable<string> Source = ["program.exe", "/par1=1,236,123", "/verbose"];
    Logger.Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Logger.Dump(Args);
    Logger.Message("Get par1 as int");
    int Target = Args.GetValue<int>("par1", 0, CultureInfo.GetCultureInfo("en-us"));
    Logger.Dump(Target);
    Assert.That(Target, Is.EqualTo(1236123));
    Logger.Ok();
  }

  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  [Test]
  public void GetValue_KeyGenericDateTimeCultureUs_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=6/12/1998", "/verbose"];
    Logger.Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Logger.Dump(Args);
    Logger.Message("Get par1 as DateTime");
    DateTime Target = Args.GetValue<DateTime>("par1", DateTime.MinValue, CultureInfo.GetCultureInfo("en-us"));
    Logger.Dump(Target);
    Assert.That(Target, Is.EqualTo(new DateTime(1998, 6, 12)));
    Logger.Ok();
  }
  #endregion Tests for GetValue<T>(key, default, culture)

  #region Tests for GetValue<T>(1, default, culture)
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericDoubleCultureUs_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236.2365", "/verbose"];
    Logger.Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Logger.Dump(Args);
    Logger.Message("Get first arg as double");
    double Target = Args.GetValue<double>(1, 0, CultureInfo.GetCultureInfo("en-us"));
    Logger.Dump(Target);
    Assert.That(Target, Is.EqualTo(1236.2365D));
    Logger.Ok();
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [Test]
  public void GetValue_PosGenericFloatCultureUs_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=1236.23", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<float>(1, 0, CultureInfo.GetCultureInfo("en-us")), Is.EqualTo(1236.23F));
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [Test]
  public void GetValue_PosGenericDateTimeCultureUs_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/par1=6/12/1998", "/verbose"];
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.That(Args.GetValue<DateTime>(1, DateTime.MinValue, CultureInfo.GetCultureInfo("en-us")), Is.EqualTo(new DateTime(1998, 6, 12)));
  }
  #endregion Tests for GetValue<T>(1, default, culture)

  /// <summary>
  ///A test for GetValue&st;string&gt; with case sensitivity
  ///</summary>
  [Test]
  public void GetValue_KeyGenericStringCaseSensitive_IsTrue() {
    IEnumerable<string> Source = ["program.exe", "/Par1=val1", "/par1=val1b"];
    SplitArgs Args = new SplitArgs() { IsCaseSensitive = true };
    Args.Parse(Source);
    Assert.That(Args.GetValue<string>("Par1", ""), Is.EqualTo("val1"));
    Assert.That(Args.GetValue<string>("par1", ""), Is.EqualTo("val1b"));
  }

  [Test]
  public void SplitArgs_OnlyKeyTestIndex_HasNoValue() {
    SplitArgs Args = new SplitArgs();
    Assert.That(Args.HasValue(2), Is.False);
  }

  [Test]
  public void SplitArgs_OnlyKeyTestKey_HasNoValue() {
    SplitArgs Args = new SplitArgs();
    Assert.That(Args.HasValue("test"), Is.False);
  }

  [Test]
  public void SplitArgs_OnlyKeyTestKey_HasValue() {
    SplitArgs Args = new SplitArgs();
    Args.Parse("prog.exe test=value");
    Assert.That(Args.HasValue("test"), Is.True);
  }

}
