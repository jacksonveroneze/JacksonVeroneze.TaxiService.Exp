using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Passenger.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Passengers";

    public DatabaseConfiguration? Database { get; init; }
}
