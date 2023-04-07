using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class OpenTelemetryExtension
{
    public static IServiceCollection AddOpenTelemetry(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        bool isEnabled = appConfiguration
            .DistributedTracing?.IsEnabled ?? false;

        if (!isEnabled)
        {
            return services;
        }

        Action<ResourceBuilder> configureResource = r => r.AddService(
            appConfiguration.Application!.Name!,
            serviceVersion: appConfiguration.Application!.Version!,
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
                        options.RecordException = true;
                        options.EnableGrpcAspNetCoreSupport = false;
                        options.Filter = ctx =>
                            ctx.Request.Path != "/metrics" && ctx.Request.Path != "/health";
                    })
                    .AddHttpClientInstrumentation()
                    .AddJaegerExporter(options =>
                    {
                        JaegerConfiguration configuration =
                            appConfiguration.DistributedTracing!.Jaeger!;

                        options.AgentHost = configuration.Host;
                        options.AgentPort = configuration.Port!.Value;
                    });
            });

        return services;
    }
}