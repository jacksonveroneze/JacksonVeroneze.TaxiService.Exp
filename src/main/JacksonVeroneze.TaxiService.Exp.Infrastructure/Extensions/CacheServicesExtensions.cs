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
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName =
                    $"{appConfiguration.AppName}-" +
                    $"{appConfiguration.AppVersion}";

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Ssl = false,
                    AbortOnConnectFail = false,
                    EndPoints = { appConfiguration.Cache!.Endpoint! },
                    ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}"
                };
            });
        }

        return services;
    }
}
