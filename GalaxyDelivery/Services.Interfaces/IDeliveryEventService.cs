using System.Collections.Generic;
using System.Threading.Tasks;
using GalaxyDelivery.Entities;

namespace GalaxyDelivery.Services.Interfaces;

public interface IDeliveryEventService
{
    Task<IEnumerable<DeliveryEvent>> GetDeliveryEventsAsync();

    Task<DeliveryEvent> GetDeliveryEventAsync(int deliveryEventId);

    Task<DeliveryEvent> CreateDeliveryEventAsync(DeliveryEvent deliveryEvent);

    Task<DeliveryEvent> UpdateDeliveryEventAsync(DeliveryEvent deliveryEvent);

    Task DeleteDeliveryEventAsync(int deliveryEventId);
}
