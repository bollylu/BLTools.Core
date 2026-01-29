using System.Collections.Specialized;

namespace BLTools.Core;

/// <summary>
/// Splits arguments of CommandLine. You can use either / or - as parameter prefix or nothing but the keyword.
/// If values are specified, they are separated from the keyword by an = sign.
/// (c) 2004-2012 Luc Bolly
/// </summary>
public class SplitArgs : ISplitArgs {

  /// <summary>
  /// The keys/values internal storage
  /// </summary>
  protected List<IArgElement> _Items = [];

  /// <summary>
  /// Default culture for conversions
  /// </summary>
  public readonly static CultureInfo DEFAULT_CULTURE_INFO = CultureInfo.InvariantCulture;

  #region Public properties
  /// <inheritdoc/>
  public CultureInfo CurrentCultureInfo {get; set { field = value ?? DEFAULT_CULTURE_INFO; } } = DEFAULT_CULTURE_INFO;
  

  /// <inheritdoc/>
  public bool IsCaseSensitive { get; set; } = false;

  /// <inheritdoc/>
  public char Separator { get; set; } = ';';

  /// <inheritdoc/>
  public char KeyValueSeparator { get; set; } = '=';
  #endregion Public properties

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// Creates an empty dictonnary of command line arguments
  /// </summary>
  public SplitArgs() {
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  #region --- Converters -------------------------------------------------------------------------------------
  /// <inheritdoc/>
  public override string ToString() {
    StringBuilder RetVal = new StringBuilder();
    RetVal.AppendLine($"- {_Items.Count} {nameof(IArgElement)} in list");
    foreach (IArgElement ArgItem in _Items) {
      RetVal.AppendIndent(ArgItem.ToString(0), 2);
    }

    return RetVal.ToString();
  }
  #endregion --- Converters -------------------------------------------------------------------------------------

  #region --- Parse input --------------------------------------------
  /// <inheritdoc/>
  public void Parse(string cmdLine) {
    #region Validate parameters
    if (cmdLine is null) {
      throw new ArgumentNullException(nameof(cmdLine), "you must pass a valid string cmdLine argument");
    }
    #endregion Validate parameters

    string PreprocessedLine = cmdLine.Trim();
    List<string> CmdLineValues = [];
    StringBuilder TempStr = new StringBuilder();

    int i = 0;
    bool InQuote = false;

    int PreprocessedLineLength = PreprocessedLine.Length;

    while (i < PreprocessedLineLength) {

      if (PreprocessedLine[i] == '"') {
        InQuote = !InQuote;
        i++;
        continue;
      }

      if (PreprocessedLine[i] == ' ') {
        if (InQuote) {
          TempStr.Append(PreprocessedLine[i]);
          i++;
          continue;

        }

        if (!(TempStr.Length == 0)) {
          CmdLineValues.Add(TempStr.ToString());
          TempStr.Clear();
          i++;
          continue;
        }

      }

      if (PreprocessedLine[i] != '"') {
        TempStr.Append(PreprocessedLine[i]);
        i++;
        continue;
      }

    }

    if (TempStr.Length != 0) {
      CmdLineValues.Add(TempStr.ToString());
    }

    Parse(CmdLineValues.ToArray());
  }

  /// <inheritdoc/>
  public void Parse(IEnumerable<string> arrayOfValues) {
    int Position = 0;
    foreach (string ValueItem in arrayOfValues) {
      string ValueItemProcessed;

      if (ValueItem.StartsWith('/') || ValueItem.StartsWith('-')) {
        ValueItemProcessed = ValueItem[1..];
      } else {
        ValueItemProcessed = ValueItem;
      }

      if (ValueItemProcessed.Contains(KeyValueSeparator)) {
        if (IsCaseSensitive) {
          _Items.Add(new ArgElement(Position, ValueItemProcessed.Before(KeyValueSeparator).TrimStart(), ValueItemProcessed.After(KeyValueSeparator).TrimEnd()));
        } else {
          _Items.Add(new ArgElement(Position, ValueItemProcessed.Before(KeyValueSeparator).TrimStart().ToLower(CurrentCultureInfo), ValueItemProcessed.After(KeyValueSeparator).TrimEnd()));
        }
      } else {
        if (IsCaseSensitive) {
          _Items.Add(new ArgElement(Position, ValueItemProcessed.Trim(), ""));
        } else {
          _Items.Add(new ArgElement(Position, ValueItemProcessed.Trim().ToLower(CurrentCultureInfo), ""));
        }
      }

      Position++;
    }
  }


  /// <inheritdoc/>
  public void Parse(NameValueCollection queryStringItems) {
    #region === Validate parameters ===
    if (queryStringItems == null || queryStringItems.Count == 0) {
      return;
    }
    #endregion === Validate parameters ===
    foreach (string QueryStringItem in queryStringItems) {
      _Items.Add(new ArgElement(0, QueryStringItem, queryStringItems[QueryStringItem] ?? ""));
    }
  }
  #endregion --- Parse input --------------------------------------------

  #region --- Elements management --------------------------------------------
  /// <inheritdoc/>
  public void Clear() {
    _Items.Clear();
  }

  /// <inheritdoc/>
  public int Count() {
    return _Items.Count;
  }

  /// <inheritdoc/>
  public void Add(IArgElement element) {
    _Items.Add(element);
  }


  /// <inheritdoc/>
  public IArgElement? this[int index] {
    get {
      IArgElement? CurrentElement = _Items.FirstOrDefault(a => a.Id == index);
      if (CurrentElement is null) {
        return new ArgElement(0, "", "");
      } else {
        return CurrentElement;
      }
    }
  }

  /// <inheritdoc/>
  public IArgElement? this[string key] {
    get {
      IArgElement? CurrentElement = _Items.FirstOrDefault(a => a.Name == key);
      if (CurrentElement is null) {
        return new ArgElement(0, "", "");
      } else {
        return CurrentElement;
      }
    }
  }

  /// <inheritdoc/>
  public IEnumerable<IArgElement> GetAll() {
    if (_Items.IsEmpty()) {
      yield break;
    }

    foreach (IArgElement ArgElementItem in _Items) {
      yield return ArgElementItem;
    }
  }

  #endregion --- Elements management --------------------------------------------

  #region --- Tests on keys/values --------------------------------------------
  /// <inheritdoc/>
  public bool IsDefined(string key) {
    if (key == null || _Items.IsEmpty()) {
      return false;
    }
    IArgElement? CurrentElement;

    if (IsCaseSensitive) {
      CurrentElement = _Items.FirstOrDefault(a => a.Name == key);
    } else {
      string KeyLower = key.ToLower(CurrentCultureInfo);
      CurrentElement = _Items.FirstOrDefault(a => a.Name.Equals(KeyLower, StringComparison.CurrentCultureIgnoreCase));
    }
    return CurrentElement != null;
  }

  /// <inheritdoc/>
  public bool HasValue(string key) {
    #region === Validate parameters ===
    if (string.IsNullOrWhiteSpace(key) || !IsDefined(key)) {
      return false;
    }
    #endregion === Validate parameters ===

    IArgElement? CurrentElement;

    if (IsCaseSensitive) {
      CurrentElement = _Items.FirstOrDefault(a => a.Name == key);
    } else {
      CurrentElement = _Items.FirstOrDefault(a => a.Name.Equals(key, StringComparison.CurrentCultureIgnoreCase));

    }
    return CurrentElement?.HasValue() ?? false;

  }

  /// <inheritdoc/>
  public bool HasValue(int index) {
    if (_Items.IsEmpty()) {
      return false;
    }

    if (index < 0 || index > (_Items.Count - 1)) {
      return false;
    }

    IArgElement CurrentElement = _Items[index];

    return CurrentElement?.HasValue() ?? false;

  }

  /// <inheritdoc/>
  public bool Any() {
    return _Items.Any();
  }

  /// <inheritdoc/>
  public bool IsEmpty() {
    return _Items.IsEmpty();
  }
  #endregion --- Tests on keys/values --------------------------------------------

  #region Generic version of the GetValue

  /// <inheritdoc/>
  public T? GetValue<T>(string key) {
    return GetValue(key, default(T), CurrentCultureInfo);
  }

  /// <inheritdoc/>
  public T GetValue<T>(string key, T defaultValue) {
    return GetValue(key, defaultValue, CurrentCultureInfo);
  }

  /// <inheritdoc/>
  public T GetValue<T>(string key, T defaultValue, CultureInfo culture) {
    if (key is null || _Items.IsEmpty()) {
      return defaultValue;
    }

    try {
      IArgElement? CurrentElement;

      if (IsCaseSensitive) {
        CurrentElement = _Items.FirstOrDefault(a => a.Name.Equals(key, StringComparison.InvariantCulture));
      } else {
        CurrentElement = _Items.FirstOrDefault(a => a.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase));
      }

      if (CurrentElement is not null) {
        return CurrentElement.Value.Parse(defaultValue, culture);
      } else {
        return defaultValue;
      }
    } catch {
      return defaultValue;
    }
  }

  /// <inheritdoc/>
  public T? GetValue<T>(int position) {
    return GetValue(position, default(T), CurrentCultureInfo);
  }

  /// <inheritdoc/>
  public T GetValue<T>(int position, T defaultValue) {
    return GetValue(position, defaultValue, CurrentCultureInfo);
  }

  /// <inheritdoc/>
  public T GetValue<T>(int position, T defaultValue, CultureInfo culture) {
    if (_Items.IsEmpty() || position.IsOutsideRange(0, _Items.Count)) {
      return defaultValue;
    }
    IArgElement CurrentElement = _Items[position];
    return CurrentElement.Value.Parse(defaultValue, culture);

  }
  #endregion Generic version of the GetValue

}
