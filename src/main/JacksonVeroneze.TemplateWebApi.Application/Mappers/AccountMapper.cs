using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Commands.Account;
using JacksonVeroneze.TemplateWebApi.Application.Models.Account;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Account;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        // Entity -> Response
        CreateMap<AccountEntity, AccountResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id));

        CreateMap<AccountEntity, GetAccountByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<AccountEntity, CreateAccountCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<IReadOnlyCollection<AccountEntity>, GetAccountPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src))
            .ForMember(dest => dest.Pagination, opts => opts.Ignore());

        // Query -> Filter
        // CreateMap<GetAccountPagedQuery, AccountPagedFilter>()
        //     .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
        //     .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
        //     .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        // Command -> Entity
        // CreateMap<CreateAccountCommand, AccountEntity>()
        //     .ConstructUsing(src => new AccountEntity(
        //         new PersonName(src.Name!), new Email(src.Email!)))
        //     .ForMember(dest => dest.Id, opts => opts.Ignore())
        //     .ForMember(dest => dest.Accounts, opts => opts.Ignore())
        //     .ForMember(dest => dest.Events, opts => opts.Ignore())
        //     .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
        //     .ForMember(dest => dest.UpdatedAt, opts => opts.Ignore())
        //     .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
        //     .ForMember(dest => dest.Version, opts => opts.Ignore());
    }
}
