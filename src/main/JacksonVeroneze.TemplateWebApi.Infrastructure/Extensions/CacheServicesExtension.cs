using JacksonVeroneze.NET.DistributedCache.Extensions;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class CacheServicesExtension
{
    public static IServiceCollection AddCached(
        this IServiceCollection services,
        AppConfiguration appConfiguration)
    {
        services.AddDistributedCacheService();

        if (appConfiguration.CacheType == CacheType.Memory)
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
                    EndPoints = { appConfiguration.CacheEndpoint },
                    ClientName = $"{appConfiguration.AppName}-{Guid.NewGuid()}"
                };
            });
        }

        return services;
    }
}