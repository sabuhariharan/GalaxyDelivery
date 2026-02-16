using GalaxyDelivery.Controllers;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

[TestFixture]
public class DeliveryControllerTests
{
    [Test]
    public async Task GetDelivery_WhenMissing_Returns404()
    {
        var service = new Mock<IDeliveryService>();
        service.Setup(s => s.GetDeliveryAsync(It.IsAny<int>())).ReturnsAsync((Delivery)null);

        var sut = new DeliveryController(service.Object);

        var result = await sut.GetDelivery(1);

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task PostDelivery_WhenValid_Returns201()
    {
        var service = new Mock<IDeliveryService>();
        service.Setup(s => s.CreateDeliveryAsync(It.IsAny<Delivery>()))
            .ReturnsAsync((Delivery d) => { d.DeliveryId = 99; return d; });

        service.Setup(s => s.GetDeliveryAsync(99)).ReturnsAsync(new Delivery
        {
            DeliveryId = 99,
            Origin = "O",
            Destination = "D",
            Driver = new Driver { DriverName = "Driver" },
            Vehicle = new Vehicle { VehicleMake = "Vehicle" },
            RouteId = 1,
            DeliveryEvent = new List<DeliveryEvent>()
        });

        var sut = new DeliveryController(service.Object);

        var result = await sut.PostDelivery(new CreateDeliveryRequestDto("O", "D", 1, 1, 1));

        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result.Result;
        Assert.That(created.StatusCode, Is.EqualTo(201));
        Assert.That(created.Value, Is.TypeOf<DeliveryDetailDto>());
    }
}
