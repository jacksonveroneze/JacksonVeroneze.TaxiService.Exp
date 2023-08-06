using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Old.City;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Old.City;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.City;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers.Old;

public class CityMapper : Profile
{
    public CityMapper()
    {
        CreateMap<CityResult, CityEntity>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        CreateMap<CityEntity, CityResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        CreateMap<Page<CityEntity>, GetCityByStatePagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.Pagination))
            .ForMember(dest => dest.Messages, opts => opts.Ignore());

        CreateMap<GetCityByStatePagedQuery, StateByIdFilter>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.StateId));

        CreateMap<GetCityByStatePagedQuery, CityByStateFilter>()
            .ForMember(dest => dest.StateId, opts => opts.MapFrom(src => src.StateId))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));
    }
}
