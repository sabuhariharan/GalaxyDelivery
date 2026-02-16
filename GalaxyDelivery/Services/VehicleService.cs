using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GalaxyDelivery.Services
{
    public class VehicleService(GalaxyDbContext db) : IVehicleService
    {
        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await db.Vehicle.AsNoTracking().ToListAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(int vehicleId)
        {
            return await db.Vehicle.AsNoTracking().SingleOrDefaultAsync(v => v.VehicleId == vehicleId);
        }

        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            db.Vehicle.Add(vehicle);
            await db.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            db.Vehicle.Update(vehicle);
            await db.SaveChangesAsync();
            return vehicle;
        }

        public async Task DeleteVehicleAsync(int vehicleId)
        {
            var existing = await db.Vehicle.SingleOrDefaultAsync(v => v.VehicleId == vehicleId);
            if (existing is null)
            {
                return;
            }

            db.Vehicle.Remove(existing);
            await db.SaveChangesAsync();
        }
    }
}