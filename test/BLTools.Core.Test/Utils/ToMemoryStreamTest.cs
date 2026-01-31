using BLTools.Core.Test.Extensions.IPAddressEx;

namespace BLTools.Core.Test.Extensions.MemoryStreamEx;

public class MemoryStreamTest {

  private static ILogger Logger => new TConsoleLogger<IPAddressExtensionTest>(TLoggerOptions.MessageOnly);

  #region --- String to stream --------------------------------------------
  [Test]
  public void ToStream_EmptyString_TargetStream() {
    string Source = "";
    Logger.Dump(Source);
    MemoryStream? Target = Source.ToStream();
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public void ToStream_EmptyStringWithEncoding_TargetStream() {
    string Source = "";
    Logger.Dump(Source);
    MemoryStream? Target = Source.ToStream(Encoding.UTF8);
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public void ToStream_ValidString_TargetStreamOk() {
    string Source = "123ABCéèà";
    MemoryStream? TempStream = Source.ToStream();
    Assert.That(TempStream, Is.Not.Null);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = Reader.ReadToEnd();
      Logger.Dump(Target);
      Assert.That(Target, Is.EqualTo(Source));
    }
    Logger.Ok();
  }

  [Test]
  public void ToStream_ValidStringWithEncoding_TargetStreamOk() {
    string Source = "123ABCéèà";
    Logger.Dump(Source);
    MemoryStream? TempStream = Source.ToStream(Encoding.UTF8);
    Assert.That(TempStream, Is.Not.Null);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = Reader.ReadToEnd();
      Logger.Dump(Target);
      Assert.That(Target, Is.EqualTo(Source));
    }
    Logger.Ok();
  }

  [Test]
  public void ToStream_ValidStringWithWrongEncoding_TargetStreamNotOk() {
    string Source = "123ABCéèà";
    Logger.Dump(Source);
    MemoryStream? TempStream = Source.ToStream(Encoding.ASCII);
    Assert.That(TempStream, Is.Not.Null);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = Reader.ReadToEnd();
      Logger.Dump(Target);
      Assert.That(Target, Is.Not.EqualTo(Source));
    }
    Logger.Ok();
  }


  [Test]
  public async Task ToStreamAsync_EmptyString_TargetStream() {
    string Source = "";
    Logger.Dump(Source);
    MemoryStream? Target = await Source.ToStreamAsync();
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_EmptyStringWithEncoding_TargetStream() {
    string Source = "";
    Logger.Dump(Source);
    MemoryStream? Target = await Source.ToStreamAsync(Encoding.UTF8);
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0)  );
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_ValidString_TargetStreamOk() {
    string Source = "123ABCéèà";
    Logger.Dump(Source);
    MemoryStream? TempStream = await Source.ToStreamAsync();
    Assert.That(TempStream, Is.Not.Null);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = await Reader.ReadToEndAsync();
      Logger.Dump(Target);
      Assert.That(Target, Is.EqualTo(Source));
    }
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_ValidStringWithEncoding_TargetStreamOk() {
    string Source = "123ABCéèà";
    Logger.Dump(Source);
    MemoryStream? TempStream = await Source.ToStreamAsync(Encoding.UTF8);
    Assert.That(TempStream, Is.Not.Null);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = await Reader.ReadToEndAsync();
      Logger.Dump(Target);
      Assert.That(Target, Is.EqualTo(Source) );
    }
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_ValidStringWithWrongEncoding_TargetStreamNotOk() {
    string Source = "123ABCéèà";
    Logger.Dump(Source);
    MemoryStream? TempStream = await Source.ToStreamAsync(Encoding.ASCII);
    Assert.That(TempStream, Is.Not.Null);
    using (TextReader Reader = new StreamReader(TempStream, Encoding.UTF8)) {
      string Target = await Reader.ReadToEndAsync();
      Logger.Dump(Target);
      Assert.That(Target, Is.Not.EqualTo(Source));
    }
    Logger.Ok();
  }

  #endregion --- String to stream --------------------------------------------

  #region --- Bytes to stream --------------------------------------------
  [Test]
  public void ToStream_EmptyByteArray_TargetStreamOkButEmpty() {
    byte[] Source = [];
    Logger.Dump(Source);
    MemoryStream? Target = Source.ToStream();
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public void ToStream_EmptyListOfBytes_TargetStreamOkButEmpty() {
    List<byte> Source = [];
    Logger.Dump(Source);
    MemoryStream? Target = Source.ToStream();
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public void ToStream_ValidByteArray_TargetStreamOk() {
    byte[] Source = [0x64, 0x65, 0x66];
    Logger.Dump(Source);
    MemoryStream? Target = Source.ToStream();
    Assert.That(Target, Is.Not.Null);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.That(Reader.BaseStream.Length, Is.EqualTo(Source.Length));
      byte[] CheckData = Reader.ReadBytes(Source.Length);
      Logger.Dump(CheckData);
      Assert.That(CheckData.Length, Is.EqualTo(Source.Length));
      Assert.That(CheckData.ToHexString(), Is.EqualTo(Source.ToHexString()));
    }
    Logger.Ok();
  }

  [Test]
  public void ToStream_ValidListOfBytes_TargetStreamOk() {
    List<byte> Source = [0x64, 0x65, 0x66];
    Logger.Dump(Source);
    MemoryStream? Target = Source.ToStream();
    Assert.That(Target, Is.Not.Null);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.That(Reader.BaseStream.Length, Is.EqualTo(Source.Count));
      byte[] CheckData = Reader.ReadBytes(Source.Count);
      Logger.Dump(CheckData);
      Assert.That(CheckData.Length, Is.EqualTo(Source.Count));
      Assert.That(CheckData.ToHexString(), Is.EqualTo(Source.ToArray().ToHexString()));
    }
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_EmptyByteArray_TargetStreamOkButEmpty() {
    byte[] Source = [];
    Logger.Dump(Source);
    MemoryStream? Target = await Source.ToStreamAsync();
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_EmptyListOfBytes_TargetStreamOkButEmpty() {
    List<byte> Source = [];
    Logger.Dump(Source);
    MemoryStream? Target = await Source.ToStreamAsync();
    Assert.That(Target, Is.Not.Null);
    Logger.Message("Target length is 0");
    Assert.That(Target.Length, Is.EqualTo(0));
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_ValidByteArray_TargetStreamOk() {
    byte[] Source = [0x64, 0x65, 0x66];
    Logger.Dump(Source);
    MemoryStream? Target = await Source.ToStreamAsync();
    Assert.That(Target, Is.Not.Null);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.That(Reader.BaseStream.Length, Is.EqualTo(Source.Length));
      byte[] CheckData = Reader.ReadBytes(Source.Length);
      Logger.Dump(CheckData);
      Assert.That(CheckData.Length, Is.EqualTo(Source.Length));
      Assert.That(CheckData.ToHexString(), Is.EqualTo(Source.ToHexString()));
    }
    Logger.Ok();
  }

  [Test]
  public async Task ToStreamAsync_ValidListOfBytes_TargetStreamOk() {
    List<byte> Source = [0x64, 0x65, 0x66];
    Logger.Dump(Source);
    MemoryStream? Target = await Source.ToStreamAsync();
    Assert.That(Target, Is.Not.Null);
    using (BinaryReader Reader = new BinaryReader(Target)) {
      Assert.That(Reader.BaseStream.Length, Is.EqualTo(Source.Count));
      byte[] CheckData = Reader.ReadBytes(Source.Count);
      Logger.Dump(CheckData);
      Assert.That(CheckData.Length, Is.EqualTo(Source.Count));
      Assert.That(CheckData.ToHexString(), Is.EqualTo(Source.ToArray().ToHexString()));
    }
    Logger.Ok();
  }
  #endregion --- Bytes to stream --------------------------------------------
}
