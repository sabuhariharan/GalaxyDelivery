using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace GalaxyDelivery.Services;

public class DeliveryService(GalaxyDbContext db) : IDeliveryService
{
    public async Task<IEnumerable<Delivery>> GetDeliveriesAsync()
    {
        return await db.Delivery.AsNoTracking()
            .Include(d => d.Driver)
            .Include(d => d.Vehicle)
            .Include(d => d.Route)
            .Include(d => d.DeliveryEvent)
            .ThenInclude(de => de.Status)
            .Include(d => d.DeliveryEvent)
            .ThenInclude(de => de.Checkpoint)
            .ToListAsync();
    }

    public async Task<Delivery> GetDeliveryAsync(int deliveryId)
    {
        return await db.Delivery.AsNoTracking()
            .Include(d => d.Driver)
            .Include(d => d.Vehicle)
            .Include(d => d.Route)
            .Include(d => d.DeliveryEvent)
            .ThenInclude(de => de.Status)
            .Include(d => d.DeliveryEvent)
            .ThenInclude(de => de.Checkpoint)
            .SingleOrDefaultAsync(d => d.DeliveryId == deliveryId);
    }

    public async Task<Delivery> CreateDeliveryAsync(Delivery delivery)
    {
        db.Delivery.Add(delivery);
        await db.SaveChangesAsync();
        return delivery;
    }

    public async Task<Delivery> UpdateDeliveryAsync(Delivery delivery)
    {
        db.Delivery.Update(delivery);
        await db.SaveChangesAsync();
        return delivery;
    }

    public async Task DeleteDeliveryAsync(int deliveryId)
    {
        var existing = await db.Delivery
            .Include(d => d.DeliveryEvent)
            .SingleOrDefaultAsync(d => d.DeliveryId == deliveryId);
        if (existing is null)
        {
            return;
        }

        var lastStatusId = (existing.DeliveryEvent ?? Array.Empty<DeliveryEvent>())
            .OrderBy(e => e.Timestamp)
            .Select(e => e.StatusId)
            .LastOrDefault();

        if (lastStatusId > (int)DeliveryStatusCode.Planned)
        {
            throw new InvalidOperationException("Delivery cannot be deleted because its last delivery event status is beyond Planned.");
        }

        db.Delivery.Remove(existing);
        await db.SaveChangesAsync();
    }
}
