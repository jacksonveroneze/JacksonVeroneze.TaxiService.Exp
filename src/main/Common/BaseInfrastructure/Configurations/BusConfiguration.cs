namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public record BusConfiguration
{
    public string? Address { get; init; }

    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string? Exchange { get; init; }
}