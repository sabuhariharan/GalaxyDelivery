using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Events;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GalaxyDelivery.Services;

public class DeliveryEventService(GalaxyDbContext db, IDomainEventPublisher eventPublisher) : IDeliveryEventService
{
    public async Task<IEnumerable<DeliveryEvent>> GetDeliveryEventsAsync()
    {
        return await db.DeliveryEvent.AsNoTracking()
            .Include(de => de.Status)
            .Include(de => de.Checkpoint)
            .ToListAsync();
    }

    public async Task<DeliveryEvent> GetDeliveryEventAsync(int deliveryEventId)
    {
        return await db.DeliveryEvent.AsNoTracking()
            .Include(de => de.Status)
            .Include(de => de.Checkpoint)
            .SingleOrDefaultAsync(de => de.DeliveryEventId == deliveryEventId);
    }

    public async Task<DeliveryEvent> CreateDeliveryEventAsync(DeliveryEvent deliveryEvent)
    {
        db.DeliveryEvent.Add(deliveryEvent);
        await db.SaveChangesAsync();

        await PublishIfFinalizedAsync(deliveryEvent);
        return deliveryEvent;
    }

    public async Task<DeliveryEvent> UpdateDeliveryEventAsync(DeliveryEvent deliveryEvent)
    {
        db.DeliveryEvent.Update(deliveryEvent);
        await db.SaveChangesAsync();

        await PublishIfFinalizedAsync(deliveryEvent);
        return deliveryEvent;
    }

    private async Task PublishIfFinalizedAsync(DeliveryEvent deliveryEvent)
    {
        if (deliveryEvent.StatusId is not ((int)DeliveryStatusCode.Completed or (int)DeliveryStatusCode.Failed)) 
        {
            return;
        }

        var statusName = await db.DeliveryStatus.AsNoTracking()
            .Where(s => s.DeliveryStatusId == deliveryEvent.StatusId)
            .Select(s => s.StatusName)
            .SingleOrDefaultAsync();

        await eventPublisher.PublishAsync(new DeliveryFinalizedEvent(
            deliveryEvent.DeliveryId,
            deliveryEvent.StatusId,
            statusName,
            deliveryEvent.Timestamp));
    }

    public async Task DeleteDeliveryEventAsync(int deliveryEventId)
    {
        var existing = await db.DeliveryEvent.SingleOrDefaultAsync(de => de.DeliveryEventId == deliveryEventId);
        if (existing is null)
        {
            return;
        }

        db.DeliveryEvent.Remove(existing);
        await db.SaveChangesAsync();
    }
}
