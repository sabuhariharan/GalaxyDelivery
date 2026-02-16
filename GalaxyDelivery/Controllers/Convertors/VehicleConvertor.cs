using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Controllers.Convertors;

public class VehicleConvertor
{
    public VehicleDto ToDto(Vehicle vehicle)
    {
        if (vehicle is null)
        {
            return null;
        }

        return new VehicleDto(vehicle.VehicleId, vehicle.VehicleMake);
    }

    public Vehicle ToEntity(VehicleDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new Vehicle
        {
            VehicleId = dto.VehicleId,
            VehicleMake = dto.VehicleMake
        };
    }
}
