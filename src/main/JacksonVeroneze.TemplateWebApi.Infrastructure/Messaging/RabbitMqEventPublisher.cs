using EasyNetQ;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    private readonly IBus _eventBus;

    public RabbitMqEventPublisher(IBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task PublishAsync<T>(T data,
        CancellationToken cancellationToken) where T : class
    {
        await _eventBus.PubSub.PublishAsync(data, conf =>
            {
                conf.WithTopic("TemplateWebApi");
                conf.WithExpires(TimeSpan.FromSeconds(10));
            },
            cancellationToken: cancellationToken);
    }
}
