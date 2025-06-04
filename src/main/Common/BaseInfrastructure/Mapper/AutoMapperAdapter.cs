using AutoMapper;
using TaxiService.Common.BaseApplication.Interfaces.Mapper;

namespace TaxiService.Common.BaseInfrastructure.Mapper;

public class AutoMapperAdapter(IMapper mapper) : IDataMapper
{
    public TDestination Map<TDestination>(object? source)
    {
        return mapper.Map<TDestination>(source);
    }

    public TDestination Map<TSource, TDestination>(
        TSource? source)
    {
        return mapper.Map<TDestination>(source);
    }
}
