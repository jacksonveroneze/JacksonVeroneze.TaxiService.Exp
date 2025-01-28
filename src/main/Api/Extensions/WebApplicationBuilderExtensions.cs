using AutoMapper;
using JacksonVeroneze.TaxiService.Exp.Api.Middlewares;
using TaxiService.Common.BaseApplication.Interfaces.UnitOfWork;
using TaxiService.Common.BaseApplication.Mappers;
using TaxiService.Common.BaseInfrastructure;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Passengers.Infrastructure.EfCore.Extensions;
using TaxiService.Modules.Passengers.Infrastructure.Extensions;
using TaxiService.Modules.Passengers.PublicApi;
using TaxiService.Modules.Rides.Infrastructure.EfCore.Extensions;
using TaxiService.Modules.Rides.Infrastructure.Extensions;

namespace TaxiService.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder Configure(
        this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureHostOptions(options =>
            options.ShutdownTimeout = TimeSpan.FromSeconds(2));

        builder.Configuration
            .AddEnvironmentVariables("APP_CONFIG_");

        AppConfiguration appConfiguration =
            builder.Services.AddAppConfigs(builder.Configuration);

        builder.AddLogger(appConfiguration);

        builder.ConfigureDefaultServices(appConfiguration);
        builder.ConfigureInfraCommonModule(appConfiguration);
        builder.ConfigurePassengerModule(appConfiguration);
        builder.ConfigureRideModule(appConfiguration);

        // TODO
        builder.Services.AddPassangerPublicApi();

        return builder;
    }

    private static WebApplicationBuilder ConfigureDefaultServices(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services
            .AddBehavior()
            .AddJsonOptionsSerialize()
            .AddCorrelation()
            .AddCommonMapper()
            .AddDistributedLock(appConfiguration)
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddCached(appConfiguration)
            .AddCultureConfiguration()
            .AddHttpContextAccessor()
            .AddAppVersioning()
            .AddProblemDetails()
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddHealthChecks();

        if (!builder.Environment.IsProduction())
        {
            builder.Services
                .AddOpenApi();
        }

        return builder;
    }

    private static WebApplicationBuilder ConfigureInfraCommonModule(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services
            .AddCommonInfraAppServices();

        return builder;
    }

    private static WebApplicationBuilder ConfigurePassengerModule(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services
            .AddPassangerInfra()
            .AddPassangerInfraDatabase(appConfiguration);

        return builder;
    }

    private static WebApplicationBuilder ConfigureRideModule(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services
            .AddRideInfra()
            .AddRideInfraDatabase(appConfiguration);

        return builder;
    }
}