using System.Net;
using System.Text.Json;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Ben.Diagnostics;
using CorrelationId;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;
using Prometheus;
using Hellang.Middleware.ProblemDetails;
using JacksonVeroneze.TemplateWebApi.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace JacksonVeroneze.TemplateWebApi.Api.Extensions;

public static class ApiConfigExtension
{
    public static WebApplicationBuilder ConfigureServices(
        this WebApplicationBuilder builder,
        AppConfiguration appConfiguration)
    {
        builder.Services
            .AddControllers(options => options.AddResultConvention(resultStatusMap => resultStatusMap
                .AddDefaultMap()
                .For(ResultStatus.Ok, HttpStatusCode.OK, resultStatusOptions => resultStatusOptions
                    .For("POST", HttpStatusCode.Created)
                    .For("DELETE", HttpStatusCode.NoContent))
                .Remove(ResultStatus.Forbidden)
                .Remove(ResultStatus.Unauthorized)
            ))
            .AddJsonOptionsSerialize()
            .ConfigureApiBehaviorOptions(options =>
                options.SuppressInferBindingSourcesForParameters = true);

        if (!builder.Environment.IsProduction())
        {
            builder.Services
                .AddEndpointsApiExplorer()
                .AddSwagger(appConfiguration)
                .AddMiniProfiler();
        }

        builder.Services
            .AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (_, _)
                    => false;

                options.Map<ValidationException>(ex =>
                    new ValidationProblemDetails(ex.ErrorsDictionary)
                    {
                        Title = ex.Message, Status = StatusCodes.Status404NotFound
                    });
            })
            .AddAppServices()
            .AddAutoMapper()
            .AddCorrelation()
            .AddMediatr()
            .AddFluentValidation()
            .AddAppVersioning()
            .AddHttpContextAccessor()
            .AddAuthentication(appConfiguration)
            .AddAuthorization(appConfiguration)
            .AddOpenTelemetry(appConfiguration)
            .AddCached(appConfiguration)
            .AddHttpClients(appConfiguration)
            .AddCultureConfiguration()
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            })
            .AddHealthChecks();

        return builder;
    }

    public static WebApplication Configure(
        this WebApplication app)
    {
        app.UseProblemDetails();

        if (app.Environment.IsDevelopment())
        {
            app.UseBlockingDetection();
            app.UseMiniProfiler();
            app.AddSwagger();
        }

        app.UseHttpMetrics()
            .UseCorrelationId()
            //.UseCustomGlobalErrorHandler()
            .UseAuthentication()
            .UseAuthorization();

        app.MapMetrics();
        app.UseHealthChecks("/health");

        app.MapControllers();

        return app;
    }
}
