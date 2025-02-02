using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Modules.Payments.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class ServicesExtensions
{
    public static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        return services;
    }
}