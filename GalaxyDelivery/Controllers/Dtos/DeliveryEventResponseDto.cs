using System;

namespace GalaxyDelivery.Controllers.Dtos;

public sealed class DeliveryEventResponseDto
{
    public DeliveryEventResponseDto(int deliveryEventId, string deliveryEventDesc, DateTime timestamp, int deliveryId, string checkpointName, string statusName)
    {
        DeliveryEventId = deliveryEventId;
        DeliveryEventDesc = deliveryEventDesc;
        Timestamp = timestamp;
        DeliveryId = deliveryId;
        CheckpointName = checkpointName;
        StatusName = statusName;
    }

    public int DeliveryEventId { get; set; }

    public string DeliveryEventDesc { get; set; }

    public DateTime Timestamp { get; set; }

    public int DeliveryId { get; set; }

    public string CheckpointName { get; set; }

    public string StatusName { get; set; }
}
