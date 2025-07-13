using TaxiService.Common.Messaging.Abstractions;
using TaxiService.Common.Messaging.Configurations;

namespace TaxiService.Common.Messaging.RabbitMQ;

internal sealed class RabbitMqEventMapper(
    RabbitMqConfiguration configuration) : IRabbitMqEventMapper
{
    public (string exchange, string routingKey) Resolve(
        string moduleName, string typeName)
    {
        bool hasModuleConfiguration = configuration.Modules!
            .TryGetValue(moduleName, 
                out RabbitMqModuleConfiguration? moduleConfiguration);

        if (!hasModuleConfiguration)
        {
            throw new KeyNotFoundException(
                $"Module key for {moduleName} not found in configuration.");
        }

        bool hasEventConfiguration = moduleConfiguration!.Routing!
            .TryGetValue(typeName, out string? routingKey);

        if (!hasEventConfiguration)
        {
            throw new KeyNotFoundException(
                $"Routing key for {typeName} not found in configuration.");
        }

        return (moduleConfiguration.Exchange!, routingKey!);
    }
}
