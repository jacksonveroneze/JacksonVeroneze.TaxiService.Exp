using JacksonVeroneze.NET.DistributedCache.Extensions;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using TaxiService.Common.BaseInfrastructure.Configurations;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CacheServicesExtensions
{
    public static IServiceCollection AddCached(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        ArgumentNullException.ThrowIfNull(appConfiguration);

        services.AddDistributedCacheService();

        if (appConfiguration.Cache?.Type is CacheType.Memory)
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            IConnectionMultiplexer connection =
                ConnectionMultiplexer.Connect(appConfiguration.Cache!.Endpoint!);

            services.AddSingleton<IConnectionMultiplexer>(_ => connection);

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConnectionMultiplexerFactory = () =>
                    Task.FromResult(connection);

                options.InstanceName =
                    $"{appConfiguration.AppName}-" +
                    $"{appConfiguration.AppVersion}";

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Ssl = false,
                    AbortOnConnectFail = false,
                    EndPoints = { appConfiguration.Cache!.Endpoint! },
                    ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}",
                };
            });
        }

        return services;
    }
}
