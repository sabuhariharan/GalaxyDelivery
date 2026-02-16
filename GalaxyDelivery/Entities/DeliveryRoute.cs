using System.Collections.Generic;

namespace GalaxyDelivery.Entities
{
    public class DeliveryRoute
    {
        public int RouteId { get; set; }

    public string RouteName { get; set; }

        public ICollection<Checkpoint> Checkpoint { get; set; }
    }
}