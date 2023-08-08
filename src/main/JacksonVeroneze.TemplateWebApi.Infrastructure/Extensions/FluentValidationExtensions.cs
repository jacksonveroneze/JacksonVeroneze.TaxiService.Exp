using FluentValidation;
using JacksonVeroneze.TemplateWebApi.Application;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}
