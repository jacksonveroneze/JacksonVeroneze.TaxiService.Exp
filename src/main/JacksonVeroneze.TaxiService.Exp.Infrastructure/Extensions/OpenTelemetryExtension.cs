using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class OpenTelemetryExtension
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        bool isEnabled = appConfiguration
            .DistributedTracing?.IsEnabled ?? false;

        if (!isEnabled)
        {
            return services;
        }

        Action<ResourceBuilder> configureResource = r => r.AddService(
            appConfiguration.AppName,
            serviceVersion: appConfiguration.AppVersion.ToString(),
            serviceInstanceId: Environment.MachineName);

        services.AddOpenTelemetry()
            .ConfigureResource(configureResource)
            .WithTracing(builder =>
            {
                ResourceBuilder resourceBuilder = ResourceBuilder
                    .CreateDefault()
                    .AddService(appConfiguration.Application!.Name!);

                builder
                    .SetSampler(new AlwaysOnSampler())
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        string[] ignoreRoutes = { "/metrics", "/health" };

                        options.RecordException = true;
                        options.EnableGrpcAspNetCoreSupport = false;
                        options.Filter = ctx => Array.IndexOf(ignoreRoutes, ctx.Request.Path) != -1;
                    })
                    .AddHttpClientInstrumentation()
                    .AddJaegerExporter(options =>
                    {
                        DistributedTracingToolConfiguration configuration =
                            appConfiguration.DistributedTracing!.Jaeger!;

                        options.AgentHost = configuration.Host;
                        options.AgentPort = configuration.Port!.Value;
                    });
            });

        return services;
    }
}
