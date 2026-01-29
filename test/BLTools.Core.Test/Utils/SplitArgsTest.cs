using System.Collections.Specialized;
using System.Web;

namespace BLTools.Test.Cli;

/// <summary>
///This is a test class for SplitArgsTest and is intended
///to contain all SplitArgsTest Unit Tests
///</summary>
[TestClass()]
public class SplitArgsTest {

  #region Additional test attributes
  // 
  //You can use the following additional attributes as you write your tests:
  //
  //Use ClassInitialize to run code before running the first test in the class
  //[ClassInitialize()]
  //public static void MyClassInitialize(TestContext testContext)
  //{
  //}
  //
  //Use ClassCleanup to run code after all tests in a class have run
  //[ClassCleanup()]
  //public static void MyClassCleanup()
  //{
  //}
  //
  //Use TestInitialize to run code before running each test
  //[TestInitialize()]
  //public void MyTestInitialize()
  //{
  //}
  //
  //Use TestCleanup to run code after each test has run
  //[TestCleanup()]
  //public void MyTestCleanup()
  //{
  //}
  //
  #endregion

  #region Tests for constructors
  /// <summary>
  ///Verifies the number of arguments from the command line
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_CommandLine_Gets3Args() {
    string cmdLine = "program.exe par1=val1 par2=val2";
    ISplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.AreEqual(3, Args.Count());
  }

  /// <summary>
  ///Verifies the number of arguments from the command line
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_CommandLineWithSpaces_Gets3Args() {
    string cmdLine = "program.exe par1=\"val1 with spaces\" par2=val2";
    ISplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.AreEqual(3, Args.Count());
  }

  /// <summary>
  ///First argument name is the program name (no value)
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_CommandLine_FirstArgIsProgram() {
    string cmdLine = "program.exe par1=val1 par2=val2";
    SplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.IsTrue(Args.Any());
    Assert.AreEqual("program.exe", Args[0]?.Name);
  }

  /// <summary>
  /// Contains a specific argument
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_CommandLine_ContainsArgElement() {
    string cmdLine = "program.exe par1=val1 par2=val2";
    SplitArgs Args = new SplitArgs();
    Args.Parse(cmdLine);
    Assert.IsTrue(Args.GetAll().Contains(new ArgElement(1, "par1", "val1")));
  }

  /// <summary>
  /// Verifies the number of arguments from an array
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_FromArray_Gets3Args() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "par1=val1", "par2=val2" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.AreEqual(3, Args.Count());
  }

  /// <summary>
  ///First argument name is the program name (no value)
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_FromArray_FirstArgIsProgram() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "par1=val1", "par2=val2" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.AreEqual("program.exe", Args[0]?.Name);
  }

  /// <summary>
  /// Contains a specific argument
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_FromArray_ContainsArgElement() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "par1=val1", "par2=val2" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsNotNull(Args.GetAll().Contains(new ArgElement(1, "par1", "val1")));
  }

#if NETCOREAPP
  /// <summary>
  /// Built from a url
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_FromUrl_ContainsArgElement() {
    string Url = "arg1=value1";
    NameValueCollection TestCollection = HttpUtility.ParseQueryString(Url);
    SplitArgs Args = new SplitArgs();
    Args.Parse(TestCollection);
    Assert.IsTrue(Args.GetAll().Contains(new ArgElement(0, "arg1", "value1")));
  }

  /// <summary>
  /// Built from a url, second argument
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void SplitArgsConstructor_FromUrlTwoArgs_ContainsArgElement() {
    string Url = "arg1=value1&arg2=value2";
    NameValueCollection TestCollection = HttpUtility.ParseQueryString(Url);
    SplitArgs Args = new SplitArgs();
    Args.Parse(TestCollection);
    Assert.IsTrue(Args.GetAll().Contains(new ArgElement(0, "arg2", "value2")));
  }
#endif

  #endregion Tests for constructors

  #region Tests for IsDefined
  /// <summary>
  ///A test for IsDefined
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void IsDefined_ValidParam_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.IsDefined("verbose"));
  }

  /// <summary>
  ///A test for IsDefined
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void IsDefined_BadParam_IsFalse() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsFalse(Args.IsDefined("otherthanverbose"));
  }
  #endregion Tests for IsDefined

  #region Tests for GetValue<T>(key, default)
  /// <summary>
  ///A test for GetValue&st;string&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericString_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>("par1", "") == "val1");
  }
  /// <summary>
  ///A test for GetValue&st;string[]&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericStringArray_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1;val2;val3", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    string[]? DataRead = Args.GetValue<string[]>("par1", Array.Empty<string>());
    Assert.IsNotNull(DataRead);
    Assert.IsTrue(DataRead[0] == "val1");
    Assert.IsTrue(DataRead[1] == "val2");
    Assert.IsTrue(DataRead[2] == "val3");
  }
  /// <summary>
  ///A test for GetValue&st;string[]&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericIntArray_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=18;4568;123", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    int[]? DataRead = Args.GetValue<int[]>("par1", Array.Empty<int>());
    Assert.IsNotNull(DataRead);
    Assert.IsTrue(DataRead[0] == 18);
    Assert.IsTrue(DataRead[1] == 4568);
    Assert.IsTrue(DataRead[2] == 123);
  }
  /// <summary>
  ///A test for GetValue&st;string[]&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericLongArray_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=456879;9874563;123654789", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    long[]? DataRead = Args.GetValue<long[]>("par1", Array.Empty<long>());
    Assert.IsNotNull(DataRead);
    Assert.IsTrue(DataRead[0] == 456879);
    Assert.IsTrue(DataRead[1] == 9874563);
    Assert.IsTrue(DataRead[2] == 123654789);
  }
  /// <summary>
  ///A test for GetValue&st;string&gt; with spaces
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericStringWithSpaces_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1 complex", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>("par1", "") == "val1 complex");
  }
  /// <summary>
  ///A test for GetValue&st;int&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericInt_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<int>("par1", 0) == 1236);
  }
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericDouble_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe",
                                                             $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}2365",
                                                             "/verbose"
                                                            };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<double>("par1", 0, CultureInfo.CurrentCulture) == 1236.2365D);
  }
  /// <summary>
  ///A test for GetValue&st;long&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericLong_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=654321987", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<long>("par1", 0) == 654321987L, Args.GetValue<float>("par1", 0).ToString());
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericFloat_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe",
                                                             $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}23",
                                                             "/verbose"
                                                           };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<float>("par1", 0, CultureInfo.CurrentCulture) == 1236.23F, Args.GetValue<float>("par1", 0).ToString());
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericDateTime_IsTrue() {
    Message("Gererating source");
    IEnumerable<string> Source = new List<string>() { "program.exe",
                                                             "/par1=12/6/1998",
                                                             "/verbose" };
    Dump(Source);

    SplitArgs Args = new SplitArgs();
    Message("Parsing data");
    Args.Parse(Source);

    Dump(Args, 1);

    DateTime Par1 = Args.GetValue<DateTime>("par1", DateTime.MinValue, CultureInfo.GetCultureInfo("FR-BE"));
    Dump(Par1, 1);

    Assert.AreEqual(new DateTime(1998, 6, 12), Par1);

    Ok();
  }
  #endregion Tests for GetValue<T>(key, default)

  #region Tests for GetValue<T>(1, default)
  /// <summary>
  ///A test for GetValue&st;string&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericString_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>(1, "") == "val1");
  }
  /// <summary>
  ///A test for GetValue&st;string&gt; with spaces
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericStringWithSpaces_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1 complex", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>(1, "") == "val1 complex");
  }
  /// <summary>
  ///A test for GetValue&st;int&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericInt_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<int>(1, 0) == 1236);
  }
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericDouble_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}2365", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<double>(1, 0, CultureInfo.CurrentCulture) == 1236.2365D);
  }
  /// <summary>
  ///A test for GetValue&st;long&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericLong_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=654321987", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<long>(1, 0) == 654321987L);
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericFloat_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}23", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<float>(1, 0, CultureInfo.CurrentCulture) == 1236.23F);
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericDateTime_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=12/6/1998", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<DateTime>(1, DateTime.MinValue, CultureInfo.GetCultureInfo("FR-BE")) == new DateTime(1998, 6, 12));
  }
  #endregion Tests for GetValue<T>(1, default)

  #region Tests for GetValue<T>(3, default)
  /// <summary>
  ///A test for GetValue&st;string&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericString_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1", "/verbose", "/par2=val2" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>(3, "") == "val2");
  }
  /// <summary>
  ///A test for GetValue&st;string&gt; with spaces
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericStringWithSpaces_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=val1 complex", "/verbose", "/par2=val2 complex" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>(3, "") == "val2 complex");
  }
  /// <summary>
  ///A test for GetValue&st;int&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericInt_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236", "/verbose", "/par2=98764" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<int>(3, 0) == 98764);
  }
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericDouble_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe",
                                                             $"/par1=1236{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}2365",
                                                             "/verbose",
                                                             $"/par2=654789{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}123456" };
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<double>(3, 0, CultureInfo.CurrentCulture) == 654789.123456D);
  }
  /// <summary>
  ///A test for GetValue&st;long&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericLong_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=654321987", "/verbose", "/par2=987641234" };
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<long>(3, 0) == 987641234L);
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericFloat_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", string.Format("/par1=1236{0}2365", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), "/verbose", string.Format("/par2=98764{0}1234", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) };
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<float>(3, 0, CultureInfo.CurrentCulture) == 98764.1234F);
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_Pos3GenericDateTime_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=12/6/1998", "/verbose", "/par2=28/04/1966" };
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<DateTime>(3, DateTime.MinValue, CultureInfo.GetCultureInfo("FR-BE")) == new DateTime(1966, 4, 28));
  }
  #endregion Tests for GetValue<T>(3, default)

  #region Tests for GetValue<T>(key, default, culture)
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericDoubleCultureUs_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236.2365", "/verbose" };
    Dump(Source);
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Dump(Args);
    Message("Get par1 as double");
    double Target = Args.GetValue<double>("par1", 0, CultureInfo.GetCultureInfo("en-us"));
    Dump(Target);
    Assert.AreEqual(Target, 1236.2365D);
    Ok();
  }

  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericFloatCultureUs_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236.23", "/verbose" };
    Dump(Source);
    ISplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Dump(Args);
    Message("Get par1 as float");
    float Target = Args.GetValue<float>("par1", 0, CultureInfo.GetCultureInfo("en-us"));
    Dump(Target);
    Assert.AreEqual(Target, 1236.23F);
    Ok();
  }

  /// <summary>
  ///A test for GetValue&st;Int&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericIntCultureUs_IsFalse() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1,236,123", "/verbose" };
    Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Dump(Args);
    Message("Get par1 as int, obtain default value");
    int Target = Args.GetValue<int>("par1", 0, CultureInfo.GetCultureInfo("en-us"));
    Dump(Target);
    Assert.AreNotEqual(Target, 1236123);
    Ok();
  }

  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericDateTimeCultureUs_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=6/12/1998", "/verbose" };
    Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Dump(Args);
    Message("Get par1 as DateTime");
    DateTime Target = Args.GetValue<DateTime>("par1", DateTime.MinValue, CultureInfo.GetCultureInfo("en-us"));
    Dump(Target);
    Assert.AreEqual(Target, new DateTime(1998, 6, 12));
    Ok();
  }
  #endregion Tests for GetValue<T>(key, default, culture)

  #region Tests for GetValue<T>(1, default, culture)
  /// <summary>
  ///A test for GetValue&st;double&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericDoubleCultureUs_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236.2365", "/verbose" };
    Dump(Source);
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Dump(Args);
    Message("Get first arg as double");
    double Target = Args.GetValue<double>(1, 0, CultureInfo.GetCultureInfo("en-us"));
    Dump(Target);
    Assert.AreEqual(Target, 1236.2365D);
    Ok();
  }
  /// <summary>
  ///A test for GetValue&st;Float&gt;
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericFloatCultureUs_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=1236.23", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<float>(1, 0, CultureInfo.GetCultureInfo("en-us")) == 1236.23F);
  }
  /// <summary>
  ///A test for GetValue&st;DateTime&gt;
  ///</summary>
  ///
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_PosGenericDateTimeCultureUs_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/par1=6/12/1998", "/verbose" };
    SplitArgs Args = new SplitArgs();
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<DateTime>(1, DateTime.MinValue, CultureInfo.GetCultureInfo("en-us")) == new DateTime(1998, 6, 12));
  }
  #endregion Tests for GetValue<T>(1, default, culture)

  /// <summary>
  ///A test for GetValue&st;string&gt; with case sensitivity
  ///</summary>
  [TestMethod(), TestCategory("SplitArgs")]
  public void GetValue_KeyGenericStringCaseSensitive_IsTrue() {
    IEnumerable<string> Source = new List<string>() { "program.exe", "/Par1=val1", "/par1=val1b" };
    SplitArgs Args = new SplitArgs() { IsCaseSensitive = true };
    Args.Parse(Source);
    Assert.IsTrue(Args.GetValue<string>("Par1", "") == "val1");
    Assert.IsTrue(Args.GetValue<string>("par1", "") == "val1b");
  }

  [TestMethod()]
  public void SplitArgs_OnlyKeyTestIndex_HasNoValue() {
    ISplitArgs Args = new SplitArgs();
    Assert.IsFalse(Args.HasValue(2));
  }

  [TestMethod()]
  public void SplitArgs_OnlyKeyTestKey_HasNoValue() {
    ISplitArgs Args = new SplitArgs();
    Assert.IsFalse(Args.HasValue("test"));
  }

  [TestMethod()]
  public void SplitArgs_OnlyKeyTestKey_HasValue() {
    ISplitArgs Args = new SplitArgs();
    Args.Parse("prog.exe test=value");
    Assert.IsTrue(Args.HasValue("test"));
  }

}
