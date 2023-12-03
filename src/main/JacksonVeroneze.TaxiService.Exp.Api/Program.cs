using JacksonVeroneze.NET.Logging.Util;
using JacksonVeroneze.TaxiService.Exp.Api.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using Serilog;

Log.Logger = BootstrapLogger.CreateLogger();

try
{
    Log.Information("Starting application");

    WebApplicationBuilder builder =
        WebApplication.CreateSlimBuilder(args);

    builder.Host.ConfigureHostOptions(options =>
        options.ShutdownTimeout = TimeSpan.FromSeconds(2));

    builder.Configuration
        .AddEnvironmentVariables("APP_CONFIG_");

    AppConfiguration appConfiguration =
        builder.Services.AddAppConfigs(builder.Configuration);

    builder.Host.AddLogger(appConfiguration);

    builder.ConfigureServices(appConfiguration);

    WebApplication app = builder.Build();

    app.Configure();

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
}


namespace JacksonVeroneze.TaxiService.Exp.Api
{
    public partial class Program
    {
    }
}
