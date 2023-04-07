using JacksonVeroneze.TemplateWebApi.Api.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting web application");

    WebApplicationBuilder builder =
        WebApplication.CreateBuilder(args);

    builder.Host.ConfigureHostOptions(o =>
        o.ShutdownTimeout = TimeSpan.FromSeconds(30));

    builder.Configuration
        .AddEnvironmentVariables("APP_CONFIG_");

    // AppConfiguration
    AppConfiguration appConfiguration =
        builder.Services.AddAppConfigs(builder.Configuration);

    // ConfigureLogger
    builder.Host.AddLogger(appConfiguration);

    // ConfigureServices
    builder.Services.ConfigureServices(appConfiguration);

    WebApplication app = builder.Build();

    app.Configure();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down");
    Log.CloseAndFlush();
}