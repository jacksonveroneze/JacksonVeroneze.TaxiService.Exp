namespace TaxiService.Common.Messaging.Abstractions;

public interface IMessageBusPublisher
{
    Task PublishAsync<T>(
        T evt,
        CancellationToken cancellationToken = default)
        where T : BaseIntegrationEvent;
}
