using GalaxyDelivery.Controllers;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

[TestFixture]
public class DeliveryEventControllerTests
{
    [Test]
    public async Task GetDeliveryEvent_WhenMissing_Returns404()
    {
        var service = new Mock<IDeliveryEventService>();
        service.Setup(s => s.GetDeliveryEventAsync(It.IsAny<int>())).ReturnsAsync((DeliveryEvent)null);

        var sut = new DeliveryEventController(service.Object);

        var result = await sut.GetDeliveryEvent(10);

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task PostDeliveryEvent_WhenTimestampNull_DefaultsUtcNow()
    {
        var service = new Mock<IDeliveryEventService>();
        service.Setup(s => s.CreateDeliveryEventAsync(It.IsAny<DeliveryEvent>()))
            .ReturnsAsync((DeliveryEvent e) => { e.DeliveryEventId = 1; return e; });

        var sut = new DeliveryEventController(service.Object);

        var result = await sut.PostDeliveryEvent(new CreateDeliveryEventRequestDto("Desc", null, 1, null, 1));

        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result.Result;
        Assert.That(created.StatusCode, Is.EqualTo(201));
        var dto = (DeliveryEventDto)created.Value;
        Assert.That(dto.Timestamp, Is.Not.EqualTo(default(DateTime)));
    }
}
