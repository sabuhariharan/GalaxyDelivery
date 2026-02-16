using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Services.Interfaces;

public interface IDeliveryService
{
    Task<IEnumerable<Delivery>> GetDeliveriesAsync();

    Task<Delivery> GetDeliveryAsync(int deliveryId);

    Task<Delivery> CreateDeliveryAsync(Delivery delivery);

    Task<Delivery> UpdateDeliveryAsync(Delivery delivery);

    Task DeleteDeliveryAsync(int deliveryId);
}
