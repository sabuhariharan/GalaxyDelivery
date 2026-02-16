using GalaxyDelivery.Entities;
using GalaxyDelivery.Services;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestHelpers;

namespace UnitTests.Services;

[TestFixture]
public class DeliveryServiceDeleteRulesTests
{
    [Test]
    public async Task DeleteDeliveryAsync_WhenLastEventIsPlanned_AllowsDelete()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(DeleteDeliveryAsync_WhenLastEventIsPlanned_AllowsDelete));

        db.Delivery.Add(new Delivery { DeliveryId = 1, Origin = "O", Destination = "D", DriverId = 1, VehicleId = 1, RouteId = 1 });
        db.DeliveryEvent.Add(new DeliveryEvent
        {
            DeliveryEventId = 1,
            DeliveryId = 1,
            DeliveryEventDesc = "Planned",
            Timestamp = DateTime.UtcNow,
            StatusId = (int)DeliveryStatusCode.Planned,
            CheckpointId = null
        });
        await db.SaveChangesAsync();

        var sut = new DeliveryService(db);

        Assert.DoesNotThrowAsync(() => sut.DeleteDeliveryAsync(1));
        Assert.That(await db.Delivery.CountAsync(), Is.EqualTo(0));
    }

    [Test]
    public async Task DeleteDeliveryAsync_WhenLastEventBeyondPlanned_ThrowsInvalidOperation()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(DeleteDeliveryAsync_WhenLastEventBeyondPlanned_ThrowsInvalidOperation));

        db.Delivery.Add(new Delivery { DeliveryId = 1, Origin = "O", Destination = "D", DriverId = 1, VehicleId = 1, RouteId = 1 });
        db.DeliveryEvent.AddRange(
            new DeliveryEvent
            {
                DeliveryEventId = 1,
                DeliveryId = 1,
                DeliveryEventDesc = "Planned",
                Timestamp = DateTime.UtcNow,
                StatusId = (int)DeliveryStatusCode.Planned,
                CheckpointId = null
            },
            new DeliveryEvent
            {
                DeliveryEventId = 2,
                DeliveryId = 1,
                DeliveryEventDesc = "Started",
                Timestamp = DateTime.UtcNow,
                StatusId = (int)DeliveryStatusCode.Started,
                CheckpointId = null
            });
        await db.SaveChangesAsync();

        var sut = new DeliveryService(db);

        Assert.ThrowsAsync<InvalidOperationException>(() => sut.DeleteDeliveryAsync(1));
    }
}
