using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Driver.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Drivers";

    public DatabaseConfiguration? Database { get; init; }
}
