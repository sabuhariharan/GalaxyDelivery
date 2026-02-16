using GalaxyDelivery.Entities;
using GalaxyDelivery.Services.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalaxyDelivery.Services
{
    public class DriverService(GalaxyDbContext db, IValidator<Driver> driverValidator) : IDriverService
    {
    public async Task<IEnumerable<Driver>> GetDriversAsync()
        {
        return await db.Driver.AsNoTracking().ToListAsync();
        }

    public async Task<Driver> GetDriverAsync(int driverId)
        {
        return await db.Driver.AsNoTracking().SingleOrDefaultAsync(d => d.DriverId == driverId);
        }

    public async Task<Driver> CreateDriverAsync(Driver driver)
        {
            driverValidator.ValidateAndThrow(driver);

        db.Driver.Add(driver);
        await db.SaveChangesAsync();
            return driver;
        }

    public async Task<Driver> UpdateDriverAsync(Driver driver)
        {
            driverValidator.ValidateAndThrow(driver);

            db.Driver.Update(driver);
        await db.SaveChangesAsync();
            return driver;
        }

    public async Task DeleteDriverAsync(int driverId)
        {
        var existing = await db.Driver.SingleOrDefaultAsync(d => d.DriverId == driverId);
            if (existing is null)
            {
                return;
            }

            db.Driver.Remove(existing);
        await db.SaveChangesAsync();
        }
    }
}