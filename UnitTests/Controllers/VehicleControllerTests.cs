using GalaxyDelivery.Controllers;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

[TestFixture]
public class VehicleControllerTests
{
    [Test]
    public async Task PutVehicle_WhenMissing_Returns404()
    {
        var service = new Mock<IVehicleService>();
        service.Setup(s => s.GetVehicleAsync(It.IsAny<int>())).ReturnsAsync((Vehicle)null);

        var sut = new VehicleController(service.Object);

        var result = await sut.PutVehicle(1, new UpdateVehicleRequestDto("M"));

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task PostVehicle_WhenValid_Returns201()
    {
        var service = new Mock<IVehicleService>();
        service.Setup(s => s.CreateVehicleAsync(It.IsAny<Vehicle>())).ReturnsAsync((Vehicle v) => { v.VehicleId = 2; return v; });

        var sut = new VehicleController(service.Object);

        var result = await sut.PostVehicle(new CreateVehicleRequestDto("Make"));

        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result.Result;
        Assert.That(((VehicleDto)created.Value).VehicleMake, Is.EqualTo("Make"));
    }
}
