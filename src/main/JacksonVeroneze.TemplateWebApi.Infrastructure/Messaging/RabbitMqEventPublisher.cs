using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    public Task PublishAsync<T>(T data,
        CancellationToken cancellationToken) where T : class
    {
        return Task.CompletedTask;
    }
}
