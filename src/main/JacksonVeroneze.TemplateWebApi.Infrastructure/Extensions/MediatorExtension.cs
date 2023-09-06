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
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblyContaining<Application.AssemblyReference>());

        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
