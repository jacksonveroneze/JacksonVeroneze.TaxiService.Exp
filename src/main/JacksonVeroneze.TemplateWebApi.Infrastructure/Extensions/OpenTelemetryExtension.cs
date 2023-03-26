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
        bool isEnable = appConfiguration
            .DistributedTracing?.IsEnabled ?? false;

        if (!isEnable)
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
                    //.AddSource(Instrumentation.ActivitySourceName)
                    .SetSampler(new AlwaysOnSampler())
                    .SetResourceBuilder(resourceBuilder)
                    .AddAspNetCoreInstrumentation(options =>
                    {
                        options.RecordException = true;
                        options.EnableGrpcAspNetCoreSupport = false;
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