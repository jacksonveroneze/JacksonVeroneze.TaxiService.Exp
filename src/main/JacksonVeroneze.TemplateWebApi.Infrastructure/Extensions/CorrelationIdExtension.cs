using CorrelationId.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CorrelationIdExtension
{
    private const string HeaderName = "x-correlation-id";

    public static IServiceCollection AddCorrelation(
        this IServiceCollection services)
    {
        services.AddDefaultCorrelationId(options =>
        {
            options.EnforceHeader = false;
            options.AddToLoggingScope = true;
            options.IncludeInResponse = true;
            options.RequestHeader = HeaderName;
            options.ResponseHeader = HeaderName;
            options.CorrelationIdGenerator = ()
                => Guid.NewGuid().ToString();
        });

        return services;
    }
}