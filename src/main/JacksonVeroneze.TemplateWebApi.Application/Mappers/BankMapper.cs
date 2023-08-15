using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Models.Bank;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Bank;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class BankMapper : Profile
{
    public BankMapper()
    {
        // Entity -> Response
        CreateMap<BankEntity, BankResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

        CreateMap<BankEntity, GetBankByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<BankEntity, CreateBankCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<BankEntity>, GetBankPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.Pagination));

        // Query -> Filter
        CreateMap<GetBankPagedQuery, BankPagedFilter>()
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        // Command -> Entity
        CreateMap<CreateBankCommand, BankEntity>()
            .ConstructUsing(src => new BankEntity(src.Name!))
            .ForMember(dest => dest.Id, opts => opts.Ignore())
            .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.UpdatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
            .ForMember(dest => dest.Version, opts => opts.Ignore())
            .ForMember(dest => dest.Status, opts => opts.Ignore());
    }
}
