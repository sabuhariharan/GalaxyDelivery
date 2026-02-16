using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Services.Interfaces
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetDriversAsync();

        Task<Driver> GetDriverAsync(int driverId);

        Task<Driver> CreateDriverAsync(Driver driver);

        Task<Driver> UpdateDriverAsync(Driver driver);

        Task DeleteDriverAsync(int driverId);
    }
}