using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using JacksonVeroneze.TemplateWebApi.Application.v1.Mappers;

namespace JacksonVeroneze.TemplateWebApi.Util.Tests.Builders.AutoMapper;

[ExcludeFromCodeCoverage]
public class AutoMapperBuilder
{
    private AutoMapperBuilder()
    {
    }

    public static IMapper Build<T>()
        where T : Profile, new()
    {
        return Build(typeof(T));
    }

    public static IMapper Build<T1, T2>()
        where T1 : Profile, new()
        where T2 : Profile, new()
    {
        return Build(typeof(T1), typeof(T2));
    }

    private static IMapper Build(params Type[]? types)
    {
        MapperConfiguration configuration = new(config =>
        {
            config.AddProfile<CommonMapper>();
            config.AddProfile<PaginationMapper>();
            config.AllowNullCollections = true;

            if (types == null)
            {
                return;
            }

            foreach (Type type in types)
            {
                config.AddProfile(type);
            }
        });

        configuration.AssertConfigurationIsValid();

        return configuration.CreateMapper();
    }
}
