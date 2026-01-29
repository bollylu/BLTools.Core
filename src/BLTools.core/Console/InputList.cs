namespace BLTools.Core;

static public partial class ConsoleExt {

  /// <summary>
  /// Allow a user at the console to select a response from a list
  /// </summary>
  /// <param name="possibleValues">Dictionnary of the possible values</param>
  /// <param name="title">Title of the list</param>
  /// <param name="question">Prompt at the bottom of the list</param>
  /// <param name="errorMessage">Message to display in case of erroneous value</param>
  /// <returns>The key of the selected dictionnary item</returns>
  public static int InputList(IDictionary<int, string> possibleValues, string title = "", string question = "", string errorMessage = "") {
    bool IsOk;
    int Answer;
    do {
      DisplayList(possibleValues, title);

      Answer = Input<int>(question, EInputValidation.Mandatory);

      if (possibleValues.ContainsKey(Answer)) {
        IsOk = true;
      } else {
        Console.WriteLine(errorMessage);
        IsOk = false;
      }

    } while (!IsOk);

    return Answer;
  }

  /// <summary>
  /// Allow a user at the console to select a response from a list
  /// </summary>
  /// <param name="possibleValues">Dictionnary of the possible values</param>
  /// <param name="title">Title of the list</param>
  /// <param name="question">Prompt at the bottom of the list</param>
  /// <param name="errorAction">The action to execute in case of error</param>
  /// <returns>The key of the selected dictionnary item</returns>
  public static int InputList(IDictionary<int, string> possibleValues, string title = "", string question = "", Action? errorAction = null) {
    bool IsOk = true;
    int Answer;

    DisplayList(possibleValues, title);

    int CurrentRow = Console.CursorTop;
    int CurrentCol = Console.CursorLeft;

    do {

      Console.SetCursorPosition(CurrentCol, CurrentRow);
      Answer = Input<int>(question, EInputValidation.Unknown);

      if (possibleValues.ContainsKey(Answer)) {
        IsOk = true;
      } else {
        errorAction?.Invoke();
        IsOk = false;
      }

    } while (!IsOk);

    return Answer;
  }

  /// <summary>
  /// Allow a user at the console to select a response from a list
  /// </summary>
  /// <param name="items">List of the possible values</param>
  /// <param name="title">Title of the list</param>
  /// <param name="question">Prompt at the bottom of the list</param>
  /// <param name="errorMessage">Message to display in case of erroneous value</param>
  /// <returns>The key of the selected item</returns>
  public static int InputList(IEnumerable<string> items, string title = "", string question = "", string errorMessage = "") {
    Dictionary<int, string> DictionaryItems = [];
    int i = 1;
    foreach (string ItemItem in items) {
      DictionaryItems.Add(i++, ItemItem);
    }
    return InputList(DictionaryItems, title, question, errorMessage);
  }

  private static void DisplayList(IDictionary<int, string> possibleValues, string title) {
    if (title != "") {
      Console.WriteLine($"[--{title}--]");
    }
    foreach (KeyValuePair<int, string> ValueItem in possibleValues) {
      Console.WriteLine($"  {ValueItem.Key}. {ValueItem.Value}");
    }
  }


}
