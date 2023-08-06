using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.Old.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Old.State;
using JacksonVeroneze.TemplateWebApi.Domain.Entities.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Filters.Old;
using JacksonVeroneze.TemplateWebApi.Domain.Results.Old.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers.Old;

public class StateMapper : Profile
{
    public StateMapper()
    {
        CreateMap<StateResult, StateEntity>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        CreateMap<StateEntity, StateResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        CreateMap<StateEntity, GetStateByIdQueryQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src))
            .ForMember(dest => dest.Messages, opts => opts.Ignore());

        CreateMap<Page<StateEntity>, GetStatePagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.Pagination))
            .ForMember(dest => dest.Messages, opts => opts.Ignore());

        CreateMap<GetStatePagedQuery, StateAllFilter>()
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        CreateMap<GetStateByIdQuery, StateByIdFilter>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));
    }
}
