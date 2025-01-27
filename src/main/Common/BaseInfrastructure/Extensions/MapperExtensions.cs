using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Common.BaseApplication.Mappers;

namespace TaxiService.Common.BaseInfrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IMapperConfigurationExpression AddCommonMapper(
        this IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<CommonMapper>();
        cfg.AddProfile<PaginationCursorMapper>();
        cfg.AddProfile<PaginationOffsetMapper>();

        return cfg;
    }
}