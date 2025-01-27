using AutoMapper;
using TaxiService.Modules.Rides.Application.v1.Mappers;

namespace TaxiService.Modules.Rides.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class RidesMapperExtensions
{
    public static IMapperConfigurationExpression AddRidesMapper(
        this IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<CoordinateMapper>();
        cfg.AddProfile<RideMapper>();

        return cfg;
    }
}