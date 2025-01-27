using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

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