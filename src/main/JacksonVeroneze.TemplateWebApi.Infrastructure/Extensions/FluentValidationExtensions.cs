using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Application.AssemblyReference>();

        return services;
    }
}
