using JacksonVeroneze.NET.Logging.Util;
using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Prometheus;
using Serilog;

Log.Logger = BootstrapLogger.CreateLogger();

IDisposable? dotNetRuntimeStats = null;

try
{
    Log.Information("Starting application");

    WebApplicationBuilder builder =
        WebApplication.CreateBuilder(args);

    builder.Host.ConfigureHostOptions(options =>
        options.ShutdownTimeout = TimeSpan.FromSeconds(2));

    // Add custom envs
    builder.Configuration
        .AddEnvironmentVariables("APP_CONFIG_");

    // AppConfiguration
    AppConfiguration appConfiguration =
        builder.Services.AddAppConfigs(builder.Configuration);

    // Metrics
    dotNetRuntimeStats = MetricsExtension
        .AddMetrics(appConfiguration);

    // ConfigureLogger
    builder.Host.AddLogger(appConfiguration);

    // ConfigureServices
    builder.ConfigureServices(appConfiguration);

    WebApplication app = builder.Build();

    app.Lifetime.ApplicationStarted.Register(() =>
        Log.Information("ApplicationStarted"));

    app.Lifetime.ApplicationStopping.Register(() =>
        Log.Information("ApplicationStopping"));

    app.Lifetime.ApplicationStopped.Register(() =>
        Log.Information("ApplicationStopped"));

    app.Configure();

    if (appConfiguration?.PushGateway?.Enable ?? false)
    {
        MetricPusher pusher = new(new MetricPusherOptions
        {
            Endpoint = appConfiguration?.PushGateway?.Address,
            Job = appConfiguration?.AppName,
            Instance = Environment.MachineName,
            AdditionalLabels = new List<Tuple<string, string>> { new("service", appConfiguration!.AppName) }
        });

        pusher.Start();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

    throw;
}
finally
{
    Log.Information("Server Shutting down");
    Log.CloseAndFlush();

    dotNetRuntimeStats?.Dispose();
}

public partial class Program
{
}
