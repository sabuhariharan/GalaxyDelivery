using System;

namespace GalaxyDelivery.Events;

public sealed class DeliveryFinalizedEvent
{
    public DeliveryFinalizedEvent(int deliveryId, int statusId, string statusName, DateTime timestamp)
    {
        DeliveryId = deliveryId;
        StatusId = statusId;
        StatusName = statusName;
        Timestamp = timestamp;
    }

    public int DeliveryId { get; }

    public int StatusId { get; }

    public string StatusName { get; }

    public DateTime Timestamp { get; }

    public bool IsCompleted => StatusId == (int)Entities.DeliveryStatusCode.Completed;

    public bool IsFailed => StatusId == (int)Entities.DeliveryStatusCode.Failed;
}
