using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GalaxyDelivery.Services
{
    public class RoleService(GalaxyDbContext db) : IRouteService
    {
        public async Task<IEnumerable<DeliveryRoute>> GetRoutesAsync()
        {
            return await db.Set<DeliveryRoute>().AsNoTracking().Include(r => r.Checkpoint).ToListAsync();
        }

        public async Task<DeliveryRoute> GetRouteAsync(int routeId)
        {
            return await db.Set<DeliveryRoute>().AsNoTracking().Include(r => r.Checkpoint)
                .SingleOrDefaultAsync(r => r.RouteId == routeId);
        }

        public async Task<DeliveryRoute> CreateRouteAsync(DeliveryRoute route)
        {
            db.Set<DeliveryRoute>().Add(route);
            await db.SaveChangesAsync();
            return route;
        }

        public async Task<DeliveryRoute> UpdateRouteAsync(DeliveryRoute route)
        {
            db.Set<DeliveryRoute>().Update(route);
            await db.SaveChangesAsync();
            return route;
        }

        public async Task DeleteRouteAsync(int routeId)
        {
            var existing = await db.Set<DeliveryRoute>().SingleOrDefaultAsync(r => r.RouteId == routeId);
            if (existing is null)
            {
                return;
            }

            db.Set<DeliveryRoute>().Remove(existing);
            await db.SaveChangesAsync();
        }
    }
}