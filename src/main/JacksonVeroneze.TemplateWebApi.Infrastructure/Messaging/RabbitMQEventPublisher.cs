using EasyNetQ;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    private readonly IBus _bus;

    public RabbitMqEventPublisher(IBus bus)
    {
        _bus = bus;
    }

    public Task PublishAsync<T>(T data,
        CancellationToken cancellationToken) where T : class
    {
        return _bus.PubSub.PublishAsync(data,
            cancellationToken);
    }
}
