using System.Net;

namespace BLTools.Core.Test.Extensions.IPAddressEx;

/// <summary>
///This is a test class for IPAddressExtensionTest and is intended
///to contain all IPAddressExtensionTest Unit Tests
///</summary>
public class IPAddressExtensionTest {

  private static ILogger Logger => new TConsoleLogger<IPAddressExtensionTest>(TLoggerOptions.MessageOnly);

  /// <summary>
  ///A test for GetSubnet
  ///</summary>
  [Test]
  public void GetSubnet_NetmaskAsIPAddress_ResultOk() {
    Logger.Message("Instanciate IP address");
    IPAddress ipAddress = new IPAddress([10, 100, 200, 28]);
    Logger.Dump(ipAddress);

    Logger.Message("Instanciate netmask");
    IPAddress netmask = new IPAddress([255, 255, 255, 0]);
    Logger.Dump(netmask);

    Logger.Message("Expected result");
    IPAddress expected = new IPAddress([10, 100, 200, 0]);
    Logger.Dump(expected);

    Logger.Message("Calculate the subnet");
    IPAddress actual = ipAddress.GetSubnet(netmask);
    Logger.Dump(actual);

    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }

  /// <summary>
  ///A test for GetSubnet
  ///</summary>
  [Test]
  public void GetSubnet_NetmaskAsByteArray_ResultOk() {
    IPAddress ipAddress = new IPAddress([10, 100, 200, 28]);
    Logger.Dump(ipAddress);
    byte[] netmask = new byte[] { 255, 255, 255, 0 };
    Logger.Dump(netmask);
    IPAddress expected = new IPAddress([10, 100, 200, 0]);
    Logger.Dump(expected);
    IPAddress actual = ipAddress.GetSubnet(netmask);
    Logger.Dump(actual);
    Assert.That(actual, Is.EqualTo(expected));
    Logger.Ok();
  }
}
