using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Passengers.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Passengers";
    
    public DatabaseConfiguration? Database { get; init; }
}