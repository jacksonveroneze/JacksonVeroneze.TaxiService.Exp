using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.v1.Mappers;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class PassengersMapperExtensions
{
    public static IMapperConfigurationExpression AddPassengersMapper(
        this IMapperConfigurationExpression cfg)
    {
        cfg.AddProfile<UserMapper>();
        cfg.AddProfile<EmailMapper>();

        return cfg;
    }
}