using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services)
    {
        services.RegisterEasyNetQ(
            "host=10.0.0.199;username=admin;password=admin");

        return services;
    }
}
