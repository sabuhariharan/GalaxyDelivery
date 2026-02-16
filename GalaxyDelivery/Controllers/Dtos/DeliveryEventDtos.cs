using System;

namespace GalaxyDelivery.Controllers.Dtos;

public sealed class CreateDeliveryEventRequestDto
{
    public CreateDeliveryEventRequestDto(string deliveryEventDesc, DateTime? timestamp, int deliveryId, int? checkpointId, int statusId)
    {
        DeliveryEventDesc = deliveryEventDesc;
        Timestamp = timestamp;
        DeliveryId = deliveryId;
        CheckpointId = checkpointId;
        StatusId = statusId;
    }

    public string DeliveryEventDesc { get; set; }

    public DateTime? Timestamp { get; set; }

    public int DeliveryId { get; set; }

    public int? CheckpointId { get; set; }

    public int StatusId { get; set; }
}

public sealed class UpdateDeliveryEventRequestDto
{
    public UpdateDeliveryEventRequestDto(string deliveryEventDesc, DateTime timestamp, int deliveryId, int? checkpointId, int statusId)
    {
        DeliveryEventDesc = deliveryEventDesc;
        Timestamp = timestamp;
        DeliveryId = deliveryId;
        CheckpointId = checkpointId;
        StatusId = statusId;
    }

    public string DeliveryEventDesc { get; set; }

    public DateTime Timestamp { get; set; }

    public int DeliveryId { get; set; }

    public int? CheckpointId { get; set; }

    public int StatusId { get; set; }
}
