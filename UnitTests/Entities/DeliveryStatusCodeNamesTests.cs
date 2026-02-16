using GalaxyDelivery.Entities;

namespace UnitTests.Entities;

[TestFixture]
public class DeliveryStatusCodeNamesTests
{
    [TestCase(DeliveryStatusCode.Planned, "Planned")]
    [TestCase(DeliveryStatusCode.Started, "Started")]
    [TestCase(DeliveryStatusCode.InProgress, "InProgress")]
    [TestCase(DeliveryStatusCode.Completed, "Completed")]
    [TestCase(DeliveryStatusCode.Failed, "Failed")]
    public void GetName_ReturnsExpected(DeliveryStatusCode code, string expected)
    {
        Assert.That(DeliveryStatusCodeNames.GetName(code), Is.EqualTo(expected));
    }
}
