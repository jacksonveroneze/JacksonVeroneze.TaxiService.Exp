using FluentValidation;
using JacksonVeroneze.TemplateWebApi.Application;
using JacksonVeroneze.TemplateWebApi.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MediatorExtension
{
    public static IServiceCollection AddMediatr(
        this IServiceCollection services)
    {
        services.AddMediatR(typeof(Metadata))
            .AddValidatorsFromAssemblyContaining(typeof(Metadata))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}