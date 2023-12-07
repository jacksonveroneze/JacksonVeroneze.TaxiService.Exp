using JacksonVeroneze.NET.Logging.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.Extensions.Hosting;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtension
{
    public static IHostBuilder AddLogger(this IHostBuilder host,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        host.AddLogging(conf =>
        {
            conf.ApplicationName = appConfiguration
                .Application!.Name;

            conf.ApplicationVersion = appConfiguration
                .Application!.Version!.ToString();
        });

        return host;
    }
}