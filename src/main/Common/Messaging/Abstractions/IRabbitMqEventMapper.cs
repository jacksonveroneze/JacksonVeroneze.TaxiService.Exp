namespace TaxiService.Common.Messaging.Abstractions;

internal interface IRabbitMqEventMapper
{
    (string exchange, string routingKey) Resolve(
        string moduleName, string typeName);
}
