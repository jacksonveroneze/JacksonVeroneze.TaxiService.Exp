using CorrelationId.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CorrelationIdExtension
{
    public static IServiceCollection AddCorrelation(
        this IServiceCollection services)
    {
        services.AddDefaultCorrelationId(options =>
        {
            options.EnforceHeader = false;
            options.AddToLoggingScope = true;
            options.IncludeInResponse = true;
            options.CorrelationIdGenerator =
                () => Guid.NewGuid().ToString();
        });

        return services;
    }
}
