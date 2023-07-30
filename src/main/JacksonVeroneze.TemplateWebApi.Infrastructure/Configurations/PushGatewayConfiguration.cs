namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class PushGatewayConfiguration
{
    public bool? Enable { get; init; }

    public string? Address { get; init; }
}
