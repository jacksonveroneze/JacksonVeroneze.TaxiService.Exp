namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class DatabaseConfiguration
{
    public string? ConnectionString { get; init; }

    public string? DatabaseName { get; init; }

    public DatabaseType Type { get; init; }
}

public enum DatabaseType
{
    Dapper,
    EntityFramework
}
