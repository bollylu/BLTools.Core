namespace BLTools.Test.Diagnostics;

public class DiagnosticsTest_Dump {

  private ILogger Logger => new TConsoleLogger<DiagnosticsTest_Dump>(TLoggerOptions.MessageOnly);

  [Test]
  public void Dump_SimpleType_String() {
    Logger.Message("Building Target");
    string Target = "A simple string";
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_SimpleType_Int() {
    Logger.Message("Building Target");
    int Target = 42;
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_SimpleType_Enum() {
    Logger.Message("Building Target");
    ETestEnum Target = ETestEnum.Second;
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_SimpleType_EnumWithFlags() {
    Logger.Message("Building Target");
    EColors TargetViolet = EColors.Red | EColors.Blue;
    Logger.Dump(TargetViolet);
    EColors TargetEmerald = EColors.Green | EColors.Blue;
    Logger.Dump(TargetEmerald);
    Logger.Ok();
  }

  [Test]
  public void Dump_ArrayOfInt() {
    Logger.Message("Building target array of int");
    int[] Target = { 1, 2, 3 };
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_ArrayOfString() {
    Logger.Message("Building target array of string");
    string[] Target = { "First", "Second", "Third" };
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_Dictionary() {
    Logger.Message("Building dictionary of string:string");
    Dictionary<string, string> Target = new Dictionary<string, string> {
      { "k1", "b" },
      { "k2", "d" },
      { "k3", "e" }
    };
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_Dictionary_WithNullValues() {
    Logger.Message("Building dictionary of string:string");
    Dictionary<string, string?> Target = new Dictionary<string, string?> {
      { "k1", null },
      { "k2", "d" },
      { "k3", "e" }
    };
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_Class() {
    Logger.Message("Building class");
    TDataHolder Target = new();
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_Interface_Holding_Class() {
    Logger.Message("Building class");
    IInnerData Target = new TDataHolder();
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_ComplexClass() {
    Logger.Message("Building class");
    TComplexDataHolder2 Target = new();
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_Struct() {
    Logger.Message("Building struct");
    SPerson Target = new SPerson() { Id = 1, Name = "luc" };
    Target.MoreData = new TMoreData2();
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_ArrayOfStruct() {
    Logger.Message("Building struct");
    SPerson P1 = new SPerson() { Id = 1, Name = "luc" };
    P1.MoreData = new TMoreData2();
    SPerson P2 = new SPerson() { Id = 2, Name = "eric" };
    SPerson[] Target = new SPerson[2];
    Target[0] = P1;
    Target[1] = P2;
    Logger.Dump(Target);
    Logger.Ok();
  }

  [Test]
  public void Dump_InheritedList() {
    Logger.Message("Building data");
    TContactFile Target = new TContactFile();
    SPerson NewCustomer = new SPerson() { Id = 3, Name = "LeNouveau" };
    NewCustomer.MoreData = new TMoreData();
    Target.Customers.Add(NewCustomer);
    Logger.Dump(Target);
    Logger.Ok();
  }
}
