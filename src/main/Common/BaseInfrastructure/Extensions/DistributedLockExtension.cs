using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class DistributedLockExtension
{
    public static IServiceCollection AddDistributedLock(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        IConnectionMultiplexer conn = services.BuildServiceProvider().GetRequiredService<IConnectionMultiplexer>();

        List<RedLockMultiplexer> multiplexer =
        [
            (ConnectionMultiplexer)conn
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