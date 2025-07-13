namespace TaxiService.Common.Messaging.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record RabbitMqConfiguration
{
    public const string SectionName = "RabbitMq";
    
    public string? Address { get; init; }

    public int? Port { get; init; }

    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string? VirtualHost { get; init; }

    public Dictionary<string, RabbitMqModuleConfiguration>? Modules { get; init; }
}
