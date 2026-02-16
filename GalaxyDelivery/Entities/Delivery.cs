using System.Collections.Generic;

namespace GalaxyDelivery.Entities
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public int DriverId { get; set; }

        public Driver Driver { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }

        public int RouteId { get; set; }

        public DeliveryRoute Route { get; set; }

        public ICollection<DeliveryEvent> DeliveryEvent { get; set; }
    }
}