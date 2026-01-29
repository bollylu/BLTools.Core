using static BLTools.Core.ProcessExtension;

namespace BLTools.Test.Helper.ProcessEx;

public class ProcessExtensionsTest {

  [Test]
  public async Task ExecuteProcessAsync_ReadOutput() {
    Message("Starting a sub process (dotnet --version)");
    string VersionNumber = await ExecuteProcessAsync("dotnet", "--version").ConfigureAwait(false);
    Message(VersionNumber.TrimEnd());
    Ok();
  }
}
