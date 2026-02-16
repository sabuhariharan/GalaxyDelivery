using System.Collections.Generic;
using System.Linq;
using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Controllers.Convertors;

public class DeliveryRouteConvertor(CheckpointConvertor checkpointConvertor)
{
    public DeliveryRouteDto ToDto(DeliveryRoute route)
    {
        if (route is null)
        {
            return null;
        }

        return new DeliveryRouteDto(
            route.RouteId,
            route.RouteName,
            route.Checkpoint?.Select(checkpointConvertor.ToDto) ?? Enumerable.Empty<CheckpointDto>());
    }

    public DeliveryRoute ToEntity(DeliveryRouteDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new DeliveryRoute
        {
            RouteId = dto.RouteId,
            RouteName = dto.RouteName,
            Checkpoint = dto.Checkpoints?.Select(checkpointConvertor.ToEntity).ToList() ?? new List<Checkpoint>()
        };
    }
}
