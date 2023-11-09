using System.Text.Json;
using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Messaging;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents;
using JacksonVeroneze.TemplateWebApi.Domain.DomainEvents.Base;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using RabbitMQ.Client;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class RabbitMqEventPublisher : IIntegrationEventPublisher
{
    private readonly AppConfiguration _appConfiguration;

    public RabbitMqEventPublisher(
        AppConfiguration appConfiguration)
    {
        _appConfiguration = appConfiguration;
    }

    public Task PublishAsync(BaseDomainEvent data,
        CancellationToken cancellationToken)
    {
        BusConfiguration? configuration =
            _appConfiguration.Bus;

        ConnectionFactory factory = new()
        {
            HostName = configuration!.Address,
            UserName = configuration.UserName,
            Password = configuration.Password
        };

        using IConnection? connection = factory.CreateConnection();

        using IModel? channel = connection.CreateModel();

        byte[] body = JsonSerializer.SerializeToUtf8Bytes(data);

        channel.BasicPublish(
            exchange: configuration.Exchange,
            routingKey: data.Type,
            body: body);

        return Task.CompletedTask;
    }
}
