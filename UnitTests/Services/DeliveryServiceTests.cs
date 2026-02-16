using GalaxyDelivery.Entities;
using GalaxyDelivery.Services;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestHelpers;

namespace UnitTests.Services;

[TestFixture]
public class DeliveryServiceTests
{
    [Test]
    public async Task GetDeliveryAsync_IncludesEventsStatusCheckpointDriverVehicle()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(GetDeliveryAsync_IncludesEventsStatusCheckpointDriverVehicle));

        db.Driver.Add(new Driver { DriverId = 1, DriverName = "D1" });
        db.Vehicle.Add(new Vehicle { VehicleId = 1, VehicleMake = "V1" });
        db.Route.Add(new DeliveryRoute { RouteId = 1, RouteName = "R1" });
        db.DeliveryStatus.Add(new DeliveryStatus { DeliveryStatusId = 1, StatusName = "Planned" });
        db.Checkpoint.Add(new Checkpoint { CheckpointId = 10, CheckpointName = "CP", RouteId = 1 });
        db.Delivery.Add(new Delivery { DeliveryId = 7, Origin = "O", Destination = "D", DriverId = 1, VehicleId = 1, RouteId = 1 });
        db.DeliveryEvent.Add(new DeliveryEvent
        {
            DeliveryEventId = 5,
            DeliveryEventDesc = "E",
            Timestamp = DateTime.UtcNow,
            DeliveryId = 7,
            StatusId = 1,
            CheckpointId = 10
        });
        await db.SaveChangesAsync();

        var sut = new DeliveryService(db);

        var delivery = await sut.GetDeliveryAsync(7);

        Assert.That(delivery, Is.Not.Null);
        Assert.That(delivery.Driver, Is.Not.Null);
        Assert.That(delivery.Vehicle, Is.Not.Null);
        Assert.That(delivery.DeliveryEvent, Is.Not.Empty);
        Assert.That(delivery.DeliveryEvent.First().Status, Is.Not.Null);
        Assert.That(delivery.DeliveryEvent.First().Checkpoint, Is.Not.Null);
    }
}
