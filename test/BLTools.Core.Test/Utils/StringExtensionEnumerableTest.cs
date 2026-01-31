namespace BLTools.Core.Test.Extensions.StringEx.IEnumerableEx;

public class StringExtensionEnumerableTest {

  private const string COMPUTER = "Computer";
  private const string PERSONAL_COMPUTER = "Personal computers";
  private const string MAINFRAME = "Mainframe";
  private const string POCKET_PC = "Pocket PC";
  private const string TELEVISION_UK = "Television";
  private const string TELEVISION_FR = "Télévision";
  private const string CHAINE_HIFI = "Chaîne hifi";

  private readonly string[] ListOfStrings = [COMPUTER, PERSONAL_COMPUTER, MAINFRAME, POCKET_PC, TELEVISION_FR];

  [Test]
  public void SearchIsIn_StringInTheList_ResultTrue() {
    Assert.That(COMPUTER.IsIn(ListOfStrings), Is.True);
  }

  [Test]
  public void SearchIsIn_StringNotInTheList_ResultFalse() {
    Assert.That(CHAINE_HIFI.IsIn(ListOfStrings), Is.False);
  }

  [Test]
  public void SearchIsIn_StringInTheListIgnoreCase_ResultTrue() {
    Assert.That(MAINFRAME.ToLower().IsIn(ListOfStrings), Is.True);
  }

  [Test]
  public void SearchIsIn_StringInTheListCase_ResultFalse() {
    Assert.That(MAINFRAME.ToLower().IsIn(ListOfStrings, StringComparison.InvariantCulture), Is.False);
  }

  [Test]
  public void SearchIsIn_StringInTheListCaseFrench_ResultTrue() {
    CultureInfo OldCulture = CultureInfo.CurrentCulture;
    CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("FR-fr");
    Assert.That(TELEVISION_UK.IsIn(ListOfStrings, StringComparison.CurrentCulture), Is.False);
    Assert.That(TELEVISION_FR.IsIn(ListOfStrings, StringComparison.CurrentCulture), Is.True );
    CultureInfo.CurrentCulture = OldCulture;
  }

  [Test]
  public void SearchIsNotIn_StringInNotTheList_ResultTrue() {
    Assert.That(CHAINE_HIFI.IsNotIn(ListOfStrings), Is.True);
  }

  [Test]
  public void SearchIsNotIn_StringNotInTheList_ResultFalse() {
    Assert.That(COMPUTER.IsNotIn(ListOfStrings),Is.False);
  }
}
