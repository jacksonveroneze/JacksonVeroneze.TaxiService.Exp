namespace JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;

public interface IIntegrationEventPublisher
{
    public Task PublishAsync<T>(T data,
        CancellationToken cancellationToken) where T : class;
}
