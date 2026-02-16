using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Controllers.Convertors;

public class CheckpointConvertor
{
    public CheckpointDto ToDto(Checkpoint checkpoint)
    {
        if (checkpoint is null)
        {
            return null;
        }

        return new CheckpointDto(checkpoint.CheckpointId, checkpoint.CheckpointName, checkpoint.Route?.RouteName);
    }

    public Checkpoint ToEntity(CheckpointDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new Checkpoint
        {
            CheckpointId = dto.CheckpointId,
            CheckpointName = dto.CheckpointName,
            RouteId = 0
        };
    }
}
