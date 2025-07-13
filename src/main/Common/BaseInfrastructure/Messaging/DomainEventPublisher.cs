using MediatR;
using TaxiService.Common.SharedKernel;
using TaxiService.Common.SharedKernel.Interfaces;

namespace TaxiService.Common.BaseInfrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class DomainEventPublisher(
    IPublisher publisher) : IDomainEventPublisher
{
    public async Task PublishAsync(
        BaseAggregateRoot aggregate,
        CancellationToken cancellation = default)
    {
        try
        {
            IEnumerable<Task> taks = aggregate.Events?
                .Select(evt => publisher.Publish(evt, cancellation)) ?? [];

            await Task.WhenAll(taks);
        }
        finally
        {
            aggregate.ClearEvents();
        }
    }
}
