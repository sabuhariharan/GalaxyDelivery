using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Controllers.Convertors;

public class DeliveryConvertor(DeliveryEventConvertor deliveryEventConvertor)
{
    public DeliveryDetailDto ToDetailDto(Delivery delivery)
    {
        if (delivery is null)
        {
            return null;
        }

        return new DeliveryDetailDto(
            delivery.DeliveryId,
            delivery.Origin,
            delivery.Destination,
            delivery.Driver?.DriverName,
            delivery.Vehicle?.VehicleMake,
            delivery.Route?.RouteName,
            (delivery.DeliveryEvent ?? Enumerable.Empty<DeliveryEvent>()).Select(e => new DeliveryEventItemDto(
                e.DeliveryEventId,
                e.DeliveryEventDesc,
                e.Timestamp,
                e.DeliveryId,
                e.Checkpoint?.CheckpointName,
                e.Status?.StatusName)));
    }

    public Delivery ToEntity(CreateDeliveryRequestDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new Delivery
        {
            Origin = dto.Origin,
            Destination = dto.Destination,
            DriverId = dto.DriverId,
            VehicleId = dto.VehicleId,
            RouteId = dto.RouteId
        };
    }
}
