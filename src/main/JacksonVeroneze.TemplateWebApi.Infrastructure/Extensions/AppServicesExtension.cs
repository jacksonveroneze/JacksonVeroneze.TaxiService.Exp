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
        #region City

        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICityDistribCachedRepository, CityDistribCachedRepository>();
        services.AddScoped<ICityPaginatedRepository, CityPaginatedRepository>();

        #endregion

        #region State

        services.AddScoped<IStateRepository, StateRepository>();
        services.AddScoped<IStateDistribCachedRepository, StateDistribCachedRepository>();
        services.AddScoped<IStatePaginatedRepository, StatePaginatedRepository>();

        #endregion

        return services;
    }
}
