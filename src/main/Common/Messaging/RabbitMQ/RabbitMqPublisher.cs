using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using TaxiService.Common.Messaging.Abstractions;

namespace TaxiService.Common.Messaging.RabbitMQ;

[ExcludeFromCodeCoverage]
internal sealed class RabbitMqPublisher(
    IConnectionFactory connectionFactory,
    IRabbitMqEventMapper resolver) : IMessageBusPublisher, IAsyncDisposable
{
    private IConnection? _connection;
    private readonly SemaphoreSlim _connectionSemaphore = new(1, 1);

    public async Task PublishAsync<T>(
        T evt,
        CancellationToken cancellationToken = default)
        where T : BaseIntegrationEvent
    {
        ArgumentNullException.ThrowIfNull(evt);

        (string exchange, string routingKey) = resolver.Resolve(
            evt.ModuleName!, evt.EvtName!);

        string json = JsonSerializer.Serialize(evt);
        byte[] body = Encoding.UTF8.GetBytes(json);

        await using IChannel channel = await GetChannelAsync();

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: routingKey,
            body: body,
            cancellationToken: cancellationToken);
    }

    private async Task<IChannel> GetChannelAsync()
    {
        await EnsureConnectionAsync();

        return await _connection!.CreateChannelAsync(
            cancellationToken: CancellationToken.None);
    }

    private async Task EnsureConnectionAsync()
    {
        if (_connection?.IsOpen == true)
        {
            return;
        }

        await _connectionSemaphore.WaitAsync(CancellationToken.None);

        try
        {
            if (_connection?.IsOpen != true)
            {
                _connection?.Dispose();
                _connection = await connectionFactory
                    .CreateConnectionAsync(CancellationToken.None);
            }
        }
        finally
        {
            _connectionSemaphore.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        if (_connection != null)
        {
            await _connection.CloseAsync(CancellationToken.None);
            _connection.Dispose();
        }

        _connectionSemaphore.Dispose();
    }
}
