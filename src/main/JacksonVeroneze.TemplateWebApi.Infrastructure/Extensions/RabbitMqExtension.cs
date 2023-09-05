using EasyNetQ;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class RabbitMqExtension
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services)
    {
        services.AddSingleton(RabbitHutch.CreateBus("host=10.0.0.199;username=admin;password=admin"));

        return services;
    }
}
