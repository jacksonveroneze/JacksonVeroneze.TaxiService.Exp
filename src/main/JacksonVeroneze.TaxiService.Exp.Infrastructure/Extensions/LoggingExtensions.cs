using JacksonVeroneze.NET.Logging.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtensions
{
    public static WebApplicationBuilder AddLogger(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        builder.Logging.ClearProviders();

        builder.Host.AddLogging(conf =>
        {
            conf.ApplicationName = appConfiguration
                .Application!.Name;

            conf.ApplicationVersion = appConfiguration
                .Application!.Version!.ToString();
        });

        return builder;
    }
}