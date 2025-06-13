using MediatR;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Common.BaseInfrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqDomainEventDispatcher(
    IPublisher publisher) : IDomainEventDispatcher
{
    public async Task DispatchAsync(
        BaseAggregateRoot aggregate,
        CancellationToken cancellation = default)
    {
        IEnumerable<Task> taks = aggregate.Events?
            .Select(evt => publisher.Publish(evt, cancellation)) ?? [];

        await Task.WhenAll(taks);

        aggregate.ClearEvents();
    }
}
