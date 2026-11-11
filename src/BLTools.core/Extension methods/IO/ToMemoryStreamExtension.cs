namespace BLTools.Core;

/// <summary>
/// Extensions to convert to memory stream
/// </summary>
public static class ToMemoryStreamExtension {

  #region --- Gets MemoryStream from string --------------------------------------------
  extension(string source) {
    /// <summary>
    /// Gets a MemoryStream from a string (encoding UTF8)
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>The requested MemoryStream</returns>
    public MemoryStream ToStream() {
      #region === Validate parameters ===
      if (string.IsNullOrEmpty(source)) {
        return new MemoryStream();
      }
      #endregion === Validate parameters ===
      return source.ToStream(Encoding.UTF8);
    }



    /// <summary>
    /// Gets a MemoryStream from a string
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="encoding">The encoding for the stream creation</param>
    /// <returns>The requested MemoryStream</returns>
    public MemoryStream ToStream(Encoding encoding) {
      #region === Validate parameters ===
      if (string.IsNullOrEmpty(source)) {
        return new MemoryStream();
      }
      #endregion === Validate parameters ===

      return encoding.GetBytes(source).ToStream();
    }



    /// <summary>
    /// Gets a MemoryStream from a string asynchronously (encoding UTF8)
    /// </summary>
    /// <param name="source">The source string</param>
    /// <returns>The requested MemoryStream</returns>
    public Task<MemoryStream> ToStreamAsync() {
      #region === Validate parameters ===
      if (string.IsNullOrEmpty(source)) {
        return Task.FromResult(new MemoryStream());
      }
      #endregion === Validate parameters ===
      return source.ToStreamAsync(Encoding.UTF8);
    }



    /// <summary>
    /// Gets a MemoryStream from a string asynchronously
    /// </summary>
    /// <param name="source">The source string</param>
    /// <param name="encoding">The encoding for the stream creation</param>
    /// <returns>The requested MemoryStream</returns>
    public async Task<MemoryStream> ToStreamAsync(Encoding encoding) {
      #region === Validate parameters ===
      if (string.IsNullOrEmpty(source)) {
        return await Task.FromResult(new MemoryStream());
      }
      #endregion === Validate parameters ===

      return await encoding.GetBytes(source).ToStreamAsync();
    }
  }
  #endregion --- Gets MemoryStream from string --------------------------------------------

  #region --- Gets MemoryStream from IEnumerable<byte> --------------------------------------------
  extension(IEnumerable<byte> sourceBytes) {

    /// <summary>
    /// Gets a MemoryStream from a byte[]
    /// </summary>
    /// <param name="sourceBytes">The array of bytes</param>
    /// <returns>The requested MemoryStream</returns>
    public MemoryStream ToStream() {
      #region === Validate parameters ===
      if (sourceBytes == null || sourceBytes.IsEmpty()) {
        return new MemoryStream();
      }
      #endregion === Validate parameters ===
      MemoryStream RetVal;
      if (sourceBytes is byte[] ByteArray) {
        RetVal = new MemoryStream(ByteArray);
      } else {
        RetVal = new MemoryStream(sourceBytes.ToArray());
      }
      RetVal.Seek(0, SeekOrigin.Begin);
      return RetVal;
    }



    /// <summary>
    /// Gets a MemoryStream from a byte[] asynchronously
    /// </summary>
    /// <param name="sourceBytes">The array of byte</param>
    /// <returns>The requested MemoryStream</returns>
    public async Task<MemoryStream> ToStreamAsync() {
      #region === Validate parameters ===
      if (sourceBytes is null || sourceBytes.IsEmpty()) {
        return await Task.FromResult(new MemoryStream());
      }
      #endregion === Validate parameters ===
      MemoryStream RetVal = new MemoryStream();
      if (sourceBytes is byte[] ByteArray) {
        await RetVal.WriteAsync(ByteArray.AsMemory(0, ByteArray.Length));
      } else {
        await RetVal.WriteAsync(sourceBytes.ToArray().AsMemory(0, sourceBytes.Count()));
      }
      await RetVal.FlushAsync();
      RetVal.Seek(0, SeekOrigin.Begin);
      return RetVal;
    }
  }
  #endregion --- Gets MemoryStream from IEnumerable<byte> --------------------------------------------

}
