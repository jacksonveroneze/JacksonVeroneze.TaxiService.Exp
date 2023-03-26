using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AplicationConfigurationExtension
{
    public static AppConfiguration AddAppConfigs(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddConfiguration<AppConfiguration>(configuration);

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

        services
            .AddOptions<TParameterType>(sectionName)
            .Bind(section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        //services.Configure<TParameterType>(section);

        services.AddScoped(conf =>
            conf.GetService<IOptionsMonitor<TParameterType>>()?.CurrentValue!);

        return services;
    }
}