namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class DatabaseConfiguration
{
    public string? ReadConnectionString { get; init; }

    public string? WriteConnectionString { get; init; }
}
