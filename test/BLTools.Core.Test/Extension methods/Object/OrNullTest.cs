
namespace BLTools.Core.Test;

public class OrNullTest {
  [Test]
  public void OrNullTest_IsNull() {
    using (ILogger Logger = new TConsoleLogger<OrNullTest>()) {

      Logger.Message("Instanciate a null string");
      string? Source = null;
      Logger.Message("The string is null");
      Assert.That(Source, Is.Null);
      Logger.Message($"The OrNull returns {ObjectExtension.VALUE_NULL}");
      Logger.Message(Source.OrNull().Dump());
      Assert.That(Source.OrNull(), Is.Not.Null);
      Assert.That(Source.OrNull(), Is.EqualTo(ObjectExtension.VALUE_NULL));

      Logger.Message("Instanciate a null object");
      object? SourceObject = null;
      Logger.Message("The object is null");
      Assert.That(SourceObject, Is.Null);
      Logger.Message($"The OrNull returns {ObjectExtension.VALUE_NULL}");
      Logger.Message(SourceObject.OrNull().Dump());
      Assert.That(SourceObject.OrNull(), Is.Not.Null);
      Assert.That(SourceObject.OrNull(), Is.EqualTo(ObjectExtension.VALUE_NULL));

      Logger.Ok();
    }
  }
}
