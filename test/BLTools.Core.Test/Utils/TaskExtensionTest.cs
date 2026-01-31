using System.Net.Sockets;

namespace BLTools.Core.Test.Extensions.TaskEx {

  public class TaskExtensionTest {

    [Test]
    public async Task TaskWithCancellation_TransformIntoTimeoutTask_TaskIsCancelled() {

      using (UdpClient Client = new UdpClient(Random.Shared.Next(1024, 2048))) {
        Assert.ThrowsAsync(typeof(OperationCanceledException), async () => {
          var Result = await Client.ReceiveAsync().WithTimeout(100).ConfigureAwait(false);
        });
      }
    }

    [Test]
    public async Task TaskWithCancellation_TransformIntoCancellableTask_TaskIsCancelled() {

      Assert.ThrowsAsync(typeof(OperationCanceledException), async () => {
        CancellationTokenSource CTS = new CancellationTokenSource();
        CTS.Cancel();
        using (UdpClient Client = new UdpClient(Random.Shared.Next(1024, 2048))) {
          var Result = await Client.ReceiveAsync().WithCancellation(CTS.Token);
        }
      });
      
    }
  }
}
