using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}