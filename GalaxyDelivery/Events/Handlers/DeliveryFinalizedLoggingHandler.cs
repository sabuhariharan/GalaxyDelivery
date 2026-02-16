using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GalaxyDelivery.Events.Handlers;

public sealed class DeliveryFinalizedLoggingHandler(ILogger<DeliveryFinalizedLoggingHandler> logger)
    : IDomainEventHandler<DeliveryFinalizedEvent>
{
    public Task HandleAsync(DeliveryFinalizedEvent domainEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            "Delivery {DeliveryId} finalized with status {StatusName} ({StatusId}) at {Timestamp}",
            domainEvent.DeliveryId,
            domainEvent.StatusName,
            domainEvent.StatusId,
            domainEvent.Timestamp);

        return Task.CompletedTask;
    }
}
