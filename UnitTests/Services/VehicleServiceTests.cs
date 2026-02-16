using GalaxyDelivery.Entities;
using GalaxyDelivery.Services;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestHelpers;

namespace UnitTests.Services;

[TestFixture]
public class VehicleServiceTests
{
    [Test]
    public async Task CreateVehicleAsync_AddsVehicle()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(CreateVehicleAsync_AddsVehicle));
        var sut = new VehicleService(db);

        await sut.CreateVehicleAsync(new Vehicle { VehicleMake = "Make" });

        Assert.That(await db.Vehicle.CountAsync(), Is.EqualTo(1));
    }
}
