using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Controllers.Convertors;

public class DriverConvertor
{
    public DriverDto ToDto(Driver driver)
    {
        if (driver is null)
        {
            return null;
        }

        return new DriverDto(driver.DriverId, driver.DriverName);
    }

    public Driver ToEntity(DriverDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new Driver
        {
            DriverId = dto.DriverId,
            DriverName = dto.DriverName
        };
    }
}
