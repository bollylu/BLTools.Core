namespace BLTools.Core.Collections;

/// <summary>
/// Implement a queue with a fixed size. If limit is reached, oldest item is lost
/// </summary>
/// <typeparam name="T">The type of queue items</typeparam>
public class TFixedSizeQueue<T> {

  private readonly int _Size;
  private readonly Queue<T> _Items;
  private readonly object _Lock = new object();

  #region --- Constructor(s) ---------------------------------------------------------------------------------
  /// <summary>
  /// A new fixed size queue
  /// </summary>
  /// <param name="size">The size of the queue</param>
  public TFixedSizeQueue(int size) {
    _Size = size;
    _Items = new Queue<T>(_Size);
  }
  #endregion --- Constructor(s) ------------------------------------------------------------------------------

  /// <summary>
  /// Add a new item to the queue. If limit is reached, oldest item is lost
  /// </summary>
  /// <param name="item">The item to add</param>
  public void Enqueue(T item) {
    lock (_Lock) {
      if (_Items.Count == _Size) {
        _Items.Dequeue();
      }
      _Items.Enqueue(item);
    }
  }

  /// <summary>
  /// Retrieve an item from the queue
  /// </summary>
  /// <returns>An item</returns>
  /// <exception cref="ApplicationException"></exception>
  public T Dequeue() {
    lock (_Lock) {
      if (_Items.Any()) {
        return _Items.Dequeue();
      }
    }
    throw new ApplicationException("Unable to dequeue items : queue is empty");
  }

  /// <summary>
  /// Clear the queue content
  /// </summary>
  public void Clear() {
    lock (_Lock) {
      _Items.Clear();
    }
  }

  /// <summary>
  /// Transform the queue to an array
  /// </summary>
  /// <returns>The array</returns>
  public T[] ToArray() {
    lock (_Lock) {
      return _Items.ToArray();
    }
  }

  /// <summary>
  /// Compare the queue to an array
  /// </summary>
  /// <param name="other">The array to compare with</param>
  /// <returns><see langword="true"/> if all content are the same, <see langword="false"/> otherwise</returns>
  public bool SequenceEqual(T[] other) {
    return _Items.ToArray().SequenceEqual(other);
  }
}
