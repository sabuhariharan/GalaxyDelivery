namespace GalaxyDelivery.Entities
{
    public class Checkpoint
    {
        public int CheckpointId { get; set; }

        public string CheckpointName { get; set; }

        public int RouteId { get; set; }

        public DeliveryRoute Route { get; set; }
    }
}
