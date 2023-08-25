using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Commands.User;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.Client;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;
using JacksonVeroneze.TemplateWebApi.Domain.ValueObjects;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // Entity -> Response
        CreateMap<UserEntity, UserResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday));

        CreateMap<UserEntity, GetUserByIdQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<UserEntity, CreateUserCommandResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src));

        CreateMap<Page<UserEntity>, GetUserPagedQueryResponse>()
            .ForMember(dest => dest.Data, opts => opts.MapFrom(src => src.Data))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src.Pagination));

        // Query -> Filter
        CreateMap<GetUserPagedQuery, UserPagedFilter>()
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status))
            .ForMember(dest => dest.Pagination, opts => opts.MapFrom(src => src));

        // Command -> Entity
        CreateMap<CreateUserCommand, UserEntity>()
            .ConstructUsing(src => new UserEntity(
                new NameValueObject(src.Name!), src.Birthday!.Value, src.Gender!.Value))
            .ForMember(dest => dest.Id, opts => opts.Ignore())
            .ForMember(dest => dest.Status, opts => opts.Ignore())
            .ForMember(dest => dest.Emails, opts => opts.Ignore())
            .ForMember(dest => dest.Phones, opts => opts.Ignore())
            .ForMember(dest => dest.ActivedOnUtc, opts => opts.Ignore())
            .ForMember(dest => dest.InactivedOnUtc, opts => opts.Ignore())
            .ForMember(dest => dest.Events, opts => opts.Ignore())
            .ForMember(dest => dest.CreatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.UpdatedAt, opts => opts.Ignore())
            .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
            .ForMember(dest => dest.Version, opts => opts.Ignore());
    }
}
