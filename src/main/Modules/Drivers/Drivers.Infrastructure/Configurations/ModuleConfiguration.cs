using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Drivers.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Drivers";

    public DatabaseConfiguration? Database { get; init; }
}
