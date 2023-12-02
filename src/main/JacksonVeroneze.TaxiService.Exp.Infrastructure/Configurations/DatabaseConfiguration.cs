namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public class DatabaseConfiguration
{
    public string? ReadConnectionString { get; init; }

    public string? WriteConnectionString { get; init; }
}
