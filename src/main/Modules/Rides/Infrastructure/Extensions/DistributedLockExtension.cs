using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using TaxiService.Modules.Rides.Infrastructure.Configurations;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

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
            ConnectionMultiplexer.Connect(appConfiguration.Cache!.Endpoint!)
        ];

        // ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        // {
        //     builder.AddConsole();
        //     builder.AddDebug();
        // });

        RedLockRetryConfiguration retryconf = new(5, 1000);

        using RedLockFactory? redlockFactory =
            RedLockFactory.Create(multiplexer, retryconf);

        services.AddSingleton<IDistributedLockFactory>(redlockFactory);

        return services;
    }
}