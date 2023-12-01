using JacksonVeroneze.NET.DomainObjects.Messaging;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher(
    AppConfiguration appConfiguration) : IIntegrationEventPublisher
{
    public Task PublishAsync(DomainEvent data,
        CancellationToken cancellationToken)
    {
        // _ = Task.Run(() =>
        // {
        //     BusConfiguration? configuration =
        //         _appConfiguration.Bus;
        //
        //     ConnectionFactory factory = new()
        //     {
        //         HostName = configuration!.Address,
        //         UserName = configuration.UserName,
        //         Password = configuration.Password
        //     };
        //
        //     using IConnection? connection = factory.CreateConnection();
        //
        //     using IModel? channel = connection.CreateModel();
        //
        //     byte[] body = JsonSerializer.SerializeToUtf8Bytes(data);
        //
        //     channel.BasicPublish(
        //         exchange: configuration.Exchange,
        //         routingKey: data.GetType().Name,
        //         body: body);
        // }, cancellationToken);

        return Task.CompletedTask;
    }
}
