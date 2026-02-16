using System;

namespace GalaxyDelivery.Entities
{
    public class DeliveryEvent
    {
        public int DeliveryEventId { get; set; }

        public string DeliveryEventDesc { get; set; }

        public DateTime Timestamp { get; set; }

        public int DeliveryId { get; set; }

        public Delivery Delivery { get; set; }

        public int? CheckpointId { get; set; }

        public Checkpoint Checkpoint { get; set; }

        public int StatusId { get; set; }

        public DeliveryStatus Status { get; set; }
    }
}
