using Ben.Diagnostics;
using CorrelationId;
using JacksonVeroneze.TaxiService.Exp.Api.Endpoints;
using JacksonVeroneze.TaxiService.Exp.Api.Middlewares;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;
using Prometheus;
using Serilog;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

namespace JacksonVeroneze.TaxiService.Exp.Api.Extensions;

public static class ApiConfigExtension
{
    public static WebApplicationBuilder ConfigureServices(
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
            .AddExceptionHandler<ExceptionToProblemDetailsHandler>()
            //.AddExceptionHandler<GlobalExceptionHandler>()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddHealthChecks();

        builder.Services
            .AddAppServices()
            .AddCached(appConfiguration)
            .AddDatabase(appConfiguration)
            .AddRabbitMq();

        return builder;
    }

    public static WebApplication Configure(
        this WebApplication app)
    {
        app.UseExceptionHandler();

        app.Lifetime.ApplicationStarted.Register(() =>
            Log.Information("ApplicationStarted"));

        app.Lifetime.ApplicationStopping.Register(() =>
            Log.Information("ApplicationStopping"));

        app.Lifetime.ApplicationStopped.Register(() =>
            Log.Information("ApplicationStopped"));

        if (app.Environment.IsDevelopment())
        {
            app.UseBlockingDetection()
                .UseSwagger()
                .UseSwaggerUI();
        }

        app.UseHttpMetrics()
            .UseCorrelationId();

        app.MapMetrics();
        app.UseHealthChecks("/health");

        app.AddUserEndpoints();
        app.AddRideEndpoints();
        app.AddPositionEndpoints();

        return app;
    }
}
