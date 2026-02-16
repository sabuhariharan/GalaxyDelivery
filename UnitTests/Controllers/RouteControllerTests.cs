using GalaxyDelivery.Controllers;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests.Controllers;

[TestFixture]
public class RouteControllerTests
{
    [Test]
    public async Task GetRoute_WhenMissing_Returns404()
    {
        var service = new Mock<IRouteService>();
        service.Setup(s => s.GetRouteAsync(It.IsAny<int>())).ReturnsAsync((DeliveryRoute)null);

        var sut = new RouteController(service.Object);

        var result = await sut.GetRoute(1);

        Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public async Task PostRoute_WhenValid_Returns201()
    {
        var service = new Mock<IRouteService>();
        service.Setup(s => s.CreateRouteAsync(It.IsAny<DeliveryRoute>())).ReturnsAsync((DeliveryRoute r) => { r.RouteId = 7; return r; });

        var sut = new RouteController(service.Object);

        var result = await sut.PostRoute(new CreateRouteRequestDto("R"));

        Assert.That(result.Result, Is.TypeOf<CreatedAtActionResult>());
        var created = (CreatedAtActionResult)result.Result;
        Assert.That(((DeliveryRouteDto)created.Value).RouteId, Is.EqualTo(7));
    }

    [Test]
    public async Task PutRoute_WhenValid_ReturnsRouteWithCheckpoints()
    {
        var service = new Mock<IRouteService>();
        service.Setup(s => s.GetRouteAsync(1)).ReturnsAsync(new DeliveryRoute
        {
            RouteId = 1,
            RouteName = "R1",
            Checkpoint = new List<Checkpoint> { new Checkpoint { CheckpointId = 10, CheckpointName = "CP", RouteId = 1 } }
        });
        service.Setup(s => s.UpdateRouteAsync(It.IsAny<DeliveryRoute>())).ReturnsAsync((DeliveryRoute r) => r);

        var sut = new RouteController(service.Object);

        var result = await sut.PutRoute(1, new UpdateRouteRequestDto("R1-Updated"));

        Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        var ok = (OkObjectResult)result.Result;
        var dto = (DeliveryRouteDto)ok.Value;
        Assert.That(dto.RouteId, Is.EqualTo(1));
        Assert.That(dto.Checkpoints, Is.Not.Empty);
    }
}
