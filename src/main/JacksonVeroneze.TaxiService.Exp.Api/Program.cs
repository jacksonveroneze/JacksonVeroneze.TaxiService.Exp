using JacksonVeroneze.NET.Logging.Util;
using JacksonVeroneze.TaxiService.Exp.Api.Extensions;
using Serilog;

Log.Logger = BootstrapLogger.CreateLogger();

try
{
    Log.Information("Starting application");

    WebApplicationBuilder builder =
        WebApplication.CreateSlimBuilder(args);

    builder.Configure();

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
