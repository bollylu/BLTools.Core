namespace BLTools.Core;

public static partial class IEnumerableExtension {

  #region --- IEnumerable<T> as pattern within IEnumerable<T> --------------------------------------------
  /// <summary>
  /// Test if one IEnumerable starts with another IEnumerable
  /// </summary>
  /// <typeparam name="T">The type of items to compare</typeparam>
  /// <param name="source">The first IEnumerable</param>
  /// <param name="pattern">The second IEnumerable</param>
  /// <returns><see langword="true"/> if the first IEnumerable starts with the second IEnumerable</returns>
  public static bool StartsWith<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source is null && pattern is null) {
      return true;
    }

    if (source is null || pattern is null) {
      return false;
    }

    if (source.IsEmpty() || pattern.IsEmpty()) {
      return false;
    }

    if (pattern.Count() > source.Count()) {
      return false;
    }
    #endregion === Validate parameters ===

    for (int i = 0; i < pattern.Count(); i++) {
      if (!source.ElementAt(i).Equals(pattern.ElementAt(i))) {
        return false;
      }
    }

    return true;
  }

  /// <summary>
  /// Test if one IEnumerable ends with another IEnumerable
  /// </summary>
  /// <typeparam name="T">The type of items to compare</typeparam>
  /// <param name="source">The first IEnumerable</param>
  /// <param name="pattern">The second IEnumerable</param>
  /// <returns><see langword="true"/> if the first IEnumerable ends with the second IEnumerable</returns>
  public static bool EndsWith<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source == null && pattern == null) {
      return true;
    }

    if (source == null || pattern == null) {
      return false;
    }

    if (source.IsEmpty() || pattern.IsEmpty()) {
      return false;
    }

    if (pattern.Count() > source.Count()) {
      return false;
    }
    #endregion === Validate parameters ===

    IEnumerable<T> ValuesToCompare = source.TakeLast(pattern.Count());

    for (int i = 0; i < ValuesToCompare.Count(); i++) {
      if (!pattern.ElementAt(i).Equals(ValuesToCompare.ElementAt(i))) {
        return false;
      }
    }

    return true;
  }

  /// <summary>
  /// Get an IEnumerable content after some IEnumerable pattern
  /// </summary>
  /// <typeparam name="T">The type of items</typeparam>
  /// <param name="source">The IEnumerable to get a piece of</param>
  /// <param name="pattern">The pattern matching the inferior limit</param>
  /// <returns>Items after the pattern</returns>
  public static IEnumerable<T> After<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source == null || pattern == null) {
      yield break;
    }

    if (source.IsEmpty() || pattern.IsEmpty()) {
      yield break;
    }

    if (pattern.Count() > source.Count()) {
      yield break;
    }
    #endregion === Validate parameters ===

    bool Found = false;
    IEnumerable<T> TempSource = source;
    int i = 1;
    while (!Found && i < (source.Count() - pattern.Count())) {
      if (TempSource.StartsWith(pattern)) {
        Found = true;
      } else {
        TempSource = source.Skip(i++);
      }
    }

    if (Found) {
      foreach (T Item in TempSource.Skip(pattern.Count())) {
        yield return Item;
      }
    }

  }

  /// <summary>
  /// Get an IEnumerable content after the last occurence of some IEnumerable pattern
  /// </summary>
  /// <typeparam name="T">The type of items</typeparam>
  /// <param name="source">The IEnumerable to get a piece of</param>
  /// <param name="pattern">The pattern matching the inferior limit</param>
  /// <returns>Items after the last occurence of the pattern</returns>
  public static IEnumerable<T> AfterLast<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (pattern == null) {
      yield break;
    }
    if (source.IsEmpty() || pattern.IsEmpty()) {
      yield break;
    }
    if (pattern.Count() > source.Count()) {
      yield break;
    }
    #endregion === Validate parameters ===

    bool Found = false;

    int i = pattern.Count();
    IEnumerable<T> TempSource = source.TakeLast(i);
    while (!Found && i <= source.Count()) {
      if (TempSource.StartsWith(pattern)) {
        Found = true;
      } else {
        TempSource = source.TakeLast(++i);
      }
    }

    if (Found) {
      foreach (T Item in TempSource.Skip(pattern.Count())) {
        yield return Item;
      }
    }

  }

  /// <summary>
  /// Get an IEnumerable content before an IEnumerable pattern
  /// </summary>
  /// <typeparam name="T">The type of items</typeparam>
  /// <param name="source">The IEnumerable to get a piece of</param>
  /// <param name="pattern">The pattern matching the superior limit</param>
  /// <returns>Items before the pattern</returns>
  public static IEnumerable<T> Before<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (pattern is null) {
      yield break;
    }
    if (source.IsEmpty() || pattern.IsEmpty()) {
      yield break;
    }
    if (pattern.Count() > source.Count()) {
      yield break;
    }
    #endregion === Validate parameters ===

    bool Found = false;
    IEnumerable<T> TempSource = source;
    int i = 0;
    while (!Found && i <= (source.Count() - pattern.Count())) {
      if (TempSource.StartsWith(pattern)) {
        Found = true;
      } else {
        TempSource = source.Skip(++i);
      }
    }

    if (Found) {
      foreach (T Item in source.Take(i)) {
        yield return Item;
      }
    }

  }

  /// <summary>
  /// Get an IEnumerable content before the last occurence of an IEnumerable pattern
  /// </summary>
  /// <typeparam name="T">The type of items</typeparam>
  /// <param name="source">The IEnumerable to get a piece of</param>
  /// <param name="pattern">The pattern matching the superior limit</param>
  /// <returns>Items before the last occurence of the pattern</returns>
  public static IEnumerable<T> BeforeLast<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (pattern == null) {
      yield break;
    }
    if (source.IsEmpty() || pattern.IsEmpty()) {
      yield break;
    }
    if (pattern.Count() > source.Count()) {
      yield break;
    }
    #endregion === Validate parameters ===

    bool Found = false;
    int i = pattern.Count();
    IEnumerable<T> TempSource = source.TakeLast(i);
    while (!Found && i <= source.Count()) {
      if (TempSource.StartsWith(pattern)) {
        Found = true;
      } else {
        TempSource = source.TakeLast(++i);
      }
    }

    if (Found) {
      foreach (T Item in source.Take(source.Count() - i)) {
        yield return Item;
      }
    }

  }

  /// <summary>
  /// Indicate if a pattern is contained in an IEnumerable
  /// </summary>
  /// <typeparam name="T">The type of items</typeparam>
  /// <param name="source">The IEnumerable that may contain the pattern</param>
  /// <param name="pattern">The pattern searched for</param>
  /// <returns><see langword="true"/> if the pattern is contained, <see langword="false"/> otherwise</returns>
  public static bool Contains<T>(this IEnumerable<T> source, IEnumerable<T> pattern) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source is null && pattern is null) {
      return true;
    }

    if (source is null || source.IsEmpty() || pattern is null || pattern.IsEmpty()) {
      return false;
    }

    if (pattern.Count() > source.Count()) {
      return false;
    }
    #endregion === Validate parameters ===

    bool Found = false;
    IEnumerable<T> TempSource = source;
    int i = 1;
    while (!Found && i < (source.Count() - pattern.Count())) {
      if (TempSource.StartsWith(pattern)) {
        Found = true;
      } else {
        TempSource = source.Skip(i++);
      }
    }

    return Found;
  }
  #endregion --- IEnumerable<T> as pattern within IEnumerable<T> -----------------------------------------

  #region --- Item T within IEnumerable<T> --------------------------------------------
  /// <summary>
  /// Get the part of the IEnumerable after the first occurence of a searched item
  /// </summary>
  /// <typeparam name="T">The type of item</typeparam>
  /// <param name="source">The source IEnumerable</param>
  /// <param name="item">The item searched for</param>
  /// <returns>Enumerate the IEnumerable atfer the item</returns>
  public static IEnumerable<T> After<T>(this IEnumerable<T> source, T item) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source is null || item is null) {
      yield break;
    }

    if (source.IsEmpty()) {
      yield break;
    }
    #endregion === Validate parameters ===

    int Pos = -1;
    int i = 0;
    while (Pos == -1 && i < source.Count()) {
      if (source.ElementAt(i).Equals(item)) {
        Pos = i;
      } else {
        i++;
      }
    }

    if (Pos != -1) {
      foreach (T SourceItem in source.Skip(Pos + 1)) {
        yield return SourceItem;
      }
    }
  }

  /// <summary>
  /// Get the part of the IEnumerable after the last occurence of a searched item
  /// </summary>
  /// <typeparam name="T">The type of item</typeparam>
  /// <param name="source">The source IEnumerable</param>
  /// <param name="item">The item searched for</param>
  /// <returns>Enumerate the IEnumerable atfer the last occurence of the item</returns>
  public static IEnumerable<T> AfterLast<T>(this IEnumerable<T> source, T item) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source is null || item is null) {
      yield break;
    }

    if (source.IsEmpty()) {
      yield break;
    }
    #endregion === Validate parameters ===

    int Pos = -1;
    int i = source.Count() - 1;
    while (Pos == -1 && i >= 0) {
      if (source.ElementAt(i).Equals(item)) {
        Pos = i;
      } else {
        i--;
      }
    }

    if (Pos != -1) {
      foreach (T SourceItem in source.Skip(Pos + 1)) {
        yield return SourceItem;
      }
    }

  }

  /// <summary>
  /// Get the part of the IEnumerable before the first occurence of a searched item
  /// </summary>
  /// <typeparam name="T">The type of item</typeparam>
  /// <param name="source">The source IEnumerable</param>
  /// <param name="item">The item searched for</param>
  /// <returns>Enumerate the IEnumerable before the item</returns>
  public static IEnumerable<T> Before<T>(this IEnumerable<T> source, T item) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source is null || item is null) {
      yield break;
    }

    if (source.IsEmpty()) {
      yield break;
    }
    #endregion === Validate parameters ===

    int Pos = -1;
    int i = 0;
    while (Pos == -1 && i < source.Count()) {
      if (source.ElementAt(i).Equals(item)) {
        Pos = i;
      } else {
        i++;
      }
    }

    if (Pos != -1) {
      foreach (T SourceItem in source.Take(Pos)) {
        yield return SourceItem;
      }
    }

  }

  /// <summary>
  /// Get the part of the IEnumerable before the last occurence of a searched item
  /// </summary>
  /// <typeparam name="T">The type of item</typeparam>
  /// <param name="source">The source IEnumerable</param>
  /// <param name="item">The item searched for</param>
  /// <returns>Enumerate the IEnumerable before the last occurence of the item</returns>
  public static IEnumerable<T> BeforeLast<T>(this IEnumerable<T> source, T item) where T : IEquatable<T> {
    #region === Validate parameters ===
    if (source is null || item is null) {
      yield break;
    }

    if (source.IsEmpty()) {
      yield break;
    }
    #endregion === Validate parameters ===

    int Pos = -1;
    int i = source.Count() - 1;
    while (Pos == -1 && i >= 0) {
      if (source.ElementAt(i).Equals(item)) {
        Pos = i;
      } else {
        i--;
      }
    }

    if (Pos != -1) {
      foreach (T SourceItem in source.Take(Pos)) {
        yield return SourceItem;
      }
    }

  }
  #endregion --- Item T within IEnumerable<T> -----------------------------------------

  /// <summary>
  /// Indicate if an IEnumerable is empty
  /// </summary>
  /// <typeparam name="T">The type of item</typeparam>
  /// <param name="source">The IEnumerable being extended</param>
  /// <returns>True if the IEnumerable is empty, False otherwise</returns>
  public static bool IsEmpty<T>(this IEnumerable<T> source) {
    #region === Validate parameters ===
    if (source is null) {
      return true;
    }
    #endregion === Validate parameters ===
    return !source.Any();
  }

  /// <summary>
  /// Indicate if an IEnumerable is empty
  /// </summary>
  /// <typeparam name="T">The type of item</typeparam>
  /// <param name="source">The IEnumerable being extended</param>
  /// <returns>True if the IEnumerable is empty, False otherwise</returns>
  public static bool IsEmpty<T>(this Span<T> source) {
    return source.Length == 0;
  }

}
