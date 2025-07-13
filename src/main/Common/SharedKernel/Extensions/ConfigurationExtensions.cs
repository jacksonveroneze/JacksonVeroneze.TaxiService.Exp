using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TaxiService.Common.SharedKernel.Extensions;

[ExcludeFromCodeCoverage]
public static class ConfigurationExtensions
{
    public static IServiceCollection AddConfiguration<TParameterType>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null)
        where TParameterType : class
    {
        ArgumentNullException.ThrowIfNull(configuration);
        
        IConfiguration section =
            string.IsNullOrEmpty(sectionName)
                ? configuration
                : configuration.GetSection(sectionName);

        services.Configure<TParameterType>(section);

        services.AddScoped(conf =>
            conf.GetService<IOptions<TParameterType>>()?
                .Value!);

        return services;
    }

    public static IServiceCollection AddConfigurationReloaded<TParameterType>(
        this IServiceCollection services,
        IConfiguration configuration,
        string? sectionName = null)
        where TParameterType : class
    {
        ArgumentNullException.ThrowIfNull(configuration);
        
        IConfiguration section =
            string.IsNullOrEmpty(sectionName)
                ? configuration
                : configuration.GetSection(sectionName);

        services.Configure<TParameterType>(section);

        services.AddScoped(conf =>
            conf.GetService<IOptionsMonitor<TParameterType>>()?
                .CurrentValue!);

        return services;
    }
}
