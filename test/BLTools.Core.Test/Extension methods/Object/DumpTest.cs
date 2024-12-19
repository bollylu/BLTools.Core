using System.Globalization;

using BLTools.Core.Logging;

using NUnit.Compatibility;

namespace BLTools.Core.Test;

public enum ETestEnum { A, B, C }

public class TDemoData {
  public byte SourceByte => 28;
  public sbyte SourceSByte => -28;
  public int SourceInt => -42;
  public uint SourceUInt => 42;
  public Int16 SourceInt16 => -42;
  public UInt16 SourceUInt16 => 42;
  public long SourceLong => -1234567890123456789L;
  public ulong SourceULong => 1234567890123456789L;
  public UInt128 SourceUInt128 => UInt128.MaxValue;
  public float SourceFloat => 3.14f;
  public double SourceDouble => 3.14d;
  public decimal SourceDecimal => 168236.698745631m;
  public bool SourceBool => true;
  public string SourceString => "Hello world!";
  public char SourceChar => 'a';
  public ETestEnum SourceEnum => ETestEnum.A;
  public DateTime SourceToday => DateTime.Today;
  public TimeSpan SourceTimeSpan => DateTime.Now.Subtract(DateTime.Now.AddDays(-1));
  public DateOnly SourceDateOnly => DateOnly.FromDateTime(DateTime.Today);
  public TimeOnly SourceTimeOnly => TimeOnly.FromDateTime(DateTime.Now);

  [DoNotDump]
  public string DoNotDumpString => "Goodbye cruel world!";

  public static TDemoData Instance => new TDemoData();

}

public class TContainerData {
  public TDemoData Source1 => TDemoData.Instance;
  public TDemoData Source2 => TDemoData.Instance;

  public List<TDemoData> Sources => [TDemoData.Instance, TDemoData.Instance];
  public Dictionary<string, TDemoData> SourcesByString => new() {
    { "A", new TDemoData() },
    { "B", new TDemoData() }
  };
}

public class DumpTest {
  [Test]
  public void BasicValueDumpTest() {

    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);

    TDemoData DemoData = new();

    SObjectDumpOptions Options = SObjectDumpOptions.Default;
    Options.Culture = CultureInfo.CurrentCulture;

    ObjectExtension.DEFAULT_DUMP_OPTIONS = Options;

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceInt)} with value {DemoData.SourceInt}");
    Logger.Dump(DemoData.SourceInt);
    Assert.That(DemoData.SourceInt.Dump(), Is.EqualTo($"{DemoData.SourceInt.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceInt)} = {DemoData.SourceInt}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceFloat)} with value {DemoData.SourceFloat}");
    Logger.Dump(DemoData.SourceFloat);
    Assert.That(DemoData.SourceFloat.Dump(), Is.EqualTo($"{DemoData.SourceFloat.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceFloat)} = {DemoData.SourceFloat}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceDouble)} with value {DemoData.SourceDouble}");
    Logger.Dump(DemoData.SourceDouble);
    Assert.That(DemoData.SourceDouble.Dump(), Is.EqualTo($"{DemoData.SourceDouble.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceDouble)} = {DemoData.SourceDouble}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceBool)} with value {DemoData.SourceBool}");
    Logger.Dump(DemoData.SourceBool);
    Assert.That(DemoData.SourceBool.Dump(), Is.EqualTo($"{DemoData.SourceBool.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceBool)} = {DemoData.SourceBool}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceString)} with value {DemoData.SourceString.WithQuotes()}");
    Logger.Dump(DemoData.SourceString);
    Assert.That(DemoData.SourceString.Dump(), Is.EqualTo($"{DemoData.SourceString.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceString)} = {DemoData.SourceString.WithQuotes()}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceChar)} with value {DemoData.SourceChar.WithQuotes()}");
    Logger.Dump(DemoData.SourceChar);
    Assert.That(DemoData.SourceChar.Dump(), Is.EqualTo($"{DemoData.SourceChar.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceChar)} = {DemoData.SourceChar.WithQuotes()}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.DoNotDumpString)} with value {DemoData.DoNotDumpString.WithQuotes()}");
    Logger.Dump(DemoData.DoNotDumpString);
    Assert.That(DemoData.DoNotDumpString.Dump(), Is.EqualTo($"{DemoData.DoNotDumpString.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.DoNotDumpString)} = {DemoData.DoNotDumpString.WithQuotes()}"));

    Logger.Ok();
  }

  [Test]
  public void ClassContentDumpTest() {

    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);

    Logger.Message($"Instanciate {nameof(TDemoData)}");
    TDemoData DemoData = new TDemoData();

    Logger.Message($"Dumping {nameof(DemoData)}");
    Logger.Dump(DemoData);

    Assert.That(DemoData.Dump(), Does.Not.Contain("DoNotDumpString"));

    Logger.Ok();
  }

  [Test]
  public void ClassMultiContentDumpTest() {

    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);

    Logger.Message($"Instanciate {nameof(TContainerData)}");
    TContainerData Container = new();

    SObjectDumpOptions Options = SObjectDumpOptions.Default;
    Options.WithTitle = true;
    Logger.Message($"Dumping {nameof(Container)}");
    Logger.Dump(Container, Options);

    Assert.That(Container.Dump(), Does.Not.Contain("DoNotDumpString"));

    Logger.Ok();
  }

  [Test]
  public void BasicValueDumpBoxTest() {

    using ILogger Logger = new TConsoleLogger<DumpTest>(TLoggerOptions.MessageOnly);

    TDemoData DemoData = new();

    SObjectDumpOptions Options = SObjectDumpOptions.Default;
    Options.Culture = CultureInfo.CurrentCulture;

    ObjectExtension.DEFAULT_DUMP_OPTIONS = Options;

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceInt)} with value {DemoData.SourceInt}");
    Logger.DumpBox(DemoData.SourceInt);
    Assert.That(DemoData.SourceInt.Dump(), Is.EqualTo($"{DemoData.SourceInt.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceInt)} = {DemoData.SourceInt}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceFloat)} with value {DemoData.SourceFloat}");
    Logger.DumpBox(DemoData.SourceFloat);
    Assert.That(DemoData.SourceFloat.Dump(), Is.EqualTo($"{DemoData.SourceFloat.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceFloat)} = {DemoData.SourceFloat}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceDouble)} with value {DemoData.SourceDouble}");
    Logger.DumpBox(DemoData.SourceDouble);
    Assert.That(DemoData.SourceDouble.Dump(), Is.EqualTo($"{DemoData.SourceDouble.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceDouble)} = {DemoData.SourceDouble}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceBool)} with value {DemoData.SourceBool}");
    Logger.DumpBox(DemoData.SourceBool);
    Assert.That(DemoData.SourceBool.Dump(), Is.EqualTo($"{DemoData.SourceBool.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceBool)} = {DemoData.SourceBool}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceString)} with value {DemoData.SourceString.WithQuotes()}");
    Logger.DumpBox(DemoData.SourceString);
    Assert.That(DemoData.SourceString.Dump(), Is.EqualTo($"{DemoData.SourceString.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceString)} = {DemoData.SourceString.WithQuotes()}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.SourceChar)} with value {DemoData.SourceChar.WithQuotes()}");
    Logger.DumpBox(DemoData.SourceChar);
    Assert.That(DemoData.SourceChar.Dump(), Is.EqualTo($"{DemoData.SourceChar.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.SourceChar)} = {DemoData.SourceChar.WithQuotes()}"));

    Logger.Message($"Dumping {nameof(DemoData)}.{nameof(DemoData.DoNotDumpString)} with value {DemoData.DoNotDumpString.WithQuotes()}");
    Logger.DumpBox(DemoData.DoNotDumpString);
    Assert.That(DemoData.DoNotDumpString.Dump(), Is.EqualTo($"{DemoData.DoNotDumpString.GetType().GetNameEx()} {nameof(DemoData)}.{nameof(DemoData.DoNotDumpString)} = {DemoData.DoNotDumpString.WithQuotes()}"));

    Logger.Ok();
  }
}
