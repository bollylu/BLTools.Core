namespace BLTools.Test.Cli;

/// <summary>
///This is a test class for ArgElementTest and is intended
///to contain all ArgElementTest Unit Tests
///</summary>
public class ArgElementTest {

  /// <summary>
  ///A test for ArgElement Constructor
  ///</summary>
  [Test]
  public void ArgElementConstructorTest() {
    int id = 0;
    string name = "first";
    string value = "first value";
    ArgElement target = new ArgElement(id, name, value);
    Assert.That(target.Id == 0, "Id should be 0");
    Assert.That(target.Name == name, $"Name should be {name}");
    Assert.That(target.Value == value, $"Value should be {value}");
  }

  /// <summary>
  ///A test for Id
  ///</summary>
  [Test]
  public void IdTest() {
    int id = 0;
    string name = "first";
    string value = "first value";
    ArgElement target = new ArgElement(id, name, value);
    Assert.That(target.Id, Is.EqualTo(0));
  }

  /// <summary>
  ///A test for Name
  ///</summary>
  [Test]
  public void NameTest() {
    int id = 0;
    string name = "first";
    string value = "first value";
    ArgElement target = new ArgElement(id, name, value);
    Assert.That(target.Name, Is.EqualTo(name));
  }

  /// <summary>
  ///A test for Value
  ///</summary>
  [Test]
  public void ValueTest() {
    int id = 0;
    string name = "first";
    string value = "first value";
    ArgElement target = new ArgElement(id, name, value);
    Assert.That(target.Value, Is.EqualTo(value));
  }
}
