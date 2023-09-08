using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.v1.Mappers;
using JacksonVeroneze.TemplateWebApi.Infrastructure.Mappers;
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
            cfg.AddProfile<CommonMapper>();
            cfg.AddProfile<PaginationMapper>();
            //
            cfg.AddProfile<UserMapper>();
            cfg.AddProfile<EmailMapper>();
            //
            cfg.AddProfile<UserInfraMapper>();
            cfg.AllowNullCollections = true;
        });

        configuration.AssertConfigurationIsValid();

        services.AddSingleton(configuration.CreateMapper());

        return services;
    }
}
