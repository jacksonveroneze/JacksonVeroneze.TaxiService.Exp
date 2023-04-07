using JacksonVeroneze.NET.HttpClient.Configuration;
using JacksonVeroneze.NET.HttpClient.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.HttpClients;
using Microsoft.Extensions.DependencyInjection;

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

    private static IServiceCollection AddClientApi<TClient>(
        this IServiceCollection services,
        AppConfiguration appConfiguration,
        string name) where TClient : class
    {
        HttpClientConfiguration config = appConfiguration.HttpClients!
            .Single(client => client.Name!.Equals(name,
                StringComparison.OrdinalIgnoreCase));

        services.ClientBuilder<TClient>(config);

        return services;
    }
}