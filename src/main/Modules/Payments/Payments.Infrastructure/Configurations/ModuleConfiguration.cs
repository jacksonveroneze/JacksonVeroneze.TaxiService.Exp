using JacksonVeroneze.NET.HttpClient.Configuration;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Modules.Payments.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
internal sealed record ModuleConfiguration
{
    internal const string Name = "Payments";
    
    public DatabaseConfiguration? Database { get; init; }
    
    public IReadOnlyCollection<HttpClientConfiguration>? HttpClients { get; init; }
}