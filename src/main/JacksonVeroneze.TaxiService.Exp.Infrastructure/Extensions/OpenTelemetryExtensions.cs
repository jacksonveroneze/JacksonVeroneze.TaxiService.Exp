using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class OpenTelemetryExtensions
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

        services.Configure<AspNetCoreTraceInstrumentationOptions>(options =>
        {
            options.Filter = ctx => ctx.Request.Path != "/metrics" &&
                                    ctx.Request.Path != "/health";
        });

        services.AddOpenTelemetry()
            .ConfigureResource(ConfigureResource)
            .AddMetrics()
            .AddTracing(appConfiguration);

        return services;

        void ConfigureResource(ResourceBuilder r) => r.AddService(
            appConfiguration.AppName,
            serviceVersion: appConfiguration.AppVersion.ToString(),
            serviceInstanceId: Environment.MachineName);
    }

    private static OpenTelemetryBuilder AddMetrics(
        this OpenTelemetryBuilder builder)
    {
        builder.WithMetrics(opts => opts
            .AddProcessInstrumentation()
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddRuntimeInstrumentation()
            .AddPrometheusExporter());

        return builder;
    }

    private static OpenTelemetryBuilder AddTracing(
        this OpenTelemetryBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.WithTracing(conf =>
        {
            conf.AddAspNetCoreInstrumentation(options =>
                {
                    options.RecordException = true;
                })
                .AddEntityFrameworkCoreInstrumentation(options =>
                {
                    options.SetDbStatementForText = true;
                    options.SetDbStatementForStoredProcedure = true;
                })
                .AddHttpClientInstrumentation()
                .AddRedisInstrumentation()
                .AddJaegerExporter(options =>
                {
                    DistributedTracingToolConfiguration configuration =
                        appConfiguration.DistributedTracing!.Jaeger!;

                    options.AgentHost = configuration.Host;
                    options.AgentPort = configuration.Port!.Value;
                });
        });

        return builder;
    }
}