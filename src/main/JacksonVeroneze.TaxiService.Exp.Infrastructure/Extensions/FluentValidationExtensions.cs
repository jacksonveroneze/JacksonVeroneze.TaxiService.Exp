using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AssemblyReferenceApp = JacksonVeroneze.TaxiService.Exp.Application.AssemblyReference;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

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
