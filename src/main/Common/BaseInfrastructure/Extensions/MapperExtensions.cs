using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseApplication.Mappers;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddAutoMapper(
        this IServiceCollection services)
    {
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile<CommonMapper>();
            cfg.AddProfile<PaginationCursorMapper>();
            cfg.AddProfile<PaginationOffsetMapper>();
            cfg.AllowNullCollections = true;
        });

        configuration.AssertConfigurationIsValid();

        services.AddSingleton(configuration.CreateMapper());

        return services;
    }
}