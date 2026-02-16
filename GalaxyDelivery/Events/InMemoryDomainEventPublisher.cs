using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace GalaxyDelivery.Events;

public sealed class InMemoryDomainEventPublisher(IServiceProvider serviceProvider) : IDomainEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent domainEvent, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();

        var handlers = scope.ServiceProvider.GetServices<IDomainEventHandler<TEvent>>();
        if (handlers is null)
        {
            return;
        }

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(domainEvent, cancellationToken);
        }
    }
}
