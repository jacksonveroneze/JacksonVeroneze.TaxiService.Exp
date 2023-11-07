using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services)
    {

        return services;
    }
}
