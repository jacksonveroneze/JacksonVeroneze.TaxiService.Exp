using JacksonVeroneze.TemplateWebApi.Application.Interfaces.Repositories;
using JacksonVeroneze.TemplateWebApi.Infrastructure.DataProviders.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AppServicesExtension
{
    public static IServiceCollection AddAppServices(
        this IServiceCollection services)
    {
        // City
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICityDistribCachedRepository, CityDistribCachedRepository>();
        services.AddScoped<ICityPaginatedRepository, CityPaginatedRepository>();

        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<IStateDistribCachedRepository, StateDistribCachedRepository>();

        return services;
    }
}