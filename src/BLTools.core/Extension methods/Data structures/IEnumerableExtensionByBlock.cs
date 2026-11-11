namespace BLTools.Core;

/// <summary>
/// Extensions for IEnumerable
/// </summary>
public static partial class IEnumerableExtension {
  extension(IEnumerable<float>? source) {
    /// <summary>
    /// Calculate a new IEnumerable by summing current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {9,14}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<float> SumByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Sum();
        yield break;
      }

      int Counter = 0;
      float Accumulator = 0;
      foreach (float Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator;
          Accumulator = 0f;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator;
      }
    }
  }
  extension(IEnumerable<double>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by summing current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {9,14}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<double> SumByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Sum();
        yield break;
      }

      int Counter = 0;
      double Accumulator = 0;
      foreach (double Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator;
          Accumulator = 0d;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator;
      }
    }
  }
  extension(IEnumerable<int>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by summing current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {9,14}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<int> SumByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Sum();
        yield break;
      }

      int Counter = 0;
      int Accumulator = 0;
      foreach (int Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator;
          Accumulator = 0;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator;
      }
    }
  }
  extension(IEnumerable<long>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by summing current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {9,14}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<long> SumByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Sum();
        yield break;
      }

      int Counter = 0;
      long Accumulator = 0;
      foreach (long Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator;
          Accumulator = 0L;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator;
      }
    }
  }
  extension(IEnumerable<decimal>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by summing current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {9,14}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<decimal> SumByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Sum();
        yield break;
      }

      int Counter = 0;
      decimal Accumulator = 0;
      foreach (decimal Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator;
          Accumulator = 0L;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator;
      }
    }
  }
  extension(IEnumerable<float>? source) {

    /****************************************************************************************************/

    /// <summary>
    /// Calculate a new IEnumerable by averaging current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {4.5,7}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<float> AverageByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Average();
        yield break;
      }

      int Counter = 0;
      float Accumulator = 0;
      foreach (float Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator / (float)blockSize;
          Accumulator = 0f;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator / (float)Counter;
      }
    }
  }
  extension(IEnumerable<double>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by averaging current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {4.5,7}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<double> AverageByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Average();
        yield break;
      }

      int Counter = 0;
      double Accumulator = 0;
      foreach (double Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator / (double)blockSize;
          Accumulator = 0d;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator / (double)Counter;
      }
    }
  }
  extension(IEnumerable<int>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by averaging current IEnumerable through blocks, results are rounded
    /// Eg. {3,6,5,9} with a block factor of 2 produces {5,7}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<int> AverageByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return (int)Math.Round(source.Average(), 0, MidpointRounding.AwayFromZero);
        yield break;
      }

      int Counter = 0;
      int Accumulator = 0;
      foreach (int Item in source) {
        if (Counter == blockSize) {
          yield return (int)Math.Round(Accumulator / (double)blockSize, 0, MidpointRounding.AwayFromZero);
          Accumulator = 0;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return (int)Math.Round(Accumulator / (double)Counter, 0, MidpointRounding.AwayFromZero);
      }
    }
  }
  extension(IEnumerable<long>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by averaging current IEnumerable through blocks, results are rounded
    /// Eg. {3,6,5,9} with a block factor of 2 produces {5,7}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<long> AverageByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return (long)Math.Round(source.Average(), 0, MidpointRounding.AwayFromZero);
        yield break;
      }

      int Counter = 0;
      long Accumulator = 0;
      foreach (long Item in source) {
        if (Counter == blockSize) {
          yield return (long)Math.Round(Accumulator / (double)blockSize, 0, MidpointRounding.AwayFromZero);
          Accumulator = 0L;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return (long)Math.Round(Accumulator / (double)Counter, 0, MidpointRounding.AwayFromZero);
      }
    }
  }
  extension(IEnumerable<decimal>? source) {

    /// <summary>
    /// Calculate a new IEnumerable by averaging current IEnumerable through blocks
    /// Eg. {3,6,5,9} with a block factor of 2 produces {4.5,7}
    /// </summary>
    /// <param name="source">The enumeration to sum</param>
    /// <param name="blockSize">The block factor to group data</param>
    /// <returns>A new enumeration where contents are summed by block of size in parameter</returns>
    public IEnumerable<decimal> AverageByBlock(int blockSize) {
      if (source == null) {
        yield break;
      }

      if (source.IsEmpty()) {
        yield break;
      }

      if (source.Count() <= blockSize) {
        yield return source.Average();
        yield break;
      }

      int Counter = 0;
      decimal Accumulator = 0;
      foreach (decimal Item in source) {
        if (Counter == blockSize) {
          yield return Accumulator / (decimal)blockSize;
          Accumulator = 0;
          Counter = 0;

        }
        Accumulator += Item;
        Counter++;
      }

      if (Counter > 0 && Counter <= blockSize) {
        yield return Accumulator / (decimal)blockSize;
      }
    }
  }
}
