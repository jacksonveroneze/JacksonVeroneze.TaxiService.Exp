using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace JacksonVeroneze.TemplateWebApi.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class AutoMapperExtension
{
    public static IServiceCollection AddAutoMapper(
        this IServiceCollection services)
    {
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AllowNullCollections = true;
        });

        configuration.AssertConfigurationIsValid();

        services.AddSingleton(configuration.CreateMapper());

        return services;
    }
}