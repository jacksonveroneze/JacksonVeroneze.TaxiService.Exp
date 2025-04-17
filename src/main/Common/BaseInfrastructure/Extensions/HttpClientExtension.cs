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
            .AddHttpMessageHandler(() => new PrometheusHttpClientHandler())
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

public class PrometheusHttpClientHandler : DelegatingHandler
{
    private static readonly string[] labelNames = new[] { "method", "host", "route" };

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var route = request.RequestUri?.AbsolutePath ?? "unknown_route";

        // Aqui, registre a métrica com uma etiqueta adicional
        Metrics.CreateCounter("http_client_requests_total", "Contagem de chamadas HTTP",
                labelNames)
            .WithLabels(request.Method.Method, request.RequestUri?.Host ?? "unknown_host", route)
            .Inc();

        return await base.SendAsync(request, cancellationToken);
    }
}