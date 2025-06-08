using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Ride.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Rides";

    public DatabaseConfiguration? Database { get; init; }
}
