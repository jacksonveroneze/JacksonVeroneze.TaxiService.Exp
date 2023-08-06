using JacksonVeroneze.NET.HttpClient.Configuration;
using JacksonVeroneze.NET.HttpClient.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients.Old;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientExtension
{
    public static IServiceCollection AddHttpClients(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddClient<IIbgeApi>(appConfiguration, "ibge");

        return services;
    }

    private static IServiceCollection AddClient<TClient>(
        this IServiceCollection services,
        AppConfiguration appConfiguration,
        string name) where TClient : class
    {
        HttpClientConfiguration config =
            appConfiguration.HttpClients!.Single(
                client => client.Name!.Equals(name,
                    StringComparison.OrdinalIgnoreCase));

        services.RefitClientBuilder<TClient>(config)
            .UseHttpClientMetrics();

        return services;
    }
}
