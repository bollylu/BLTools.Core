namespace BLTools.Test.Extensions.MemoryStreamEx;

[TestClass]
public class MemoryStreamTest {

  #region --- String to stream --------------------------------------------
  [TestMethod(), TestCategory("String to MemoryStream")]
  public void ToStream_EmptyString_TargetStream() {
    string Source = "";
    Dump(Source);
    MemoryStream Target = Source.ToStream();
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public void ToStream_EmptyStringWithEncoding_TargetStream() {
    string Source = "";
    Dump(Source);
    MemoryStream Target = Source.ToStream(Encoding.UTF8);
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public void ToStream_ValidString_TargetStreamOk() {
    string Source = "123ABCéèà";
    MemoryStream TempStream = Source.ToStream();
    Assert.IsNotNull(TempStream);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = Reader.ReadToEnd();
      Dump(Target);
      Assert.AreEqual(Source, Target);
    }
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public void ToStream_ValidStringWithEncoding_TargetStreamOk() {
    string Source = "123ABCéèà";
    Dump(Source);
    MemoryStream TempStream = Source.ToStream(Encoding.UTF8);
    Assert.IsNotNull(TempStream);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = Reader.ReadToEnd();
      Dump(Target);
      Assert.AreEqual(Source, Target);
    }
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public void ToStream_ValidStringWithWrongEncoding_TargetStreamNotOk() {
    string Source = "123ABCéèà";
    Dump(Source);
    MemoryStream TempStream = Source.ToStream(Encoding.ASCII);
    Assert.IsNotNull(TempStream);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = Reader.ReadToEnd();
      Dump(Target);
      Assert.AreNotEqual(Source, Target);
    }
    Ok();
  }


  [TestMethod(), TestCategory("String to MemoryStream")]
  public async Task ToStreamAsync_EmptyString_TargetStream() {
    string Source = "";
    Dump(Source);
    MemoryStream Target = await Source.ToStreamAsync();
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public async Task ToStreamAsync_EmptyStringWithEncoding_TargetStream() {
    string Source = "";
    Dump(Source);
    MemoryStream Target = await Source.ToStreamAsync(Encoding.UTF8);
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public async Task ToStreamAsync_ValidString_TargetStreamOk() {
    string Source = "123ABCéèà";
    Dump(Source);
    MemoryStream TempStream = await Source.ToStreamAsync();
    Assert.IsNotNull(TempStream);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = await Reader.ReadToEndAsync();
      Dump(Target);
      Assert.AreEqual(Source, Target);
    }
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public async Task ToStreamAsync_ValidStringWithEncoding_TargetStreamOk() {
    string Source = "123ABCéèà";
    Dump(Source);
    MemoryStream TempStream = await Source.ToStreamAsync(Encoding.UTF8);
    Assert.IsNotNull(TempStream);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = await Reader.ReadToEndAsync();
      Dump(Target);
      Assert.AreEqual(Source, Target);
    }
    Ok();
  }

  [TestMethod(), TestCategory("String to MemoryStream")]
  public async Task ToStreamAsync_ValidStringWithWrongEncoding_TargetStreamNotOk() {
    string Source = "123ABCéèà";
    Dump(Source);
    MemoryStream TempStream = await Source.ToStreamAsync(Encoding.ASCII);
    Assert.IsNotNull(TempStream);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = await Reader.ReadToEndAsync();
      Dump(Target);
      Assert.AreNotEqual(Source, Target);
    }
    Ok();
  }

  #endregion --- String to stream --------------------------------------------

  #region --- Bytes to stream --------------------------------------------
  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public void ToStream_EmptyByteArray_TargetStreamOkButEmpty() {
    byte[] Source = Array.Empty<byte>();
    Dump(Source);
    MemoryStream Target = Source.ToStream();
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public void ToStream_EmptyListOfBytes_TargetStreamOkButEmpty() {
    List<byte> Source = new();
    Dump(Source);
    MemoryStream Target = Source.ToStream();
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public void ToStream_ValidByteArray_TargetStreamOk() {
    byte[] Source = new byte[] { 0x64, 0x65, 0x66 };
    Dump(Source);
    MemoryStream Target = Source.ToStream();
    Assert.IsNotNull(Target);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.AreEqual(Reader.BaseStream.Length, Source.Length);
      byte[] CheckData = Reader.ReadBytes(Source.Length);
      Dump(CheckData);
      Assert.AreEqual(CheckData.Length, Source.Length);
      Assert.AreEqual(CheckData.ToHexString(), Source.ToHexString());
    }
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public void ToStream_ValidListOfBytes_TargetStreamOk() {
    List<byte> Source = new List<byte>() { 0x64, 0x65, 0x66 };
    Dump(Source);
    MemoryStream Target = Source.ToStream();
    Assert.IsNotNull(Target);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.AreEqual(Reader.BaseStream.Length, Source.Count);
      byte[] CheckData = Reader.ReadBytes(Source.Count);
      Dump(CheckData);
      Assert.AreEqual(CheckData.Length, Source.Count);
      Assert.AreEqual(CheckData.ToHexString(), Source.ToArray().ToHexString());
    }
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public async Task ToStreamAsync_EmptyByteArray_TargetStreamOkButEmpty() {
    byte[] Source = Array.Empty<byte>();
    Dump(Source);
    MemoryStream Target = await Source.ToStreamAsync();
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public async Task ToStreamAsync_EmptyListOfBytes_TargetStreamOkButEmpty() {
    List<byte> Source = new();
    Dump(Source);
    MemoryStream Target = await Source.ToStreamAsync();
    Assert.IsNotNull(Target);
    Message("Target length is 0");
    Assert.AreEqual(Target.Length, 0);
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public async Task ToStreamAsync_ValidByteArray_TargetStreamOk() {
    byte[] Source = new byte[] { 0x64, 0x65, 0x66 };
    Dump(Source);
    MemoryStream Target = await Source.ToStreamAsync();
    Assert.IsNotNull(Target);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.AreEqual(Reader.BaseStream.Length, Source.Length);
      byte[] CheckData = Reader.ReadBytes(Source.Length);
      Dump(CheckData);
      Assert.AreEqual(CheckData.Length, Source.Length);
      Assert.AreEqual(CheckData.ToHexString(), Source.ToHexString());
    }
    Ok();
  }

  [TestMethod(), TestCategory("IEnumerable<byte> to MemoryStream")]
  public async Task ToStreamAsync_ValidListOfBytes_TargetStreamOk() {
    List<byte> Source = new List<byte>() { 0x64, 0x65, 0x66 };
    Dump(Source);
    MemoryStream Target = await Source.ToStreamAsync();
    Assert.IsNotNull(Target);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.AreEqual(Reader.BaseStream.Length, Source.Count);
      byte[] CheckData = Reader.ReadBytes(Source.Count);
      Dump(CheckData);
      Assert.AreEqual(CheckData.Length, Source.Count);
      Assert.AreEqual(CheckData.ToHexString(), Source.ToArray().ToHexString());
    }
    Ok();
  }
  #endregion --- Bytes to stream --------------------------------------------
}
