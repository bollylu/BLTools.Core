namespace BLTools.Test.Diagnostic.Logging;

[TestClass]
public class TLoggerPoolTest {

  [TestMethod]
  public void Instanciate_TLoggerPool() {
    Message("Creating TLoggerPool");
    TLoggerPool Target = new TLoggerPool();
    Dump(Target);
    Ok();
  }

  [TestMethod]
  public void TLoggerPool_GetDefaultLogger() {
    Message("Creating TLoggerPool");
    TLoggerPool Source = new TLoggerPool();
    Dump(Source);

    Message($"Get default logger {nameof(TLoggerPoolTest).WithQuotes()}");
    ILogger Target = Source.GetDefaultLogger<TLoggerPoolTest>();
    Assert.IsNotNull(Target);
    Dump(Target);
    Ok();
  }

  [TestMethod]
  public void TLoggerPool_SetThenGetDefaultLogger() {
    Message("Creating TLoggerPool");
    TLoggerPool Source = new TLoggerPool();
    Dump(Source);

    Message("Set default logger");
    ILogger NewDefaultLogger = new TConsoleLogger<TLoggerPoolTest>();
    Dump(NewDefaultLogger);
    Source.SetDefaultLogger(NewDefaultLogger);

    Message("Get default logger");
    ILogger Target = Source.GetDefaultLogger<TLoggerPoolTest>();
    Assert.IsNotNull(Target);
    Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<TLoggerPoolTest>));
    Dump(Target);
    Ok();
  }

  [TestMethod]
  public void TLoggerPoolEmpty_SetDefault_GetLoggerByDefault_CastToType() {
    Message("Creating TLoggerPool");
    TLoggerPool Source = new TLoggerPool();
    Dump(Source);

    Message($"Set default logger : {nameof(TConsoleLogger<TLoggerPoolTest>).WithQuotes()}");
    Source.SetDefaultLogger(new TConsoleLogger<TLoggerPoolTest>());
    Dump(Source);

    Message($"Search for missing logger {nameof(TLoggerPool).WithQuotes()}");
    Message("Get the default logger instead");
    ILogger Target = Source.GetLogger<TLoggerPool>();
    Dump(Target);

    Assert.IsNotNull(Target);
    Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<TLoggerPool>));

    Ok();
  }

  [TestMethod]
  public void TLoggerPoolEmpty_GetLoggerByDefault_CastToType() {
    Message("Creating TLoggerPool");
    TLoggerPool Source = new TLoggerPool();
    Dump(Source);

    Message($"Search for missing logger {nameof(TLoggerPool).WithQuotes()}");
    ILogger Target = Source.GetLogger<TLoggerPool>();

    Message("Get the default logger instead");
    Assert.IsNotNull(Target);
    Assert.IsInstanceOfType(Target, typeof(TConsoleLogger));
    Assert.IsInstanceOfType(Target, typeof(TConsoleLogger<TLoggerPool>));
    Dump(Target);
    Ok();
  }



  [TestMethod]
  public void TLoggerPool_FillWithLoggers() {
    Message("Creating TLoggerPool");
    TLoggerPool Source = new TLoggerPool();

    Message("Adding Loggers");
    Source.AddLogger(new TConsoleLogger<TLoggerPool>());
    Source.AddLogger(new TFileLogger<TLoggerPoolTest>("test.log"));
    Source.AddLogger(new TConsoleLogger());
    Dump(Source);

    Message($"Get logger {nameof(TLoggerPoolTest).WithQuotes()}");
    ILogger Target = Source.GetLogger<TLoggerPoolTest>();
    Assert.IsNotNull(Target);
    Dump(Target);

    Message("logger type is ok");
    Assert.IsInstanceOfType(Target, typeof(TFileLogger<TLoggerPoolTest>));

    Ok();
  }

  [TestMethod]
  public void TLoggerPool_FillWithLoggers_Duplicate() {
    Message("Creating TLoggerPool");
    TLoggerPool Target = new TLoggerPool();
    Dump(Target);

    Message("Adding logger");
    ILogger Logger = new TConsoleLogger<TLoggerPool>();
    Dump(Logger);
    Target.AddLogger(Logger);
    Dump(Target);

    Message("Adding duplicate logger");
    ILogger DuplicateLogger = new TFileLogger<TLoggerPool>("test.log");
    Dump(DuplicateLogger);
    Assert.ThrowsException<TDuplicateLoggerException>(() => Target.AddLogger(DuplicateLogger));

    Message("Got exception");
    Ok();
  }

  [TestMethod]
  public void TLoggerPool_FillWithLoggers_DuplicateUnTyped() {
    Message("Creating TLoggerPool");
    TLoggerPool Target = new TLoggerPool();
    Dump(Target);

    Message("Adding logger");
    ILogger Logger = new TConsoleLogger();
    Dump(Logger);
    Target.AddLogger(Logger);
    Dump(Target);

    Message("Adding duplicate logger");
    ILogger DuplicateLogger = new TConsoleLogger();
    Dump(DuplicateLogger);
    Assert.ThrowsException<TDuplicateLoggerException>(() => Target.AddLogger(DuplicateLogger));

    Message("Got exception");
    Ok();
  }
}
