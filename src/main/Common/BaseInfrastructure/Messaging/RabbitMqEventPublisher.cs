using JacksonVeroneze.NET.DomainObjects.Messaging;
using TaxiService.Common.BaseApplication.Interfaces.Messaging;

namespace TaxiService.Common.BaseInfrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}