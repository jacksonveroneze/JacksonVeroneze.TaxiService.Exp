using TaxiService.Api.Middlewares;
using TaxiService.Common.BaseInfrastructure.Configurations;
using TaxiService.Common.BaseInfrastructure.Extensions;
using TaxiService.Modules.Driver.Infrastructure.Extensions;
using TaxiService.Modules.Passenger.Infrastructure.Extensions;
using TaxiService.Modules.Payment.Infrastructure.Extensions;
using TaxiService.Modules.Ride.Infrastructure.Extensions;

namespace TaxiService.Api.Extensions;

internal static class WebApplicationBuilderExtensions
{
    private static readonly string[] Modules =
        ["Drivers", "Passengers", "Payments", "Rides"];

    public static WebApplicationBuilder Configure(
        this WebApplicationBuilder builder)
    {
        builder.Host.ConfigureHostOptions(options =>
            options.ShutdownTimeout = TimeSpan.FromSeconds(2));

        builder.Configuration
            .AddModuleConfiguration(builder.Environment, Modules)
            .AddEnvironmentVariables("APP_CONFIG_");

        builder.Services.AddAppConfigs(builder.Configuration);

        ServiceProvider serviceProvider = builder.Services
            .BuildServiceProvider();

        AppConfiguration appConfiguration = serviceProvider
            .GetRequiredService<AppConfiguration>();

        builder.AddLogger(appConfiguration);

        MetricsExtensions.AddMetrics();

        builder.ConfigureDefaultServices(appConfiguration);
        builder.ConfigureInfraCommonModule();

        builder.ConfigureDriverModule();
        builder.ConfigurePaymentModule();
        builder.ConfigurePassengerModule();
        builder.ConfigureRideModule();

        return builder;
    }

    private static WebApplicationBuilder ConfigureDefaultServices(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("all", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services
            .AddProblemDetails()
            .AddExceptionHandler<CustomExceptionHandler>()
            .AddJsonOptionsSerialize()
            .AddCorrelation()
            .AddCached(appConfiguration)
            .AddDistributedLock(appConfiguration)
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddHttpContextAccessor()
            .AddCultureConfiguration()
            .AddAppVersioning()
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
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddBehavior()
            .AddCommonMapper()
            .AddCommonInfraAppServices();

        return builder;
    }

    private static WebApplicationBuilder ConfigurePassengerModule(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddPassengerInfra(builder.Configuration);

        return builder;
    }

    private static WebApplicationBuilder ConfigureRideModule(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddRideInfra(builder.Configuration);

        return builder;
    }

    private static WebApplicationBuilder ConfigurePaymentModule(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddPaymentInfra(builder.Configuration);

        return builder;
    }

    private static WebApplicationBuilder ConfigureDriverModule(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddDriverInfra(builder.Configuration);

        return builder;
    }
}
