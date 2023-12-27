using JacksonVeroneze.NET.DistributedCache.Extensions;
using JacksonVeroneze.TaxiService.Exp.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace JacksonVeroneze.TaxiService.Exp.Infrastructure.Extensions;

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
            IConnectionMultiplexer redisConnectionMultiplexer =
                ConnectionMultiplexer.Connect(appConfiguration.Cache!.Endpoint!, options =>
                {
                    options.Ssl = false;
                    options.AbortOnConnectFail = false;
                    options.ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}";
                });

            services.AddSingleton(redisConnectionMultiplexer);

            services.AddStackExchangeRedisCache(options =>
                options.ConnectionMultiplexerFactory =
                    () => Task.FromResult(redisConnectionMultiplexer));

            // services.AddStackExchangeRedisCache(options =>
            // {
            //     options.InstanceName =
            //         $"{appConfiguration.AppName}-" +
            //         $"{appConfiguration.AppVersion}";
            //
            //     options.ConfigurationOptions = new ConfigurationOptions
            //     {
            //         Ssl = false,
            //         AbortOnConnectFail = false,
            //         EndPoints = { appConfiguration.Cache!.Endpoint! },
            //         ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}"
            //     };
            // });
        }

        return services;
    }
}