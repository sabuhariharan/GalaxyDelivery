using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Services.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<DeliveryRoute>> GetRoutesAsync();

        Task<DeliveryRoute> GetRouteAsync(int routeId);

        Task<DeliveryRoute> CreateRouteAsync(DeliveryRoute route);

        Task<DeliveryRoute> UpdateRouteAsync(DeliveryRoute route);

        Task DeleteRouteAsync(int routeId);
    }
}