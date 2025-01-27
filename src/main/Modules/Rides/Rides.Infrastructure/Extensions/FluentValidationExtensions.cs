using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
internal static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}