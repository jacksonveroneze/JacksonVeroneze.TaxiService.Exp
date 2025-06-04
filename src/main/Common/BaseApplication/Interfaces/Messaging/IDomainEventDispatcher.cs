using TaxiService.Common.BaseDomain.Entities.Base;

namespace TaxiService.Common.BaseApplication.Interfaces.Messaging;

public interface IDomainEventDispatcher
{
    public Task DispatchAsync(BaseAggregateRoot aggregate);
}
