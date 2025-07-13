using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.SharedKernel.Extensions;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppConfigurationExtensions
{
    public static IServiceCollection AddAppConfigs(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddConfiguration<AppConfiguration>(configuration);

        return services;
    }
}
