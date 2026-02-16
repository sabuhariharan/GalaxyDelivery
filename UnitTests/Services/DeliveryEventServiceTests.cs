using GalaxyDelivery.Entities;
using GalaxyDelivery.Events;
using GalaxyDelivery.Services;
using Microsoft.EntityFrameworkCore;
using UnitTests.TestHelpers;

namespace UnitTests.Services;

[TestFixture]
public class DeliveryEventServiceTests
{
    [Test]
    public async Task GetDeliveryEventAsync_IncludesStatusAndCheckpoint()
    {
        await using var db = DbContextHelper.CreateInMemoryDbContext(nameof(GetDeliveryEventAsync_IncludesStatusAndCheckpoint));

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

        var publisher = new Mock<IDomainEventPublisher>();
        var sut = new DeliveryEventService(db, publisher.Object);

        var ev = await sut.GetDeliveryEventAsync(5);

        Assert.That(ev, Is.Not.Null);
        Assert.That(ev.Status, Is.Not.Null);
        Assert.That(ev.Checkpoint, Is.Not.Null);
    }
}
