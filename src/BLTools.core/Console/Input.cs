namespace BLTools.Core;

/// <summary>
/// Extensions to the console
/// </summary>
public static partial class ConsoleExt {
  /// <summary>
  /// Display a message to the console and wait for an answer. Return the entered value converted to requested type or default value for this type in case of convert error
  /// </summary>
  /// <typeparam name="T">Requested type of the return value</typeparam>
  /// <param name="questionMessage">Message to display on the console</param>
  /// <param name="optionFlags">Validation option for the answer (mandatory, alpha, ...)</param>
  /// <param name="errorMessage">Message to display in case of error</param>
  /// <returns>Entered value converted to the requested type or default for this type in case of convert error</returns>
  static public T? Input<T>(string questionMessage = "", EInputValidation optionFlags = EInputValidation.Unknown, string errorMessage = "") {
    bool IsOk = true;
    string? AnswerAsString = "";
    do {

      if (optionFlags.HasFlag(EInputValidation.Mandatory)) {
        Console.Write($"* {questionMessage}");
      } else {
        Console.Write(questionMessage);
      }

      AnswerAsString = Console.ReadLine();
      if (AnswerAsString is null) {
        continue;
      }

      if (optionFlags.HasFlag(EInputValidation.Mandatory) && AnswerAsString == "") {
        IsOk = false;
      } else if (optionFlags.HasFlag(EInputValidation.IsNumeric) && !AnswerAsString.IsNumeric()) {
        IsOk = false;
      } else if (optionFlags.HasFlag(EInputValidation.IsAlpha) && !AnswerAsString.IsAlpha()) {
        IsOk = false;
      } else if (optionFlags.HasFlag(EInputValidation.IsAlphaNumeric) && !AnswerAsString.IsAlphaNumeric()) {
        IsOk = false;
      } else if (optionFlags.HasFlag(EInputValidation.IsAlphaNumericAndSpacesAndDashes) && !AnswerAsString.IsAlphaNumericOrBlankOrDashes()) {
        IsOk = false;
      } else {
        IsOk = true;
      }

      if (!IsOk) {
        Console.WriteLine(errorMessage);
      }

    } while (!IsOk);

    if (AnswerAsString is null) {
      return default(T?);
    }

    return BLConverter.BLConvert(AnswerAsString, System.Globalization.CultureInfo.CurrentCulture, default(T?));
  }

  /// <summary>
  /// Ask a question, validate with a predicate, execute action in case of error
  /// </summary>
  /// <typeparam name="T">The type of data to request</typeparam>
  /// <param name="questionMessage">The question to display</param>
  /// <param name="predicate">The predicate used to validate the answer</param>
  /// <param name="errorAction">The action in case of error</param>
  /// <returns>The data converted and validated</returns>
  static public T? Input<T>(string questionMessage, Predicate<string> predicate, Action errorAction) {

    predicate ??= new Predicate<string>(a => a != "");

    string? AnswerAsString;
    bool IsOk = false;

    do {

      Console.Write(questionMessage);

      AnswerAsString = Console.ReadLine();
      if (AnswerAsString is null) {
        continue;
      }

      IsOk = predicate(AnswerAsString);

    } while (!IsOk);

    if (AnswerAsString is null) {
      return default(T?);
    }
    return BLConverter.BLConvert(AnswerAsString, System.Globalization.CultureInfo.CurrentCulture, default(T?));
  }
}
