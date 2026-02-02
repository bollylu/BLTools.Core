using BLTools.Core.Text;

using static BLTools.Core.Text.TextBox;

namespace BLTools.Core.Test.Text;

public class TextTest {

  [Test]
  public void TextBox_FixedWidthDefault_ResultOk() {
    string source = "Sample text";
    string FormattedSource = source.PadRight(DEFAULT_FIXED_WIDTH - 4);
    string TopAndBottom = $"+{new string('-', DEFAULT_FIXED_WIDTH - 2)}+";
    string ExpectedResult = $"{TopAndBottom}{Environment.NewLine}| {FormattedSource} |{Environment.NewLine}{TopAndBottom}";
    string actual = BuildFixedWidth(source, ETextBoxAlignment.Left);
    Assert.That(actual, Is.EqualTo(ExpectedResult));
  }

  [Test]
  public void TextBox_FixedWidth40_ResultOk() {
    int BoxWidth = 40;
    string source = "Sample text";
    string FormattedSource = source.PadRight(BoxWidth - 4);
    string TopAndBottom = $"+{new string('-', BoxWidth - 2)}+";
    string ExpectedResult = $"{TopAndBottom}{Environment.NewLine}| {FormattedSource} |{Environment.NewLine}{TopAndBottom}";
    string actual = BuildFixedWidth(source, BoxWidth, ETextBoxAlignment.Left);
    Assert.That(actual, Is.EqualTo(ExpectedResult));
  }

  [Test]
  public void TextBox_HorizontalRowDouble40_ResultOk() {
    string ExpectedResult = new string('=', 40);
    string actual = TTextHRow.BuildHorizontalRow(40, EHorizontalRowType.Double);
    Assert.That(actual, Is.EqualTo(ExpectedResult));
  }


}
