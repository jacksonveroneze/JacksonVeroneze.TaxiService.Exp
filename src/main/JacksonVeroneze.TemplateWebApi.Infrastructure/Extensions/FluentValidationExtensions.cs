using FluentValidation;
using FluentValidation.AspNetCore;
using JacksonVeroneze.TemplateWebApi.Application;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class FluentValidationExtensions
{
    public static IServiceCollection AddFluentValidation(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<Metadata>();
        services.AddFluentValidationAutoValidation();

        return services;
    }
}