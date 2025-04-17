using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Rides.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Rides";
    
    public DatabaseConfiguration? Database { get; init; }
}