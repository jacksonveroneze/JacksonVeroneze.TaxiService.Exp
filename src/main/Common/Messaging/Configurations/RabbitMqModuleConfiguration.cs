namespace TaxiService.Common.Messaging.Configurations;

internal sealed class RabbitMqModuleConfiguration
{
    public string? Exchange { get; init; }

    public Dictionary<string, string>? Routing { get; init; }
}
