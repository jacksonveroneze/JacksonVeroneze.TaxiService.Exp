using JacksonVeroneze.NET.DomainObjects.Messaging;

using TaxiService.Common.BaseApplication.Interfaces.Messaging;
using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Common.BaseInfrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqDomainEventDispatcher : IDomainEventDispatcher
{
    public async Task DispatchAsync(BaseAggregateRoot aggregate)
    {
        foreach (DomainEvent domainEvent in aggregate.Events ?? [])
        {
            await Task.Delay(100);
        }

        aggregate.ClearEvents();
    }
}
