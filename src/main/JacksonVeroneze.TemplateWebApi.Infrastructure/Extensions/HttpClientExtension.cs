using CorrelationId.HttpClient;
using JacksonVeroneze.NET.HttpClient.Configuration;
using JacksonVeroneze.NET.HttpClient.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients;
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
        services.AddClientApi<IIbgeApi>(appConfiguration, "ibge");

        return services;
    }

    private static void AddClientApi<TClient>(
        this IServiceCollection services,
        AppConfiguration appConfiguration,
        string name) where TClient : class
    {
        HttpClientConfiguration config =
            appConfiguration.HttpClients!.Single(
                client => client.Name!.Equals(name,
                    StringComparison.OrdinalIgnoreCase));

        services.RefitClientBuilder<TClient>(config)
            .AddCorrelationIdForwarding()
            .UseHttpClientMetrics();
    }
}