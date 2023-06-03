using FluentValidation;
using JacksonVeroneze.TemplateWebApi.Application;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtension
{
    public static IServiceCollection AddMediatr(
        this IServiceCollection services)
    {
        services.AddMediatR(conf =>
                conf.RegisterServicesFromAssemblyContaining<ApplicationMetadata>())
            .AddValidatorsFromAssemblyContaining(typeof(ApplicationMetadata));

        return services;
    }
}
