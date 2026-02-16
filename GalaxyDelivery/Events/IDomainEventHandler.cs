using System.Threading;
using System.Threading.Tasks;

namespace GalaxyDelivery.Events;

public interface IDomainEventHandler<in TEvent>
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken = default);
}
