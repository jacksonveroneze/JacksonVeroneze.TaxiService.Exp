namespace TaxiService.Common.BaseApplication.Interfaces.Mapper;

public interface IDataMapper
{
    TDestination Map<TDestination>(object? source);

    TDestination Map<TSource, TDestination>(TSource? source);
}
