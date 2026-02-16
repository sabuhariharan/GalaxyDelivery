using GalaxyDelivery.Controllers.Dtos;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Controllers.Convertors;

public class DeliveryEventConvertor
{
    public DeliveryEventDto ToDto(DeliveryEvent deliveryEvent)
    {
        if (deliveryEvent is null)
        {
            return null;
        }

        return new DeliveryEventDto(
            deliveryEvent.DeliveryEventId,
            deliveryEvent.DeliveryEventDesc,
            deliveryEvent.Timestamp,
            deliveryEvent.DeliveryId,
            deliveryEvent.CheckpointId,
            deliveryEvent.Checkpoint?.CheckpointName,
            deliveryEvent.StatusId,
            deliveryEvent.Status?.StatusName);
    }

    public DeliveryEvent ToEntity(DeliveryEventDto dto)
    {
        if (dto is null)
        {
            return null;
        }

        return new DeliveryEvent
        {
            DeliveryEventId = dto.DeliveryEventId,
            DeliveryEventDesc = dto.DeliveryEventDesc,
            Timestamp = dto.Timestamp,
            DeliveryId = dto.DeliveryId,
            CheckpointId = dto.CheckpointId,
            StatusId = dto.StatusId
        };
    }
}
