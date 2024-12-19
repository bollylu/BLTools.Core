namespace BLTools.Core.Collections;

/// <summary>
/// An implementation of a circular list
/// </summary>
/// <typeparam name="T">The type of values in the list</typeparam>
public class TCircularList<T> : List<T>, ICircularList<T> {

  /// <summary>
  /// Does it behave like circular list or just normal list ?
  /// </summary>
  public bool IsCircular { get; set; }

  private int CurrentIndex = -1;

  /// <summary>
  /// Get next item in the list. When in circular mode and reach the last item, get the first item
  /// </summary>
  /// <returns>An item or default value for type when nothing available</returns>
  public T? GetNext() {
    if (this.IsEmpty()) {
      return default(T);
    }
    CurrentIndex++;
    if (CurrentIndex >= this.Count && !IsCircular) {
      return default(T);
    }
    if (CurrentIndex >= this.Count) {
      CurrentIndex = 0;
    }
    return this[CurrentIndex];

  }

  /// <summary>
  /// Get previous item in list. When in circular mode nad reach the first item, get the last item
  /// </summary>
  /// <returns>An item or default value for type when nothing available</returns>
  public T? GetPrevious() {
    if (!this.Any()) {
      return default(T);
    }
    CurrentIndex--;
    if (CurrentIndex < 0 && !IsCircular) {
      return default(T);
    }
    if (CurrentIndex < 0) {
      CurrentIndex = this.Count - 1;
    }
    return this[CurrentIndex];
  }

  /// <summary>
  /// Reset the index to the first item
  /// </summary>
  public void ResetIndex() {
    CurrentIndex = -1;
  }

}
