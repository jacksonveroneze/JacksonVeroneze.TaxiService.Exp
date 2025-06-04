namespace TaxiService.Common.BaseInfrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed record BusConfiguration
{
    public string? Address { get; init; }

    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string? Exchange { get; init; }
}
