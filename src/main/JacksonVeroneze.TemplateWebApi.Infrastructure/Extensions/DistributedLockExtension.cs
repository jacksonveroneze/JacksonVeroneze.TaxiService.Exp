using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DistributedLockExtension
{
    public static IServiceCollection AddDistributedLock(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        List<RedLockMultiplexer> multiplexer =
        [
            ConnectionMultiplexer.Connect(appConfiguration!.Cache!.Endpoint!)
        ];

        ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
        });

        var retryconf = new RedLockRetryConfiguration(5, 1000);

        using RedLockFactory? redlockFactory =
            RedLockFactory.Create(multiplexer, retryconf, loggerFactory);

        services.AddSingleton<IDistributedLockFactory>(redlockFactory);

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configurationOptions = ConfigurationOptions.Parse(appConfiguration!.Cache!.Endpoint!);
            return ConnectionMultiplexer.Connect(configurationOptions);
        });

        return services;

    }
}
