using BLTools.Core.Logging;

namespace BLTools.Core.Text.Test;

public class TextBoxTest {

  [Test]
  public void InstanciateTextBox() {

    using ILogger Logger = new TConsoleLogger<TextBoxTest>(TLoggerOptions.MessageOnly);

    Logger.Message("Instanciate a TextBox");
    ITextBox Target = new TTextBox();
    Assert.That(Target, Is.Not.Null);

    Logger.DumpBox(Target);

    Assert.That(Target.Options, Is.Not.Null);
    Assert.That(Target.Options.IsDynamicWidth, Is.False);
    Assert.That(Target.Options.TextBoxType, Is.EqualTo(ETextBoxType.Standard));

    Logger.Ok();

  }

  [Test]
  public void TextBoxRender() {

    using ILogger Logger = new TConsoleLogger<TextBoxTest>(TLoggerOptions.MessageOnly);

    Logger.Message("Instanciate a TextBox");
    ITextBox Target = new TTextBox();
    Assert.That(Target, Is.Not.Null);
    Logger.DumpBox(Target);

    Logger.Message("Render the empty TextBox as standard box");
    Target.Options.Border = TTextBoxBorder.Standard;
    Logger.Log(Target.Render());

    Logger.Message("Render the empty TextBox as IBM box");
    Target.Options.Border = TTextBoxBorder.Ibm;
    Logger.Log(Target.Render());

    Logger.Message("Render the empty TextBox as IBM double box");
    Target.Options.Border = TTextBoxBorder.IbmDouble;
    Logger.Log(Target.Render());

    Logger.Message("Add a title and render the empty TextBox as standard box");
    Target.Options.Border = TTextBoxBorder.Standard;
    Target.Title = "This is a title";
    Logger.Log(Target.Render());

    Logger.Message("Add a title and a content and render the empty TextBox as standard box");
    Target.Options.Border = TTextBoxBorder.Standard;
    Target.Title = "This is a title";
    Target.Content = "This is a content";
    Logger.Log(Target.Render());

    Logger.Message("Add a title and a content and render the empty TextBox as IBM box");
    Target.Options.Border = TTextBoxBorder.Ibm;
    Target.Title = "This is a title";
    Target.Content = "This is a content";
    Logger.Log(Target.Render());

    Logger.Ok();

  }
}
