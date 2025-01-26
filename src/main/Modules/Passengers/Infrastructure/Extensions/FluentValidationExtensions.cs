using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AssemblyReferenceApp = TaxiService.Modules.Passengers.Application.AssemblyReference;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AssemblyReferenceApp>();

        return services;
    }
}