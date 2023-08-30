using JacksonVeroneze.NET.Pagination;
using JacksonVeroneze.TemplateWebApi.Application.Models.User;
using JacksonVeroneze.TemplateWebApi.Application.Queries.User;
using JacksonVeroneze.TemplateWebApi.Domain.Entities;
using JacksonVeroneze.TemplateWebApi.Domain.Filters;

namespace JacksonVeroneze.TemplateWebApi.Application.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        // Entity -> Response
        CreateMap<UserEntity, UserResponse>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.Status));

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
    }
}
