using JacksonVeroneze.TaxiService.Exp.Api.Middlewares;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.EfCore.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.MongoDb.Extensions;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Extensions;

public static class WebApplicationBuilderExtensions
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

        builder.ConfigureServices(appConfiguration);

        return builder;
    }

    private static WebApplicationBuilder ConfigureServices(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        if (!builder.Environment.IsProduction())
        {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen();
        }

        builder.Services
            .AddJsonOptionsSerialize()
            .AddAutoMapper()
            .AddCorrelation()
            .AddMediatr()
            .AddFluentValidation()
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddCultureConfiguration()
            .AddHttpContextAccessor()
            .AddFluentValidationAutoValidation()
            .AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
                    ctx.ProblemDetails.Extensions.Add("instance",
                        $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                };
            })
            .AddExceptionHandler<GlobalExceptionHandler>()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddHealthChecks();

        builder.Services
            .AddAppServices()
            .AddEfCoreServices()
            .AddCached(appConfiguration)
            .AddDatabaseEfCore(appConfiguration)
            .AddDatabaseMongoDb()
            .AddRabbitMq();

        return builder;
    }
}