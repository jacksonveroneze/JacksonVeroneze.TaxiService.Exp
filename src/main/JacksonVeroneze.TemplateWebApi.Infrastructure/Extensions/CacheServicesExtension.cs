using JacksonVeroneze.NET.Cache.Extensions;
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
        services.AddDistribCache();
        services.AddMemoryCache();

        if (appConfiguration.CacheType == CacheType.Memory)
        {
            services.AddDistributedMemoryCache();
        }
        else
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.InstanceName =
                    $"{appConfiguration.Application!.Name!}-" +
                    $"{appConfiguration.Application!.Version!}";

                options.ConfigurationOptions = new ConfigurationOptions
                {
                    Ssl = false,
                    AbortOnConnectFail = false,
                    EndPoints = { appConfiguration.CacheEndpoint },
                    ClientName = $"{appConfiguration.Application.Name}-{Guid.NewGuid()}"
                };
            });
        }

        return services;
    }
}