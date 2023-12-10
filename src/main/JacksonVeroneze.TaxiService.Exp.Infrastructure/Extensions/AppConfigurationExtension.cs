using JacksonVeroneze.TaxiService.Exp.Domain.Parameters;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppConfigurationExtension
{
    public static AppConfiguration AddAppConfigs(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddConfiguration<AppConfiguration>(configuration)
            .AddConfiguration<UserParameters>(configuration, UserParameters.Name);

        using ServiceProvider provider =
            services.BuildServiceProvider();

        return provider
            .GetRequiredService<AppConfiguration>();
    }

    private static IServiceCollection AddConfiguration<TParameterType>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null) where TParameterType : class
    {
        IConfiguration section =
            string.IsNullOrEmpty(sectionName)
                ? configuration
                : configuration.GetSection(sectionName);

        services.Configure<TParameterType>(section);

        services.AddScoped(conf =>
            conf.GetService<IOptionsMonitor<TParameterType>>()?.CurrentValue!);

        return services;
    }
}
