using GalaxyDelivery.Controllers;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

[TestFixture]
public class DriverControllerTests
{
    [Test]
    public async Task GetDriver_WhenMissing_Returns404()
    {
        var service = new Mock<IDriverService>();
        service.Setup(s => s.GetDriverAsync(It.IsAny<int>())).ReturnsAsync((Driver)null);

        var sut = new DriverController(service.Object);

        var result = await sut.GetDriver(1);

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task PostDriver_WhenValid_Returns201()
    {
        var service = new Mock<IDriverService>();
        service.Setup(s => s.CreateDriverAsync(It.IsAny<Driver>())).ReturnsAsync((Driver d) => { d.DriverId = 5; return d; });

        var sut = new DriverController(service.Object);

        var result = await sut.PostDriver(new CreateDriverRequestDto("X"));

        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result.Result;
        Assert.That(((DriverDto)created.Value).DriverName, Is.EqualTo("X"));
    }
}
