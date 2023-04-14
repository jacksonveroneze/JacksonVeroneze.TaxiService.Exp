using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.City;
using JacksonVeroneze.TemplateWebApi.Application.Queries.City;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.City;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class CityMapper : Profile
{
    public CityMapper()
    {
        CreateMap<CityResult, CityResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        CreateMap<Page<CityResult>, GetCityByStatePagedQueryResponse>()
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