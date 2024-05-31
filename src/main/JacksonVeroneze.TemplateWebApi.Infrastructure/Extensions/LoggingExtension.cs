using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggingExtension
{
    public static WebApplicationBuilder AddLogger(
        this WebApplicationBuilder builder,
        IConfiguration conf)
    {
        builder.Host.UseSerilog((hostingContext,
            services, loggerConfiguration) =>
        {
            loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .ReadFrom.Services(services)
                .ConfigureEnrich();
        });

        return builder;
    }

    private static void ConfigureEnrich(
        this LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithExceptionDetails()
            .Enrich.WithEnvironmentName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithCorrelationIdHeader()
            .Enrich.WithSpan();
    }
}
