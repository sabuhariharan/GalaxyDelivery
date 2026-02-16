using System.Threading;
using System.Threading.Tasks;

namespace GalaxyDelivery.Events;

public interface IDomainEventPublisher
{
    Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default);
}
