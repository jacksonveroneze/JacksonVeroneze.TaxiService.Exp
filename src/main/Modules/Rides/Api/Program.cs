using JacksonVeroneze.NET.Logging.Util;
using Serilog;
using TaxiService.Modules.Rides.Api.Extensions;

Log.Logger = BootstrapLogger.CreateLogger();

try
{
    Log.Information("Starting application");

    WebApplicationBuilder builder =
        WebApplication.CreateSlimBuilder(args);

    builder.Configure();

    WebApplication app = builder.Build();

    app.Configure();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");

    throw;
}
finally
{
    Log.Information("Server Shutting down");
    await Log.CloseAndFlushAsync();
}