using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TaxiService.Modules.Passengers.Application.v1.Mappers;

namespace TaxiService.Modules.Passengers.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class MapperExtensions
{
    public static IServiceCollection AddAutoMapper(
        this IServiceCollection services)
    {
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile<UserMapper>();
            cfg.AddProfile<EmailMapper>();
        });

        configuration.AssertConfigurationIsValid();

        services.AddSingleton(configuration.CreateMapper());

        return services;
    }
}