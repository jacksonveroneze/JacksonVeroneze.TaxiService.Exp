using System.Text.Json;
using System.Text.Json.Serialization;
using JacksonVeroneze.NET.HttpClient.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus;
using Refit;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpClientExtension
{
    public static IServiceCollection AddHttpClient<TClient>(
        this IServiceCollection services,
        HttpClientConfiguration config) where TClient : class
    {
        services
            .AddRefitClient<TClient>(GetDefaultRefitJsonSettings())
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(config.Address!);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                HttpClientHandler handler = new();

                return handler;
            })
            .UseHttpClientMetrics()
            .AddStandardResilienceHandler();

        return services;
    }

    private static RefitSettings GetDefaultRefitJsonSettings()
    {
        JsonSerializerOptions jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() }
        };

        RefitSettings settings = new()
        {
            ContentSerializer =
                new SystemTextJsonContentSerializer(jsonOptions)
        };

        return settings;
    }
}
