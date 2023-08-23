using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Client;
using JacksonVeroneze.TemplateWebApi.Application.Models.Client;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class ClientMapper : Profile
{
    public ClientMapper()
    {
        // Entity -> Response
        CreateMap<ClientEntity, ClientResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email.Value));

        CreateMap<ClientEntity, GetClientByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<ClientEntity, CreateClientCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<ClientEntity>, GetClientPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.Pagination));

        // Query -> Filter
        CreateMap<GetClientPagedQuery, ClientPagedFilter>()
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        // Command -> Entity
        CreateMap<CreateClientCommand, ClientEntity>()
            .ConstructUsing(src => new ClientEntity(
                new PersonName(src.Name!), new Email(src.Email!)))
            .ForMember(dest => dest.Id, opts => opts.Ignore())
            .ForMember(dest => dest.Accounts, opts => opts.Ignore())
            .ForMember(dest => dest.Events, opts => opts.Ignore())
            .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.UpdatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
            .ForMember(dest => dest.Version, opts => opts.Ignore());
    }
}
