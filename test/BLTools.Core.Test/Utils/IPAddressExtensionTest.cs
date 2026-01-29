using System.Net;

namespace BLTools.Test.Extensions.IPAddressEx;

/// <summary>
///This is a test class for IPAddressExtensionTest and is intended
///to contain all IPAddressExtensionTest Unit Tests
///</summary>
[TestClass()]
public class IPAddressExtensionTest {

  /// <summary>
  ///A test for GetSubnet
  ///</summary>
  [TestMethod(), TestCategory("Network")]
  public void GetSubnet_NetmaskAsIPAddress_ResultOk() {
    Message("Instanciate IP address");
    IPAddress ipAddress = new IPAddress(new byte[] { 10, 100, 200, 28 });
    Dump(ipAddress);

    Message("Instanciate netmask");
    IPAddress netmask = new IPAddress(new byte[] { 255, 255, 255, 0 });
    Dump(netmask);

    Message("Expected result");
    IPAddress expected = new IPAddress(new byte[] { 10, 100, 200, 0 });
    Dump(expected);

    Message("Calculate the subnet");
    IPAddress actual = ipAddress.GetSubnet(netmask);
    Dump(actual);

    Assert.AreEqual(expected, actual);
    Ok();
  }

  /// <summary>
  ///A test for GetSubnet
  ///</summary>
  [TestMethod(), TestCategory("Network")]
  public void GetSubnet_NetmaskAsByteArray_ResultOk() {
    IPAddress ipAddress = new IPAddress(new byte[] { 10, 100, 200, 28 });
    Dump(ipAddress);
    byte[] netmask = new byte[] { 255, 255, 255, 0 };
    Dump(netmask);
    IPAddress expected = new IPAddress(new byte[] { 10, 100, 200, 0 });
    Dump(expected);
    IPAddress actual = ipAddress.GetSubnet(netmask);
    Dump(actual);
    Assert.AreEqual(expected, actual);
    Ok();
  }
}
