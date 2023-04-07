using JacksonVeroneze.TemplateWebApi.Application.Models.State;
using JacksonVeroneze.TemplateWebApi.Application.Queries;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.Results.State;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class StateMapper : Profile
{
    public StateMapper()
    {
        CreateMap<StateResult, StateResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

        CreateMap<StateResult, GetStateByIdQueryQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src))
            .ForMember(dest => dest.Messages, opts => opts.Ignore());

        CreateMap<ICollection<StateResult>, GetStatePagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src))
            .ForMember(dest => dest.Messages, opts => opts.Ignore());

        CreateMap<GetStateByIdQuery, StateFilter>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));
    }
}