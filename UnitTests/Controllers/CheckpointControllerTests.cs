using GalaxyDelivery.Controllers;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

[TestFixture]
public class CheckpointControllerTests
{
    [Test]
    public async Task GetCheckpoint_WhenMissing_Returns404()
    {
        var service = new Mock<ICheckpointService>();
        service.Setup(s => s.GetCheckpointAsync(It.IsAny<int>())).ReturnsAsync((Checkpoint)null);

        var sut = new CheckpointController(service.Object);

        var result = await sut.GetCheckpoint(1);

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task PostCheckpoint_WhenValid_Returns201()
    {
        var service = new Mock<ICheckpointService>();
        service.Setup(s => s.CreateCheckpointAsync(It.IsAny<Checkpoint>())).ReturnsAsync((Checkpoint c) => { c.CheckpointId = 3; return c; });
        service.Setup(s => s.GetCheckpointAsync(3)).ReturnsAsync(new Checkpoint { CheckpointId = 3, CheckpointName = "CP", RouteId = 1, Route = new DeliveryRoute { RouteId = 1, RouteName = "R1" } });

        var sut = new CheckpointController(service.Object);

        var result = await sut.PostCheckpoint(new CreateCheckpointRequestDto("CP", 1));

        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result.Result;
        Assert.That(((CheckpointDto)created.Value).CheckpointName, Is.EqualTo("CP"));
    }
}
