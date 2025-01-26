using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Rides.Application.Mappers;
using TaxiService.Modules.Rides.Application.v1.Mappers;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

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
            cfg.AddProfile<CoordinateMapper>();
            cfg.AddProfile<UserMapper>();
            cfg.AddProfile<EmailMapper>();
            cfg.AddProfile<RideMapper>();
            cfg.AllowNullCollections = true;
        });

        configuration.AssertConfigurationIsValid();

        services.AddSingleton(configuration.CreateMapper());

        return services;
    }
}