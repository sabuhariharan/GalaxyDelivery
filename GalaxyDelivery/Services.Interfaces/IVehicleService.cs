using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();

        Task<Vehicle> GetVehicleAsync(int vehicleId);

        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);

        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);

        Task DeleteVehicleAsync(int vehicleId);
    }
}