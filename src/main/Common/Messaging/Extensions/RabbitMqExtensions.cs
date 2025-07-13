using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using TaxiService.Common.Messaging.Abstractions;
using TaxiService.Common.Messaging.Configurations;
using TaxiService.Common.Messaging.RabbitMQ;
using TaxiService.Common.SharedKernel.Extensions;

namespace TaxiService.Common.Messaging.Extensions;

[ExcludeFromCodeCoverage]
public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        IConfigurationSection rabbitConfigSection = configuration
            .GetSection(RabbitMqConfiguration.SectionName);

        RabbitMqConfiguration? rabbitConfig = rabbitConfigSection
            .Get<RabbitMqConfiguration>();

        services.AddConfiguration<RabbitMqConfiguration>(rabbitConfigSection);

        services.AddSingleton<IConnectionFactory>(_ =>
            new ConnectionFactory
            {
                HostName = rabbitConfig!.Address!,
                Port = rabbitConfig.Port!.Value,
                UserName = rabbitConfig.UserName!,
                Password = rabbitConfig.Password!,
                VirtualHost = rabbitConfig.VirtualHost!,
                AutomaticRecoveryEnabled = true,
                RequestedHeartbeat = TimeSpan.FromSeconds(60),
            });

        services.AddScoped<IMessageBusPublisher, RabbitMqPublisher>();
        services.AddScoped<IRabbitMqEventMapper, RabbitMqEventMapper>();

        return services;
    }
}
