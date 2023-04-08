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

    builder.Host.ConfigureHostOptions(options =>
        options.ShutdownTimeout = TimeSpan.FromSeconds(5));

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

    app.Lifetime.ApplicationStarted.Register(() => 
        Log.Information("ApplicationStarted"));
    
    app.Lifetime.ApplicationStopping.Register(() => 
        Log.Information("ApplicationStopping"));
    
    app.Lifetime.ApplicationStopped.Register(() => 
        Log.Information("ApplicationStopped"));
    
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