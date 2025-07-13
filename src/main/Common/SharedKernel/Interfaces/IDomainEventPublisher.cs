namespace TaxiService.Common.SharedKernel.Interfaces;

public interface IDomainEventPublisher
{
    Task PublishAsync(
        BaseAggregateRoot aggregate,
        CancellationToken cancellation = default);
}
